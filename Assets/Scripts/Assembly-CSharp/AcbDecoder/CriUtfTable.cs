using System;
using System.Collections.Generic;

namespace AcbDecoder
{
    internal sealed class CriUtfTable
    {
        private const int ColumnBitmaskFlag = 0xF0;
        private const int ColumnBitmaskType = 0x0F;
        private const int ColumnFlagName = 0x10;
        private const int ColumnFlagDefault = 0x20;
        private const int ColumnFlagRow = 0x40;
        private const int ColumnFlagUndefined = 0x80;

        private readonly byte[] _data;
        private readonly Column[] _columns;
        private readonly Dictionary<string, int> _columnIndexes;
        private readonly int _tableOffset;
        private readonly int _schemaOffset;
        private readonly int _rowsOffset;
        private readonly int _stringsOffset;
        private readonly int _dataOffset;
        private readonly int _stringsSize;
        private readonly int _rowWidth;

        private CriUtfTable(
            byte[] data,
            int tableOffset,
            int tableSize,
            int rowsOffset,
            int stringsOffset,
            int dataOffset,
            int nameOffset,
            int rowWidth,
            int rowCount,
            Column[] columns,
            Dictionary<string, int> columnIndexes)
        {
            _data = data;
            _tableOffset = tableOffset;
            TableSize = tableSize;
            _schemaOffset = 0x20;
            _rowsOffset = rowsOffset;
            _stringsOffset = stringsOffset;
            _dataOffset = dataOffset;
            _stringsSize = dataOffset - stringsOffset;
            _rowWidth = rowWidth;
            RowCount = rowCount;
            _columns = columns;
            _columnIndexes = columnIndexes;
            Name = ReadString(nameOffset);
        }

        public string Name { get; }

        public int TableSize { get; }

        public int RowCount { get; }

        public static CriUtfTable Open(byte[] data, int tableOffset)
        {
            if ((uint)tableOffset > (uint)data.Length || data.Length - tableOffset < 0x20)
                throw new AcbException("@UTF table is outside the input buffer.");
            if (!BinaryUtil.HasId(data, tableOffset, "@UTF"))
                throw new AcbException("Expected @UTF table.");

            int tableSize = checked((int)BinaryUtil.U32BE(data, tableOffset + 0x04) + 0x08);
            int version = BinaryUtil.U16BE(data, tableOffset + 0x08);
            int rowsOffset = BinaryUtil.U16BE(data, tableOffset + 0x0A) + 0x08;
            int stringsOffset = checked((int)BinaryUtil.U32BE(data, tableOffset + 0x0C) + 0x08);
            int dataOffset = checked((int)BinaryUtil.U32BE(data, tableOffset + 0x10) + 0x08);
            int nameOffset = checked((int)BinaryUtil.U32BE(data, tableOffset + 0x14));
            int columnCount = BinaryUtil.U16BE(data, tableOffset + 0x18);
            int rowWidth = BinaryUtil.U16BE(data, tableOffset + 0x1A);
            int rowCount = checked((int)BinaryUtil.U32BE(data, tableOffset + 0x1C));

            if (version != 0 && version != 1)
                throw new AcbException($"Unsupported @UTF version {version}.");
            if (tableSize < 0x20 || tableOffset + tableSize > data.Length)
                throw new AcbException("@UTF table size is invalid.");
            if (rowsOffset > tableSize || stringsOffset > tableSize || dataOffset > tableSize)
                throw new AcbException("@UTF section offsets are invalid.");
            if (stringsOffset >= dataOffset)
                throw new AcbException("@UTF string table is empty or invalid.");
            if (columnCount <= 0)
                throw new AcbException("@UTF has no columns.");

            int schemaOffset = 0x20;
            int schemaPos = 0;
            int schemaSize = rowsOffset - schemaOffset;
            int columnOffset = 0;
            int stringsSize = dataOffset - stringsOffset;
            var columns = new Column[columnCount];
            var indexes = new Dictionary<string, int>(StringComparer.Ordinal);

            for (int i = 0; i < columnCount; i++)
            {
                int infoOffset = tableOffset + schemaOffset + schemaPos;
                if (schemaPos + 5 > schemaSize)
                    throw new AcbException("@UTF schema is truncated.");

                int info = data[infoOffset];
                int nameStringOffset = checked((int)BinaryUtil.U32BE(data, infoOffset + 1));
                int flag = info & ColumnBitmaskFlag;
                ColumnType type = (ColumnType)(info & ColumnBitmaskType);
                schemaPos += 5;

                if (nameStringOffset > stringsSize)
                    throw new AcbException("@UTF column name offset is invalid.");
                if (flag == 0 || (flag & ColumnFlagName) == 0 || (flag & ColumnFlagUndefined) != 0)
                    throw new AcbException($"Unsupported @UTF column flag 0x{flag:X2}.");

                int valueSize = GetValueSize(type);
                int valueOffset = 0;
                if ((flag & ColumnFlagDefault) != 0)
                {
                    if (schemaPos + valueSize > schemaSize)
                        throw new AcbException("@UTF schema default value is truncated.");
                    valueOffset = schemaPos;
                    schemaPos += valueSize;
                }
                else if ((flag & ColumnFlagRow) != 0)
                {
                    valueOffset = columnOffset;
                    columnOffset += valueSize;
                }

                string columnName = ReadString(data, tableOffset + stringsOffset, stringsSize, nameStringOffset);
                columns[i] = new Column(flag, type, columnName, valueOffset);
                if (!indexes.ContainsKey(columnName))
                    indexes.Add(columnName, i);
            }

            if (nameOffset > stringsSize)
                throw new AcbException("@UTF table name offset is invalid.");

            return new CriUtfTable(data, tableOffset, tableSize, rowsOffset, stringsOffset, dataOffset, nameOffset, rowWidth, rowCount, columns, indexes);
        }

