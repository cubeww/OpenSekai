using System;
using System.Collections.Generic;
using System.IO;

namespace AcbDecoder
{
    public sealed class AcbFile
    {
        private readonly byte[] _data;
        private readonly ushort _awbSubKey;

        private AcbFile(byte[] data, IReadOnlyList<AcbEntry> entries, ushort awbSubKey)
        {
            _data = data;
            Entries = entries;
            _awbSubKey = awbSubKey;
        }

        public IReadOnlyList<AcbEntry> Entries { get; }

        public static AcbFile Load(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            return Load(File.ReadAllBytes(path));
        }

        public static AcbFile Load(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (data.Length < 0x20 || !BinaryUtil.HasId(data, 0, "@UTF"))
                throw new AcbException("Input is not an ACB @UTF file.");

            var header = CriUtfTable.Open(data, 0);
            if (header.RowCount != 1 || header.Name != "Header")
                throw new AcbException("ACB header table is not valid.");
            if (!header.TryGetData(0, "AwbFile", out int awbOffset, out int awbSize) || awbSize <= 0)
                throw new AcbException("ACB has no embedded AwbFile data.");

            Dictionary<int, string> namesByWaveId = TryBuildNames(data, header);
            var awb = new AwbParser(data, awbOffset, awbSize);
            IReadOnlyList<AcbEntry> entries = awb.ReadEntries(namesByWaveId);
            return new AcbFile(data, entries, awb.SubKey);
        }

        public AudioData Decode(int index, DecodeOptions? options = null)
        {
            if (index < 0 || index >= Entries.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            return Decode(Entries[index], options);
        }

        public AudioData Decode(AcbEntry entry, DecodeOptions? options = null)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));
            if (entry.Offset < 0 || entry.Size < 0 || entry.Offset > _data.Length || _data.Length - entry.Offset < entry.Size)
                throw new AcbException("ACB entry range is invalid.");

