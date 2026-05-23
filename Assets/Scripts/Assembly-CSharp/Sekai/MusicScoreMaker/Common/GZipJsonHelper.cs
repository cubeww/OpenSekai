using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Sekai.MusicScoreMaker.Common
{
	public static class GZipJsonHelper
	{
		public static string CompressJsonToString(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentException("json is null or empty.", nameof(json));
			}
			byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
			using MemoryStream memoryStream = new MemoryStream();
			using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
			{
				gzipStream.Write(jsonBytes, 0, jsonBytes.Length);
			}
			byte[] gzipBytes = memoryStream.ToArray();
			NormalizeGZipHeader(gzipBytes);
			return Convert.ToBase64String(gzipBytes);
		}

		private static void NormalizeGZipHeader(byte[] gzipBytes)
		{
			if (gzipBytes == null)
			{
				throw new ArgumentNullException(nameof(gzipBytes));
			}
			if (gzipBytes.Length >= 10 && gzipBytes[0] == 0x1f && gzipBytes[1] == 0x8b)
			{
				gzipBytes[4] = 0;
				gzipBytes[5] = 0;
				gzipBytes[6] = 0;
				gzipBytes[7] = 0;
				gzipBytes[9] = 0xff;
			}
		}

		public static string DecompressStringToJson(string compressedBase64)
		{
			if (string.IsNullOrEmpty(compressedBase64))
			{
				return compressedBase64;
			}
			byte[] gzipBytes = Convert.FromBase64String(compressedBase64);
			using MemoryStream inputStream = new MemoryStream(gzipBytes);
			using GZipStream gzipStream = new GZipStream(inputStream, CompressionMode.Decompress);
			using MemoryStream outputStream = new MemoryStream();
			gzipStream.CopyTo(outputStream);
			return Encoding.UTF8.GetString(outputStream.ToArray());
		}
	}
}