        public bool TryGetData(int row, string columnName, out int offset, out int size)
        {
            offset = 0;
            size = 0;
            if (!TryQuery(row, columnName, out UtfValue value) || value.Type != ColumnType.VlData)
                return false;

            offset = checked(_tableOffset + _dataOffset + (int)value.DataOffset);
            size = checked((int)value.DataSize);
            if (offset < 0 || size < 0 || offset > _data.Length || _data.Length - offset < size)
                return false;
            return true;
        }

        public bool TryGetString(int row, string columnName, out string value)
        {
            value = string.Empty;
            if (!TryQuery(row, columnName, out UtfValue result) || result.Type != ColumnType.String)
                return false;
            value = ReadString(checked((int)result.UInt64));
            return true;
        }

        public bool TryGetUInt8(int row, string columnName, out byte value)
        {
            value = 0;
            if (!TryQuery(row, columnName, out UtfValue result) || result.Type != ColumnType.UInt8)
                return false;
            value = (byte)result.UInt64;
            return true;
        }

        public bool TryGetUInt16(int row, string columnName, out ushort value)
        {
            value = 0;
            if (!TryQuery(row, columnName, out UtfValue result) || result.Type != ColumnType.UInt16)
                return false;
            value = (ushort)result.UInt64;
            return true;
        }

        public bool TryGetUInt32(int row, string columnName, out uint value)
        {
            value = 0;
            if (!TryQuery(row, columnName, out UtfValue result) || result.Type != ColumnType.UInt32)
                return false;
            value = (uint)result.UInt64;
            return true;
        }

        public bool TryGetInt32(int row, string columnName, out int value)
        {
            value = 0;
            if (!TryQuery(row, columnName, out UtfValue result) || result.Type != ColumnType.Int32)
                return false;
            value = (int)result.Int64;
            return true;
        }

        public bool HasColumn(string columnName)
        {
            return _columnIndexes.ContainsKey(columnName);
        }

        private bool TryQuery(int row, string columnName, out UtfValue value)
        {
            value = default;
            if (!_columnIndexes.TryGetValue(columnName, out int column))
                return false;
            return TryQuery(row, column, out value);
        }