            options = options ?? DecodeOptions.Default;
            switch (entry.Codec)
            {
                case AcbCodec.Hca:
                    ushort subkey = options.HcaSubKey ?? _awbSubKey;
                    return HcaDecoder.Decode(_data, entry.Offset, entry.Size, options.HcaKey, subkey);
                default:
                    throw new AcbException($"Codec {entry.Codec} is not implemented yet. This decoder currently supports HCA inside ACB/AWB.");
            }
        }

        private static Dictionary<int, string> TryBuildNames(byte[] data, CriUtfTable header)
        {
            try
            {
                return new NameResolver(data, header).Resolve();
            }
            catch (AcbException)
            {
                return new Dictionary<int, string>();
            }
        }

        private static bool TryGetWaveId(CriUtfTable waveforms, int row, out int waveId)
        {
            waveId = 0;
            if (row < 0 || row >= waveforms.RowCount)
                return false;
            if (waveforms.TryGetUInt16(row, "Id", out ushort id) ||
                waveforms.TryGetUInt16(row, "MemoryAwbId", out id) ||
                waveforms.TryGetUInt16(row, "StreamAwbId", out id))
            {
                waveId = id;
                return true;
            }

            return false;
        }

        private sealed class NameResolver
        {
            private readonly byte[] _data;
            private readonly CriUtfTable _header;
            private readonly Dictionary<int, string> _names = new Dictionary<int, string>();

            private CriUtfTable? _cueNames;
            private CriUtfTable? _cues;
            private CriUtfTable? _waveforms;
            private CriUtfTable? _synths;
            private CriUtfTable? _sequences;
            private CriUtfTable? _tracks;
            private CriUtfTable? _trackCommands;
            private CriUtfTable? _blocks;
            private CriUtfTable? _blockSequences;

            public NameResolver(byte[] data, CriUtfTable header)
            {
                _data = data;
                _header = header;
            }

            public Dictionary<int, string> Resolve()
            {
                _cueNames = LoadTable("CueNameTable");
                _cues = LoadTable("CueTable");
                _waveforms = LoadTable("WaveformTable");
                if (_cueNames == null || _cues == null || _waveforms == null)
                    return _names;

                for (int i = 0; i < _cueNames.RowCount; i++)
                {
                    if (!_cueNames.TryGetUInt16(i, "CueIndex", out ushort cueIndex) ||
                        !_cueNames.TryGetString(i, "CueName", out string cueName) ||
                        string.IsNullOrEmpty(cueName))
                        continue;

                    ResolveCue(cueIndex, cueName, 0);
                }

                return _names;
            }

            private CriUtfTable? LoadTable(string name)
            {
                if (!_header.TryGetData(0, name, out int offset, out int size) || size <= 0)
                    return null;
                return CriUtfTable.Open(_data, offset);
            }

            private void ResolveCue(int index, string cueName, int depth)
            {
                if (_cues == null || index < 0 || index >= _cues.RowCount || depth > 8)
                    return;
                if (!_cues.TryGetUInt8(index, "ReferenceType", out byte referenceType) ||
                    !_cues.TryGetUInt16(index, "ReferenceIndex", out ushort referenceIndex))
                    return;

                switch (referenceType)
                {
                    case 1:
                        ResolveWaveform(referenceIndex, cueName);
                        break;
                    case 2:
                        ResolveSynth(referenceIndex, cueName, depth + 1);
                        break;
                    case 3:
                        ResolveSequence(referenceIndex, cueName, depth + 1);
                        break;
                    case 8:
                        ResolveBlockSequence(referenceIndex, cueName, depth + 1);
                        break;
                }
            }

            private void ResolveWaveform(int index, string cueName)
            {
                if (_waveforms == null || !TryGetWaveId(_waveforms, index, out int waveId))
                    return;
                if (_names.TryGetValue(waveId, out string existing))
                {
                    if (existing.IndexOf(cueName, StringComparison.Ordinal) < 0)
                        _names[waveId] = existing + "; " + cueName;
                }
                else
                {
                    _names.Add(waveId, cueName);
                }
            }

            private void ResolveSynth(int index, string cueName, int depth)
            {
                if (depth > 8)
                    return;
                _synths = _synths ?? LoadTable("SynthTable");
                if (_synths == null || index < 0 || index >= _synths.RowCount)
                    return;
                if (!_synths.TryGetData(index, "ReferenceItems", out int offset, out int size))
                    return;

                int count = size / 4;
                for (int i = 0; i < count; i++)
                {
                    int itemOffset = offset + i * 4;
                    ushort type = BinaryUtil.U16BE(_data, itemOffset);
                    ushort itemIndex = BinaryUtil.U16BE(_data, itemOffset + 2);
                    switch (type)
                    {
                        case 0:
                            return;
                        case 1:
                            ResolveWaveform(itemIndex, cueName);
                            break;
                        case 2:
                            ResolveSynth(itemIndex, cueName, depth + 1);
                            break;
                        case 3:
                            ResolveSequence(itemIndex, cueName, depth + 1);
                            break;
                    }
                }
            }

            private void ResolveSequence(int index, string cueName, int depth)
            {
                if (depth > 8)
                    return;
                _sequences = _sequences ?? LoadTable("SequenceTable");
                if (_sequences == null || index < 0 || index >= _sequences.RowCount)
                    return;
                if (!_sequences.TryGetUInt16(index, "NumTracks", out ushort numTracks) ||
                    !_sequences.TryGetData(index, "TrackIndex", out int offset, out int size))
                    return;

                int count = Math.Min(numTracks, size / 2);
                for (int i = 0; i < count; i++)
                    ResolveTrack(ReadInt16BE(offset + i * 2), cueName, depth + 1);
            }

            private void ResolveTrack(int index, string cueName, int depth)
            {
                if (depth > 8)
                    return;
                _tracks = _tracks ?? LoadTable("TrackTable");
                if (_tracks == null || index < 0 || index >= _tracks.RowCount)
                    return;
                if (_tracks.TryGetUInt16(index, "EventIndex", out ushort eventIndex) && eventIndex != 0xFFFF)
                    ResolveTrackCommand(eventIndex, cueName, depth + 1);
            }

            private void ResolveTrackCommand(int index, string cueName, int depth)
            {
                if (depth > 8)
                    return;
                _trackCommands = _trackCommands ?? LoadTable("TrackEventTable") ?? LoadTable("CommandTable");
                if (_trackCommands == null || index < 0 || index >= _trackCommands.RowCount)
                    return;
                if (!_trackCommands.TryGetData(index, "Command", out int offset, out int size))
                    return;

                int pos = 0;
                while (pos + 3 <= size)
                {
                    int commandOffset = offset + pos;
                    ushort code = BinaryUtil.U16BE(_data, commandOffset);
                    byte tlvSize = _data[commandOffset + 2];
                    pos += 3;
                    if (pos + tlvSize > size)
                        break;

                    if ((code == 2000 || code == 2003) && tlvSize >= 4)
                    {
                        ushort type = BinaryUtil.U16BE(_data, offset + pos);
                        ushort itemIndex = BinaryUtil.U16BE(_data, offset + pos + 2);
                        if (type == 2)
                            ResolveSynth(itemIndex, cueName, depth + 1);
                        else if (type == 3)
                            ResolveSequence(itemIndex, cueName, depth + 1);
                    }

                    pos += tlvSize;
                }
            }

            private void ResolveBlockSequence(int index, string cueName, int depth)
            {
                if (depth > 8)
                    return;
                _blockSequences = _blockSequences ?? LoadTable("BlockSequenceTable");
                if (_blockSequences == null || index < 0 || index >= _blockSequences.RowCount)
                    return;

                if (_blockSequences.TryGetUInt16(index, "NumTracks", out ushort numTracks) &&
                    _blockSequences.TryGetData(index, "TrackIndex", out int trackOffset, out int trackSize))
                {
                    int count = Math.Min(numTracks, trackSize / 2);
                    for (int i = 0; i < count; i++)
                        ResolveTrack(ReadInt16BE(trackOffset + i * 2), cueName, depth + 1);
                }

                if (_blockSequences.TryGetUInt16(index, "NumBlocks", out ushort numBlocks) &&
                    _blockSequences.TryGetData(index, "BlockIndex", out int blockOffset, out int blockSize))
                {
                    int count = Math.Min(numBlocks, blockSize / 2);
                    for (int i = 0; i < count; i++)
                        ResolveBlock(ReadInt16BE(blockOffset + i * 2), cueName, depth + 1);
                }
            }

            private void ResolveBlock(int index, string cueName, int depth)
            {
                if (depth > 8)
                    return;
                _blocks = _blocks ?? LoadTable("BlockTable");
                if (_blocks == null || index < 0 || index >= _blocks.RowCount)
                    return;
                if (!_blocks.TryGetUInt16(index, "NumTracks", out ushort numTracks) ||
                    !_blocks.TryGetData(index, "TrackIndex", out int offset, out int size))
                    return;

                int count = Math.Min(numTracks, size / 2);
                for (int i = 0; i < count; i++)
                    ResolveTrack(ReadInt16BE(offset + i * 2), cueName, depth + 1);
            }

            private short ReadInt16BE(int offset)
            {
                return unchecked((short)BinaryUtil.U16BE(_data, offset));
            }
        }
    }
}
