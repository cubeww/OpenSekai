using System;

namespace AcbDecoder
{
    public sealed class HcaDecoder
    {
        private const int HcaMask = 0x7F7F7F7F;
        private const int HcaVersion101 = 0x0101;
        private const int HcaVersion102 = 0x0102;
        private const int HcaVersion103 = 0x0103;
        private const int HcaVersion200 = 0x0200;
        private const int HcaVersion300 = 0x0300;
        private const int Subframes = 8;
        private const int SamplesPerSubframe = 128;
        private const int SamplesPerFrame = Subframes * SamplesPerSubframe;
        private const int MaxChannels = 16;

        private readonly HcaChannel[] _channels = new HcaChannel[MaxChannels];
        private readonly byte[] _athCurve = new byte[SamplesPerSubframe];
        private readonly byte[] _cipherTable = new byte[256];
        private readonly byte[] _frameBuffer;
        private uint _random = 1;

        private int _version;
        private int _headerSize;
        private int _channelCount;
        private int _sampleRate;
        private int _frameCount;
        private int _encoderDelay;
        private int _encoderPadding;
        private int _frameSize;
        private int _minResolution;
        private int _maxResolution;
        private int _trackCount;
        private int _channelConfig;
        private int _stereoType;
        private int _totalBandCount;
        private int _baseBandCount;
        private int _stereoBandCount;
        private int _bandsPerHfrGroup;
        private int _msStereo;
        private int _athType;
        private int _loopFlag;
        private int _loopStartFrame;
        private int _loopEndFrame;
        private int _loopStartDelay;
        private int _loopEndPadding;
        private int _ciphType;
        private ulong _keyCode;
        private int _hfrGroupCount;
        private float _rvaVolume = 1.0f;

        private HcaDecoder(ReadOnlySpan<byte> data, ulong keyCode)
        {
            for (int i = 0; i < _channels.Length; i++)
                _channels[i] = new HcaChannel();

            _keyCode = keyCode;
            int headerSize = ProbeHeaderSize(data);
            DecodeHeader(data.Slice(0, headerSize));
            _frameBuffer = new byte[_frameSize];
        }

        public int SampleRate => _sampleRate;

        public int Channels => _channelCount;

        public int SampleCount => Math.Max(0, _frameCount * SamplesPerFrame - _encoderDelay - _encoderPadding);

        public int LoopStartSample => _loopFlag == 0 ? 0 : _loopStartFrame * SamplesPerFrame - _encoderDelay + _loopStartDelay;

        public int LoopEndSample => _loopFlag == 0 ? 0 : _loopEndFrame * SamplesPerFrame - _encoderDelay + (SamplesPerFrame - _loopEndPadding);

        public bool EncryptionEnabled => _ciphType == 56;

        public static AudioData Decode(byte[] data, int offset, int size, ulong keyCode = 0, ushort subKey = 0)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (offset < 0 || size < 0 || offset > data.Length || data.Length - offset < size)
                throw new ArgumentOutOfRangeException(nameof(offset));

            ReadOnlySpan<byte> hcaData = new ReadOnlySpan<byte>(data, offset, size);
            ulong effectiveKey = ApplySubKey(keyCode, subKey);
            var decoder = new HcaDecoder(hcaData, effectiveKey);
            if (decoder.EncryptionEnabled && effectiveKey == 0)
                throw new AcbException("This HCA is encrypted. Set DecodeOptions.HcaKey before decoding.");

            int maxFramesByData = Math.Max(0, (size - decoder._headerSize) / decoder._frameSize);
            int frameCount = Math.Min(decoder._frameCount, maxFramesByData);
            int sampleCount = Math.Max(0, frameCount * SamplesPerFrame - decoder._encoderDelay - decoder._encoderPadding);
            var output = new float[sampleCount * decoder._channelCount];

            int outSampleBase = -decoder._encoderDelay;
            for (int frame = 0; frame < frameCount; frame++)
            {
                int frameOffset = decoder._headerSize + frame * decoder._frameSize;
                hcaData.Slice(frameOffset, decoder._frameSize).CopyTo(decoder._frameBuffer);
                decoder.DecodeBlock(decoder._frameBuffer);
                decoder.CopyFrameToOutput(output, outSampleBase);
                outSampleBase += SamplesPerFrame;
            }

            int loopStart = Math.Max(0, Math.Min(sampleCount, decoder.LoopStartSample));
            int loopEnd = Math.Max(0, Math.Min(sampleCount, decoder.LoopEndSample));
            return new AudioData(output, decoder._sampleRate, decoder._channelCount, loopStart, loopEnd);
        }

        public static bool IsHca(ReadOnlySpan<byte> data)
        {
            return data.Length >= 4 && ((int)BinaryUtil.U32BE(data, 0) & HcaMask) == 0x48434100;
        }

        private static ulong ApplySubKey(ulong keyCode, ushort subKey)
        {
            if (keyCode == 0 || subKey == 0)
                return keyCode;
            ulong seed = ((ulong)subKey << 16) | (ushort)(~subKey + 2);
            return keyCode * seed;
        }

        private static int ProbeHeaderSize(ReadOnlySpan<byte> data)
        {
            if (data.Length < 8)
                throw new AcbException("HCA header is truncated.");
            var br = new BitReader(data.Slice(0, 8));
            if (((int)br.Peek(32) & HcaMask) != 0x48434100)
                throw new AcbException("Input is not an HCA stream.");
            br.Skip(32 + 16);
            int headerSize = (int)br.Read(16);
            if (headerSize <= 0 || headerSize > data.Length)
                throw new AcbException("HCA header size is invalid.");
            return headerSize;
        }

