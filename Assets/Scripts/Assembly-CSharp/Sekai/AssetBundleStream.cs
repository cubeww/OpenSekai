using System;
using System.IO;

namespace Sekai
{
	public class AssetBundleStream : Stream
	{
		[ThreadStatic]
		private static byte[] _sharedByteBuffer;

		private FileStream _fileStream;
		private bool _isInverted;

		public override bool CanRead => _fileStream != null && _fileStream.CanRead;

		public override bool CanSeek => _fileStream != null && _fileStream.CanSeek;

		public override bool CanWrite => false;

		public override long Length => _fileStream.Length;

		public override long Position
		{
			get => _fileStream.Position;
			set => _fileStream.Position = value;
		}

		public AssetBundleStream(FileStream fileStream)
		{
			_fileStream = fileStream ?? throw new ArgumentNullException(nameof(fileStream));
			_isInverted = true;
		}

		public override void Flush()
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_fileStream?.Dispose();
				_fileStream = null;
			}
			base.Dispose(disposing);
		}

		public override int ReadByte()
		{
			if (_sharedByteBuffer == null)
			{
				_sharedByteBuffer = new byte[1];
			}

			return Read(_sharedByteBuffer, 0, 1) == 0 ? -1 : _sharedByteBuffer[0];
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			long start = Position;
			int read = _fileStream.Read(buffer, offset, count);
			if (_isInverted && start < 128L && read > 0)
			{
				int invertCount = (int)Math.Min(read, 128L - start);
				for (int i = 0; i < invertCount; i++)
				{
					buffer[offset + i] = (byte)~buffer[offset + i];
				}
			}
			return read;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return _fileStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}
	}
}