        private bool TryQuery(int row, int column, out UtfValue value)
        {
            value = default;
            if (row < 0 || row >= RowCount || column < 0 || column >= _columns.Length)
                return false;

            Column col = _columns[column];
            int valueOffset;
            if ((col.Flag & ColumnFlagDefault) != 0)
                valueOffset = _tableOffset + _schemaOffset + col.Offset;
            else if ((col.Flag & ColumnFlagRow) != 0)
                valueOffset = _tableOffset + _rowsOffset + row * _rowWidth + col.Offset;
            else
                return false;

            if (valueOffset < 0 || valueOffset >= _data.Length)
                return false;

            switch (col.Type)
            {
                case ColumnType.UInt8:
                    value = UtfValue.FromUInt(col.Type, _data[valueOffset]);
                    return true;
                case ColumnType.Int8:
                    value = UtfValue.FromInt(col.Type, unchecked((sbyte)_data[valueOffset]));
                    return true;
                case ColumnType.UInt16:
                    value = UtfValue.FromUInt(col.Type, BinaryUtil.U16BE(_data, valueOffset));
                    return true;
                case ColumnType.Int16:
                    value = UtfValue.FromInt(col.Type, unchecked((short)BinaryUtil.U16BE(_data, valueOffset)));
                    return true;
                case ColumnType.UInt32:
                    value = UtfValue.FromUInt(col.Type, BinaryUtil.U32BE(_data, valueOffset));
                    return true;
                case ColumnType.Int32:
                    value = UtfValue.FromInt(col.Type, BinaryUtil.I32BE(_data, valueOffset));
                    return true;
                case ColumnType.UInt64:
                    value = UtfValue.FromUInt(col.Type, BinaryUtil.U64BE(_data, valueOffset));
                    return true;
                case ColumnType.Int64:
                    value = UtfValue.FromInt(col.Type, unchecked((long)BinaryUtil.U64BE(_data, valueOffset)));
                    return true;
                case ColumnType.Float:
                    value = UtfValue.FromUInt(col.Type, BinaryUtil.U32BE(_data, valueOffset));
                    return true;
                case ColumnType.String:
                    value = UtfValue.FromUInt(col.Type, BinaryUtil.U32BE(_data, valueOffset));
                    return true;
                case ColumnType.VlData:
                    value = UtfValue.FromData(BinaryUtil.U32BE(_data, valueOffset), BinaryUtil.U32BE(_data, valueOffset + 4));
                    return true;
                default:
                    return false;
            }
        }

        private string ReadString(int stringOffset)
        {
            return ReadString(_data, _tableOffset + _stringsOffset, _stringsSize, stringOffset);
        }

        private static string ReadString(byte[] data, int stringsStart, int stringsSize, int stringOffset)
        {
            if (stringOffset < 0 || stringOffset > stringsSize)
                return string.Empty;
            return BinaryUtil.NullTerminatedAscii(new ReadOnlySpan<byte>(data, stringsStart + stringOffset, stringsSize - stringOffset));
        }

        private static int GetValueSize(ColumnType type)
        {
            switch (type)
            {
                case ColumnType.UInt8:
                case ColumnType.Int8:
                    return 1;
                case ColumnType.UInt16:
                case ColumnType.Int16:
                    return 2;
                case ColumnType.UInt32:
                case ColumnType.Int32:
                case ColumnType.Float:
                case ColumnType.String:
                    return 4;
                case ColumnType.UInt64:
                case ColumnType.Int64:
                case ColumnType.VlData:
                    return 8;
                default:
                    throw new AcbException($"Unsupported @UTF column type {type}.");
            }
        }

        private readonly struct Column
        {
            public Column(int flag, ColumnType type, string name, int offset)
            {
                Flag = flag;
                Type = type;
                Name = name;
                Offset = offset;
            }

            public int Flag { get; }

            public ColumnType Type { get; }

            public string Name { get; }

            public int Offset { get; }
        }

        private readonly struct UtfValue
        {
            private UtfValue(ColumnType type, ulong unsignedValue, long signedValue, uint dataOffset, uint dataSize)
            {
                Type = type;
                UInt64 = unsignedValue;
                Int64 = signedValue;
                DataOffset = dataOffset;
                DataSize = dataSize;
            }

            public ColumnType Type { get; }

            public ulong UInt64 { get; }

            public long Int64 { get; }

            public uint DataOffset { get; }

            public uint DataSize { get; }

            public static UtfValue FromUInt(ColumnType type, ulong value)
            {
                return new UtfValue(type, value, unchecked((long)value), 0, 0);
            }

            public static UtfValue FromInt(ColumnType type, long value)
            {
                return new UtfValue(type, unchecked((ulong)value), value, 0, 0);
            }

            public static UtfValue FromData(uint offset, uint size)
            {
                return new UtfValue(ColumnType.VlData, 0, 0, offset, size);
            }
        }

        private enum ColumnType
        {
            UInt8 = 0x00,
            Int8 = 0x01,
            UInt16 = 0x02,
            Int16 = 0x03,
            UInt32 = 0x04,
            Int32 = 0x05,
            UInt64 = 0x06,
            Int64 = 0x07,
            Float = 0x08,
            Double = 0x09,
            String = 0x0A,
            VlData = 0x0B,
            UInt128 = 0x0C,
        }
    }
}