        private void DecodeHeader(ReadOnlySpan<byte> data)
        {
            var br = new BitReader(data);
            int sizeLeft = data.Length;

            if (((int)br.Peek(32) & HcaMask) != 0x48434100)
                throw new AcbException("Missing HCA header.");
            br.Skip(32);
            _version = (int)br.Read(16);
            _headerSize = (int)br.Read(16);
            if (_version != HcaVersion101 && _version != HcaVersion102 && _version != HcaVersion103 && _version != HcaVersion200 && _version != HcaVersion300)
                throw new AcbException($"Unsupported HCA version 0x{_version:X4}.");
            if (_headerSize != data.Length)
                throw new AcbException("HCA header read size does not match header size.");
            if (Crc16(data) != 0)
                throw new AcbException("HCA header checksum failed.");
            sizeLeft -= 0x08;

            if (sizeLeft < 0x10 || ((int)br.Peek(32) & HcaMask) != 0x666D7400)
                throw new AcbException("Missing HCA fmt chunk.");
            br.Skip(32);
            _channelCount = (int)br.Read(8);
            _sampleRate = (int)br.Read(24);
            _frameCount = unchecked((int)br.Read(32));
            _encoderDelay = (int)br.Read(16);
            _encoderPadding = (int)br.Read(16);
            if (_channelCount < 1 || _channelCount > MaxChannels || _frameCount <= 0 || _sampleRate <= 0)
                throw new AcbException("HCA fmt values are invalid.");
            sizeLeft -= 0x10;

            if (sizeLeft >= 0x10 && ((int)br.Peek(32) & HcaMask) == 0x636F6D70)
            {
                br.Skip(32);
                _frameSize = (int)br.Read(16);
                _minResolution = (int)br.Read(8);
                _maxResolution = (int)br.Read(8);
                _trackCount = (int)br.Read(8);
                _channelConfig = (int)br.Read(8);
                _totalBandCount = (int)br.Read(8);
                _baseBandCount = (int)br.Read(8);
                _stereoBandCount = (int)br.Read(8);
                _bandsPerHfrGroup = (int)br.Read(8);
                _msStereo = (int)br.Read(8);
                br.Read(8);
                sizeLeft -= 0x10;
            }
            else if (sizeLeft >= 0x0C && ((int)br.Peek(32) & HcaMask) == 0x64656300)
            {
                br.Skip(32);
                _frameSize = (int)br.Read(16);
                _minResolution = (int)br.Read(8);
                _maxResolution = (int)br.Read(8);
                _totalBandCount = (int)br.Read(8) + 1;
                _baseBandCount = (int)br.Read(8) + 1;
                _trackCount = (int)br.Read(4);
                _channelConfig = (int)br.Read(4);
                _stereoType = (int)br.Read(8);
                if (_stereoType == 0)
                    _baseBandCount = _totalBandCount;
                _stereoBandCount = _totalBandCount - _baseBandCount;
                _bandsPerHfrGroup = 0;
                sizeLeft -= 0x0C;
            }
            else
            {
                throw new AcbException("Missing HCA comp/dec chunk.");
            }

            if (sizeLeft >= 0x08 && ((int)br.Peek(32) & HcaMask) == 0x76627200)
            {
                br.Skip(32);
                int vbrMaxFrameSize = (int)br.Read(16);
                br.Read(16);
                if (!(_frameSize == 0 && vbrMaxFrameSize > 8 && vbrMaxFrameSize <= 0x1FF))
                    throw new AcbException("Unsupported HCA VBR header.");
                sizeLeft -= 0x08;
            }

            if (sizeLeft >= 0x06 && ((int)br.Peek(32) & HcaMask) == 0x61746800)
            {
                br.Skip(32);
                _athType = (int)br.Read(16);
                sizeLeft -= 0x06;
            }
            else
            {
                _athType = _version < HcaVersion200 ? 1 : 0;
            }

            if (sizeLeft >= 0x10 && ((int)br.Peek(32) & HcaMask) == 0x6C6F6F70)
            {
                br.Skip(32);
                _loopStartFrame = unchecked((int)br.Read(32));
                _loopEndFrame = unchecked((int)br.Read(32));
                _loopStartDelay = (int)br.Read(16);
                _loopEndPadding = (int)br.Read(16);
                _loopFlag = 1;
                if (_loopStartFrame < 0 || _loopStartFrame > _loopEndFrame || _loopEndFrame >= _frameCount)
                    throw new AcbException("HCA loop values are invalid.");
                sizeLeft -= 0x10;
            }

            if (sizeLeft >= 0x06 && ((int)br.Peek(32) & HcaMask) == 0x63697068)
            {
                br.Skip(32);
                _ciphType = (int)br.Read(16);
                if (_ciphType != 0 && _ciphType != 1 && _ciphType != 56)
                    throw new AcbException($"Unsupported HCA cipher type {_ciphType}.");
                sizeLeft -= 0x06;
            }

            if (sizeLeft >= 0x08 && ((int)br.Peek(32) & HcaMask) == 0x72766100)
            {
                br.Skip(32);
                _rvaVolume = BinaryUtil.FloatFromBits(br.Read(32));
                sizeLeft -= 0x08;
            }

            if (sizeLeft >= 0x05 && ((int)br.Peek(32) & HcaMask) == 0x636F6D6D)
            {
                br.Skip(32);
                int commentLength = (int)br.Read(8);
                for (int i = 0; i < commentLength; i++)
                    br.Read(8);
                sizeLeft -= 0x05 + commentLength;
            }

            ValidateAndInitialize();
        }

        private void ValidateAndInitialize()
        {
            if (_frameSize < 8 || _frameSize > 0xFFFF)
                throw new AcbException("HCA frame size is invalid.");
            if (_version <= HcaVersion200)
            {
                if (_minResolution != 1 || _maxResolution != 15)
                    throw new AcbException("Unsupported HCA resolution range.");
            }
            else if (_minResolution > _maxResolution || _maxResolution > 15)
            {
                throw new AcbException("Unsupported HCA resolution range.");
            }

            if (_trackCount == 0)
                _trackCount = 1;
            if (_trackCount > _channelCount)
                throw new AcbException("HCA track count is invalid.");
            if (_totalBandCount > SamplesPerSubframe || _baseBandCount > SamplesPerSubframe ||
                _stereoBandCount > SamplesPerSubframe || _baseBandCount + _stereoBandCount > SamplesPerSubframe ||
                _bandsPerHfrGroup > SamplesPerSubframe)
                throw new AcbException("HCA band counts are invalid.");

            _hfrGroupCount = HeaderCeil2(_totalBandCount - _baseBandCount - _stereoBandCount, _bandsPerHfrGroup);
            InitializeAth();
            InitializeCipher();
            InitializeChannels();

            if (_msStereo != 0)
                throw new AcbException("HCA mid/side stereo is not implemented in this C# decoder.");
        }

        private static int HeaderCeil2(int a, int b)
        {
            if (b < 1)
                return 0;
            return a / b + (a % b != 0 ? 1 : 0);
        }

        private void InitializeAth()
        {
            Array.Clear(_athCurve, 0, _athCurve.Length);
            if (_athType == 0)
                return;
            throw new AcbException("HCA ATH type 1 is not implemented. Modern ADX2/HCA files normally use ATH type 0.");
        }

        private void InitializeChannels()
        {
            ChannelType[] channelTypes = new ChannelType[MaxChannels];
            int channelsPerTrack = _channelCount / _trackCount;
            if (_stereoBandCount > 0 && channelsPerTrack > 1)
            {
                for (int track = 0; track < _trackCount; track++)
                {
                    int idx = track * channelsPerTrack;
                    switch (channelsPerTrack)
                    {
                        case 2:
                            channelTypes[idx] = ChannelType.StereoPrimary;
                            channelTypes[idx + 1] = ChannelType.StereoSecondary;
                            break;
                        case 3:
                            channelTypes[idx] = ChannelType.StereoPrimary;
                            channelTypes[idx + 1] = ChannelType.StereoSecondary;
                            break;
                        case 4:
                            channelTypes[idx] = ChannelType.StereoPrimary;
                            channelTypes[idx + 1] = ChannelType.StereoSecondary;
                            if (_channelConfig == 0)
                            {
                                channelTypes[idx + 2] = ChannelType.StereoPrimary;
                                channelTypes[idx + 3] = ChannelType.StereoSecondary;
                            }
                            break;
                        case 5:
                            channelTypes[idx] = ChannelType.StereoPrimary;
                            channelTypes[idx + 1] = ChannelType.StereoSecondary;
                            if (_channelConfig <= 2)
                            {
                                channelTypes[idx + 3] = ChannelType.StereoPrimary;
                                channelTypes[idx + 4] = ChannelType.StereoSecondary;
                            }
                            break;
                        case 6:
                            channelTypes[idx] = ChannelType.StereoPrimary;
                            channelTypes[idx + 1] = ChannelType.StereoSecondary;
                            channelTypes[idx + 4] = ChannelType.StereoPrimary;
                            channelTypes[idx + 5] = ChannelType.StereoSecondary;
                            break;
                        case 7:
                            channelTypes[idx] = ChannelType.StereoPrimary;
                            channelTypes[idx + 1] = ChannelType.StereoSecondary;
                            channelTypes[idx + 4] = ChannelType.StereoPrimary;
                            channelTypes[idx + 5] = ChannelType.StereoSecondary;
                            break;
                        case 8:
                            channelTypes[idx] = ChannelType.StereoPrimary;
                            channelTypes[idx + 1] = ChannelType.StereoSecondary;
                            channelTypes[idx + 4] = ChannelType.StereoPrimary;
                            channelTypes[idx + 5] = ChannelType.StereoSecondary;
                            channelTypes[idx + 6] = ChannelType.StereoPrimary;
                            channelTypes[idx + 7] = ChannelType.StereoSecondary;
                            break;
                    }
                }
            }

            for (int i = 0; i < _channelCount; i++)
            {
                HcaChannel ch = _channels[i];
                ch.Type = channelTypes[i];
                ch.CodedCount = ch.Type != ChannelType.StereoSecondary
                    ? _baseBandCount + _stereoBandCount
                    : _baseBandCount;
            }
        }

        private void InitializeCipher()
        {
            if (_ciphType == 56 && _keyCode == 0)
            {
                InitCipher0(_cipherTable);
                return;
            }

            switch (_ciphType)
            {
                case 0:
                    InitCipher0(_cipherTable);
                    break;
                case 1:
                    InitCipher1(_cipherTable);
                    break;
                case 56:
                    InitCipher56(_cipherTable, _keyCode);
                    break;
            }
        }

        private void DecodeBlock(byte[] block)
        {
            var br = new BitReader(block);
            int sync = (int)br.Read(16);
            if (sync != 0xFFFF)
                throw new AcbException("HCA frame sync failed.");
            if (Crc16(block) != 0)
                throw new AcbException("HCA frame checksum failed.");

            Decrypt(block, _cipherTable, _frameSize);

            int acceptableNoiseLevel = (int)br.Read(9);
            int evaluationBoundary = (int)br.Read(7);
            int packedNoiseLevel = (acceptableNoiseLevel << 8) - evaluationBoundary;

            for (int ch = 0; ch < _channelCount; ch++)
            {
                UnpackScaleFactors(_channels[ch], ref br);
                UnpackIntensity(_channels[ch], ref br);
                CalculateResolution(_channels[ch], packedNoiseLevel);
                CalculateGain(_channels[ch]);
            }

            for (int subframe = 0; subframe < Subframes; subframe++)
            {
                for (int ch = 0; ch < _channelCount; ch++)
                    DequantizeCoefficients(_channels[ch], ref br, subframe);
            }

            TransformBlock();
        }

        private void TransformBlock()
        {
            for (int subframe = 0; subframe < Subframes; subframe++)
            {
                for (int ch = 0; ch < _channelCount; ch++)
                {
                    ReconstructNoise(_channels[ch], subframe);
                    ReconstructHighFrequency(_channels[ch], subframe);
                }

                if (_stereoBandCount > 0)
                {
                    for (int ch = 0; ch < _channelCount - 1; ch++)
                    {
                        ApplyIntensityStereo(_channels[ch], _channels[ch + 1], subframe);
                    }
                }

                for (int ch = 0; ch < _channelCount; ch++)
                    ImdctTransform(_channels[ch], subframe);
            }
        }

        private void CopyFrameToOutput(float[] output, int firstSampleIndex)
        {
            int outSamples = output.Length / _channelCount;
            for (int subframe = 0; subframe < Subframes; subframe++)
            {
                int waveBase = subframe * SamplesPerSubframe;
                for (int s = 0; s < SamplesPerSubframe; s++)
                {
                    int sampleIndex = firstSampleIndex + subframe * SamplesPerSubframe + s;
                    if (sampleIndex < 0 || sampleIndex >= outSamples)
                        continue;

                    int dst = sampleIndex * _channelCount;
                    int src = waveBase + s;
                    for (int ch = 0; ch < _channelCount; ch++)
                        output[dst + ch] = _channels[ch].Wave[src] * _rvaVolume;
                }
            }
        }

        private void UnpackScaleFactors(HcaChannel ch, ref BitReader br)
        {
            int csCount = ch.CodedCount;
            int extraCount;
            int deltaBits = (int)br.Read(3);

            if (ch.Type == ChannelType.StereoSecondary || _hfrGroupCount <= 0 || _version <= HcaVersion200)
            {
                extraCount = 0;
            }
            else
            {
                extraCount = _hfrGroupCount;
                csCount += extraCount;
                if (csCount > SamplesPerSubframe)
                    throw new AcbException("HCA scalefactor count is invalid.");
            }

            if (deltaBits >= 6)
            {
                for (int i = 0; i < csCount; i++)
                    ch.ScaleFactors[i] = (byte)br.Read(6);
            }
            else if (deltaBits > 0)
            {
                int expectedDelta = (1 << deltaBits) - 1;
                int value = (int)br.Read(6);
                ch.ScaleFactors[0] = (byte)value;
                for (int i = 1; i < csCount; i++)
                {
                    int delta = (int)br.Read(deltaBits);
                    if (delta == expectedDelta)
                    {
                        value = (int)br.Read(6);
                    }
                    else
                    {
                        int test = value + (delta - (expectedDelta >> 1));
                        if (test < 0 || test >= 64)
                            throw new AcbException("HCA scalefactor delta is invalid.");
                        value = (value - (expectedDelta >> 1) + delta) & 0x3F;
                    }

                    ch.ScaleFactors[i] = (byte)value;
                }
            }
            else
            {
                Array.Clear(ch.ScaleFactors, 0, ch.ScaleFactors.Length);
            }

            for (int i = 0; i < extraCount; i++)
                ch.ScaleFactors[SamplesPerSubframe - 1 - i] = ch.ScaleFactors[csCount - i];
        }

        private void UnpackIntensity(HcaChannel ch, ref BitReader br)
        {
            if (ch.Type == ChannelType.StereoSecondary)
            {
                if (_version <= HcaVersion200)
                {
                    int value = (int)br.Peek(4);
                    ch.Intensity[0] = (byte)value;
                    if (value < 15)
                    {
                        br.Skip(4);
                        for (int i = 1; i < Subframes; i++)
                            ch.Intensity[i] = (byte)br.Read(4);
                    }
                }
                else
                {
                    int value = (int)br.Peek(4);
                    if (value < 15)
                    {
                        br.Skip(4);
                        int deltaBits = (int)br.Read(2);
                        ch.Intensity[0] = (byte)value;
                        if (deltaBits == 3)
                        {
                            for (int i = 1; i < Subframes; i++)
                                ch.Intensity[i] = (byte)br.Read(4);
                        }
                        else
                        {
                            int bmax = (2 << deltaBits) - 1;
                            int bits = deltaBits + 1;
                            for (int i = 1; i < Subframes; i++)
                            {
                                int delta = (int)br.Read(bits);
                                if (delta == bmax)
                                {
                                    value = (int)br.Read(4);
                                }
                                else
                                {
                                    value = value - (bmax >> 1) + delta;
                                    if (value > 15)
                                        throw new AcbException("HCA intensity delta is invalid.");
                                }

                                ch.Intensity[i] = (byte)value;
                            }
                        }
                    }
                    else
                    {
                        br.Skip(4);
                        for (int i = 0; i < Subframes; i++)
                            ch.Intensity[i] = 7;
                    }
                }
            }
            else if (_version <= HcaVersion200)
            {
                int hfrOffset = SamplesPerSubframe - _hfrGroupCount;
                for (int i = 0; i < _hfrGroupCount; i++)
                    ch.ScaleFactors[hfrOffset + i] = (byte)br.Read(6);
            }
        }

        private void CalculateResolution(HcaChannel ch, int packedNoiseLevel)
        {
            int crCount = ch.CodedCount;
            int noiseCount = 0;
            int validCount = 0;

            for (int i = 0; i < crCount; i++)
            {
                byte newResolution = 0;
                byte scalefactor = ch.ScaleFactors[i];
                if (scalefactor > 0)
                {
                    int noiseLevel = _athCurve[i] + ((packedNoiseLevel + i) >> 8);
                    int curvePosition = noiseLevel + 1 - ((5 * scalefactor) >> 1);
                    if (curvePosition < 0)
                        newResolution = 15;
                    else if (curvePosition <= 65)
                        newResolution = InvertTable[curvePosition];
                    else
                        newResolution = 0;

                    if (newResolution > _maxResolution)
                        newResolution = (byte)_maxResolution;
                    else if (newResolution < _minResolution)
                        newResolution = (byte)_minResolution;

                    if (newResolution < 1)
                        ch.Noises[noiseCount++] = (byte)i;
                    else
                        ch.Noises[SamplesPerSubframe - 1 - validCount++] = (byte)i;
                }

                ch.Resolution[i] = newResolution;
            }

            ch.NoiseCount = noiseCount;
            ch.ValidCount = validCount;
            Array.Clear(ch.Resolution, crCount, SamplesPerSubframe - crCount);
        }

        private static void CalculateGain(HcaChannel ch)
        {
            for (int i = 0; i < ch.CodedCount; i++)
            {
                float scalefactorScale = DequantizerScalingTable[ch.ScaleFactors[i]];
                float resolutionScale = DequantizerRangeTable[ch.Resolution[i]];
                ch.Gain[i] = scalefactorScale * resolutionScale;
            }
        }

        private static void DequantizeCoefficients(HcaChannel ch, ref BitReader br, int subframe)
        {
            int baseIndex = subframe * SamplesPerSubframe;
            for (int i = 0; i < ch.CodedCount; i++)
            {
                int resolution = ch.Resolution[i];
                int bits = MaxBitTable[resolution];
                uint code = br.Read(bits);
                float qc;
                if (resolution > 7)
                {
                    int signedCode = (1 - ((int)(code & 1) << 1)) * ((int)code >> 1);
                    if (signedCode == 0)
                        br.Skip(-1);
                    qc = signedCode;
                }
                else
                {
                    int index = (resolution << 4) + (int)code;
                    int skip = ReadBitTable[index] - bits;
                    br.Skip(skip);
                    qc = ReadValueTable[index];
                }

                ch.Spectra[baseIndex + i] = ch.Gain[i] * qc;
            }

            Array.Clear(ch.Spectra, baseIndex + ch.CodedCount, SamplesPerSubframe - ch.CodedCount);
        }

        private void ReconstructNoise(HcaChannel ch, int subframe)
        {
            if (_minResolution > 0 || ch.ValidCount <= 0 || ch.NoiseCount <= 0)
                return;
            if (_msStereo != 0 && ch.Type != ChannelType.StereoPrimary)
                return;

            int baseIndex = subframe * SamplesPerSubframe;
            uint random = _random;
            for (int i = 0; i < ch.NoiseCount; i++)
            {
                random = 0x343FDu * random + 0x269EC3u;
                int randomIndex = SamplesPerSubframe - ch.ValidCount + (int)(((random & 0x7FFF) * ch.ValidCount) >> 15);
                int noiseIndex = ch.Noises[i];
                int validIndex = ch.Noises[randomIndex];
                int sfNoise = ch.ScaleFactors[noiseIndex];
                int sfValid = ch.ScaleFactors[validIndex];
                int scIndex = sfNoise - sfValid + 62;
                if (scIndex < 0)
                    scIndex = 0;
                if (scIndex >= ScaleConversionTable.Length)
                    scIndex = ScaleConversionTable.Length - 1;
                ch.Spectra[baseIndex + noiseIndex] = ScaleConversionTable[scIndex] * ch.Spectra[baseIndex + validIndex];
            }

            _random = random;
        }

        private void ReconstructHighFrequency(HcaChannel ch, int subframe)
        {
            if (_bandsPerHfrGroup == 0 || ch.Type == ChannelType.StereoSecondary)
                return;

            int baseIndex = subframe * SamplesPerSubframe;
            int startBand = _stereoBandCount + _baseBandCount;
            int highBand = startBand;
            int lowBand = startBand - 1;
            int hfrOffset = SamplesPerSubframe - _hfrGroupCount;
            int groupLimit = _version <= HcaVersion200 ? _hfrGroupCount : _hfrGroupCount >> 1;

            for (int group = 0; group < _hfrGroupCount; group++)
            {
                int lowBandSub = group < groupLimit ? 1 : 0;
                for (int i = 0; i < _bandsPerHfrGroup; i++)
                {
                    if (highBand >= _totalBandCount || lowBand < 0)
                        break;

                    int scIndex = ch.ScaleFactors[hfrOffset + group] - ch.ScaleFactors[lowBand] + 63;
                    if (scIndex < 0)
                        scIndex = 0;
                    if (scIndex >= ScaleConversionTable.Length)
                        scIndex = ScaleConversionTable.Length - 1;
                    ch.Spectra[baseIndex + highBand] = ScaleConversionTable[scIndex] * ch.Spectra[baseIndex + lowBand];
                    highBand++;
                    lowBand -= lowBandSub;
                }
            }

            if (highBand > 0 && highBand <= SamplesPerSubframe)
                ch.Spectra[baseIndex + highBand - 1] = 0.0f;
        }

        private void ApplyIntensityStereo(HcaChannel left, HcaChannel right, int subframe)
        {
            if (left.Type != ChannelType.StereoPrimary)
                return;
            int baseIndex = subframe * SamplesPerSubframe;
            float ratioL = IntensityRatioTable[right.Intensity[subframe]];
            float ratioR = 2.0f - ratioL;
            for (int band = _baseBandCount; band < _totalBandCount; band++)
            {
                float coef = left.Spectra[baseIndex + band];
                left.Spectra[baseIndex + band] = coef * ratioL;
                right.Spectra[baseIndex + band] = coef * ratioR;
            }
        }

        private static void ImdctTransform(HcaChannel ch, int subframe)
        {
            int spectraBase = subframe * SamplesPerSubframe;
            for (int k = 0; k < SamplesPerSubframe; k++)
            {
                float sum = 0;
                int matrixBase = k * SamplesPerSubframe;
                for (int n = 0; n < SamplesPerSubframe; n++)
                    sum += ch.Spectra[spectraBase + n] * Dct4Matrix[matrixBase + n];
                ch.Temp[k] = sum;
            }

            int waveBase = subframe * SamplesPerSubframe;
            for (int i = 0; i < 64; i++)
            {
                ch.Wave[waveBase + i] = ImdctWindow[i] * ch.Temp[i + 64] + ch.ImdctPrevious[i];
                ch.Wave[waveBase + i + 64] = ImdctWindow[i + 64] * ch.Temp[SamplesPerSubframe - 1 - i] - ch.ImdctPrevious[i + 64];
                ch.ImdctPrevious[i] = ImdctWindow[SamplesPerSubframe - 1 - i] * ch.Temp[64 - i - 1];
                ch.ImdctPrevious[i + 64] = ImdctWindow[64 - i - 1] * ch.Temp[i];
            }
        }

        private static ushort Crc16(ReadOnlySpan<byte> data)
        {
            ushort sum = 0;
            for (int i = 0; i < data.Length; i++)
                sum = (ushort)((sum << 8) ^ CrcTable[((sum >> 8) ^ data[i]) & 0xFF]);
            return sum;
        }

        private static ushort[] BuildCrcTable()
        {
            var table = new ushort[256];
            for (int i = 0; i < table.Length; i++)
            {
                int value = i << 8;
                for (int bit = 0; bit < 8; bit++)
                {
                    if ((value & 0x8000) != 0)
                        value = ((value << 1) ^ 0x8005) & 0xFFFF;
                    else
                        value = (value << 1) & 0xFFFF;
                }

                table[i] = (ushort)value;
            }

            return table;
        }

        private static void InitCipher0(byte[] table)
        {
            for (int i = 0; i < 256; i++)
                table[i] = (byte)i;
        }

        private static void InitCipher1(byte[] table)
        {
            const int mul = 13;
            const int add = 11;
            int v = 0;
            for (int i = 1; i < 255; i++)
            {
                v = (v * mul + add) & 0xFF;
                if (v == 0 || v == 0xFF)
                    v = (v * mul + add) & 0xFF;
                table[i] = (byte)v;
            }

            table[0] = 0;
            table[0xFF] = 0xFF;
        }

        private static void InitCipher56(byte[] table, ulong keyCode)
        {
            if (keyCode != 0)
                keyCode--;

            byte[] kc = new byte[8];
            for (int r = 0; r < 7; r++)
            {
                kc[r] = (byte)(keyCode & 0xFF);
                keyCode >>= 8;
            }

            byte[] seed = new byte[16];
            seed[0x00] = kc[1];
            seed[0x01] = (byte)(kc[1] ^ kc[6]);
            seed[0x02] = (byte)(kc[2] ^ kc[3]);
            seed[0x03] = kc[2];
            seed[0x04] = (byte)(kc[2] ^ kc[1]);
            seed[0x05] = (byte)(kc[3] ^ kc[4]);
            seed[0x06] = kc[3];
            seed[0x07] = (byte)(kc[3] ^ kc[2]);
            seed[0x08] = (byte)(kc[4] ^ kc[5]);
            seed[0x09] = kc[4];
            seed[0x0A] = (byte)(kc[4] ^ kc[3]);
            seed[0x0B] = (byte)(kc[5] ^ kc[6]);
            seed[0x0C] = kc[5];
            seed[0x0D] = (byte)(kc[5] ^ kc[4]);
            seed[0x0E] = (byte)(kc[6] ^ kc[1]);
            seed[0x0F] = kc[6];

            byte[] baseTable = new byte[256];
            byte[] baseR = new byte[16];
            byte[] baseC = new byte[16];
            InitCipher56CreateTable(baseR, kc[0]);
            for (int r = 0; r < 16; r++)
            {
                InitCipher56CreateTable(baseC, seed[r]);
                int nb = baseR[r] << 4;
                for (int c = 0; c < 16; c++)
                    baseTable[r * 16 + c] = (byte)(nb | baseC[c]);
            }

            int x = 0;
            int pos = 1;
            for (int i = 0; i < 256; i++)
            {
                x = (x + 17) & 0xFF;
                if (baseTable[x] != 0 && baseTable[x] != 0xFF)
                    table[pos++] = baseTable[x];
            }

            table[0] = 0;
            table[0xFF] = 0xFF;
        }

        private static void InitCipher56CreateTable(byte[] output, byte key)
        {
            int mul = ((key & 1) << 3) | 5;
            int add = (key & 0xE) | 1;
            int value = key >> 4;
            for (int i = 0; i < 16; i++)
            {
                value = (value * mul + add) & 0xF;
                output[i] = (byte)value;
            }
        }

        private static void Decrypt(byte[] data, byte[] table, int size)
        {
            for (int i = 0; i < size; i++)
                data[i] = table[data[i]];
        }

        private static float[] BuildDequantizerScalingTable()
        {
            var table = new float[64];
            double baseValue = Math.Pow(2.0, 53.0 / 128.0);
            double sqrt128 = Math.Sqrt(128.0);
            for (int i = 0; i < table.Length; i++)
                table[i] = (float)(sqrt128 * Math.Pow(baseValue, i - 63));
            return table;
        }

        private static float[] BuildScaleConversionTable()
        {
            var table = new float[128];
            double baseValue = Math.Pow(2.0, 53.0 / 128.0);
            for (int i = 1; i < 126; i++)
                table[i] = (float)Math.Pow(baseValue, i - 63);
            return table;
        }

        private static float[] BuildDct4Matrix()
        {
            var matrix = new float[SamplesPerSubframe * SamplesPerSubframe];
            const double scale = 0.125;
            for (int k = 0; k < SamplesPerSubframe; k++)
            {
                for (int n = 0; n < SamplesPerSubframe; n++)
                {
                    matrix[k * SamplesPerSubframe + n] =
                        (float)(Math.Cos(Math.PI / SamplesPerSubframe * (n + 0.5) * (k + 0.5)) * scale);
                }
            }

            return matrix;
        }

        private static float[] Floats(params uint[] bits)
        {
            var values = new float[bits.Length];
            for (int i = 0; i < bits.Length; i++)
                values[i] = BinaryUtil.FloatFromBits(bits[i]);
            return values;
        }

        private static readonly ushort[] CrcTable = BuildCrcTable();
        private static readonly float[] Dct4Matrix = BuildDct4Matrix();
        private static readonly float[] DequantizerScalingTable = BuildDequantizerScalingTable();
        private static readonly float[] ScaleConversionTable = BuildScaleConversionTable();

        private static readonly byte[] InvertTable =
        {
            14,14,14,14,14,14,13,13, 13,13,13,13,12,12,12,12,
            12,12,11,11,11,11,11,11, 10,10,10,10,10,10,10, 9,
             9, 9, 9, 9, 9, 8, 8, 8,  8, 8, 8, 7, 6, 6, 5, 4,
             4, 4, 3, 3, 3, 2, 2, 2,  2, 1, 1, 1, 1, 1, 1, 1,
             1, 1,
        };

        private static readonly float[] DequantizerRangeTable =
        {
            1.0f, 0.6666666667f, 0.4f, 0.2857142857f,
            0.2222222222f, 0.1818181818f, 0.1538461538f, 0.1333333333f,
            0.0645161290f, 0.0317460317f, 0.0157480315f, 0.0078431373f,
            0.0039138943f, 0.0019550342f, 0.0009770396f, 0.0004884005f,
        };

        private static readonly byte[] MaxBitTable =
        {
            0,2,3,3,4,4,4,4, 5,6,7,8,9,10,11,12
        };

        private static readonly byte[] ReadBitTable =
        {
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,2,2,0,0,0,0, 0,0,0,0,0,0,0,0,
            2,2,2,2,2,2,3,3, 0,0,0,0,0,0,0,0,
            2,2,3,3,3,3,3,3, 0,0,0,0,0,0,0,0,
            3,3,3,3,3,3,3,3, 3,3,3,3,3,3,4,4,
            3,3,3,3,3,3,3,3, 3,3,4,4,4,4,4,4,
            3,3,3,3,3,3,4,4, 4,4,4,4,4,4,4,4,
            3,3,4,4,4,4,4,4, 4,4,4,4,4,4,4,4,
        };

        private static readonly float[] ReadValueTable =
        {
            +0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f, +0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f,
            +0.0f,+0.0f,+1.0f,-1.0f,+0.0f,+0.0f,+0.0f,+0.0f, +0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f,
            +0.0f,+0.0f,+1.0f,+1.0f,-1.0f,-1.0f,+2.0f,-2.0f, +0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f,
            +0.0f,+0.0f,+1.0f,-1.0f,+2.0f,-2.0f,+3.0f,-3.0f, +0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f,+0.0f,
            +0.0f,+0.0f,+1.0f,+1.0f,-1.0f,-1.0f,+2.0f,+2.0f, -2.0f,-2.0f,+3.0f,+3.0f,-3.0f,-3.0f,+4.0f,-4.0f,
            +0.0f,+0.0f,+1.0f,+1.0f,-1.0f,-1.0f,+2.0f,+2.0f, -2.0f,-2.0f,+3.0f,-3.0f,+4.0f,-4.0f,+5.0f,-5.0f,
            +0.0f,+0.0f,+1.0f,+1.0f,-1.0f,-1.0f,+2.0f,-2.0f, +3.0f,-3.0f,+4.0f,-4.0f,+5.0f,-5.0f,+6.0f,-6.0f,
            +0.0f,+0.0f,+1.0f,-1.0f,+2.0f,-2.0f,+3.0f,-3.0f, +4.0f,-4.0f,+5.0f,-5.0f,+6.0f,-6.0f,+7.0f,-7.0f,
        };

        private static readonly float[] IntensityRatioTable =
        {
            2.0f, 13.0f / 7.0f, 12.0f / 7.0f, 11.0f / 7.0f,
            10.0f / 7.0f, 9.0f / 7.0f, 8.0f / 7.0f, 1.0f,
            6.0f / 7.0f, 5.0f / 7.0f, 4.0f / 7.0f, 3.0f / 7.0f,
            2.0f / 7.0f, 1.0f / 7.0f, 0.0f, 0.0f,
        };

        private static readonly float[] ImdctWindow = Floats(
            0x3A3504F0,0x3B0183B8,0x3B70C538,0x3BBB9268,0x3C04A809,0x3C308200,0x3C61284C,0x3C8B3F17,
            0x3CA83992,0x3CC77FBD,0x3CE91110,0x3D0677CD,0x3D198FC4,0x3D2DD35C,0x3D434643,0x3D59ECC1,
            0x3D71CBA8,0x3D85741E,0x3D92A413,0x3DA078B4,0x3DAEF522,0x3DBE1C9E,0x3DCDF27B,0x3DDE7A1D,
            0x3DEFB6ED,0x3E00D62B,0x3E0A2EDA,0x3E13E72A,0x3E1E00B1,0x3E287CF2,0x3E335D55,0x3E3EA321,
            0x3E4A4F75,0x3E56633F,0x3E62DF37,0x3E6FC3D1,0x3E7D1138,0x3E8563A2,0x3E8C72B7,0x3E93B561,
            0x3E9B2AEF,0x3EA2D26F,0x3EAAAAAB,0x3EB2B222,0x3EBAE706,0x3EC34737,0x3ECBD03D,0x3ED47F46,
            0x3EDD5128,0x3EE6425C,0x3EEF4EFF,0x3EF872D7,0x3F00D4A9,0x3F0576CA,0x3F0A1D3B,0x3F0EC548,
            0x3F136C25,0x3F180EF2,0x3F1CAAC2,0x3F213CA2,0x3F25C1A5,0x3F2A36E7,0x3F2E9998,0x3F32E705,
            0xBF371C9E,0xBF3B37FE,0xBF3F36F2,0xBF431780,0xBF46D7E6,0xBF4A76A4,0xBF4DF27C,0xBF514A6F,
            0xBF547DC5,0xBF578C03,0xBF5A74EE,0xBF5D3887,0xBF5FD707,0xBF6250DA,0xBF64A699,0xBF66D908,
            0xBF68E90E,0xBF6AD7B1,0xBF6CA611,0xBF6E5562,0xBF6FE6E7,0xBF715BEF,0xBF72B5D1,0xBF73F5E6,
            0xBF751D89,0xBF762E13,0xBF7728D7,0xBF780F20,0xBF78E234,0xBF79A34C,0xBF7A5397,0xBF7AF439,
            0xBF7B8648,0xBF7C0ACE,0xBF7C82C8,0xBF7CEF26,0xBF7D50CB,0xBF7DA88E,0xBF7DF737,0xBF7E3D86,
            0xBF7E7C2A,0xBF7EB3CC,0xBF7EE507,0xBF7F106C,0xBF7F3683,0xBF7F57CA,0xBF7F74B6,0xBF7F8DB6,
            0xBF7FA32E,0xBF7FB57B,0xBF7FC4F6,0xBF7FD1ED,0xBF7FDCAD,0xBF7FE579,0xBF7FEC90,0xBF7FF22E,
            0xBF7FF688,0xBF7FF9D0,0xBF7FFC32,0xBF7FFDDA,0xBF7FFEED,0xBF7FFF8F,0xBF7FFFDF,0xBF7FFFFC);

        private enum ChannelType
        {
            Discrete = 0,
            StereoPrimary = 1,
            StereoSecondary = 2,
        }

        private sealed class HcaChannel
        {
            public ChannelType Type;
            public int CodedCount;
            public readonly byte[] Intensity = new byte[Subframes];
            public readonly byte[] ScaleFactors = new byte[SamplesPerSubframe];
            public readonly byte[] Resolution = new byte[SamplesPerSubframe];
            public readonly byte[] Noises = new byte[SamplesPerSubframe];
            public int NoiseCount;
            public int ValidCount;
            public readonly float[] Gain = new float[SamplesPerSubframe];
            public readonly float[] Spectra = new float[Subframes * SamplesPerSubframe];
            public readonly float[] Temp = new float[SamplesPerSubframe];
            public readonly float[] ImdctPrevious = new float[SamplesPerSubframe];
            public readonly float[] Wave = new float[Subframes * SamplesPerSubframe];
        }
    }
}
