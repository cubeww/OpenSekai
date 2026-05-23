using MessagePack;

namespace CP
{
	public class BinarySerializer
	{
		public BinarySerializer()
		{
		}

		public static byte[] Serialize<T>(T data)
		{
			return MessagePackSerializer.Serialize(data, MessagePackSerializer.DefaultOptions);
		}

		public static int Serialize<T>(ref byte[] buffer, T data)
		{
			byte[] bytes = Serialize(data);
			if (buffer == null || buffer.Length < bytes.Length)
			{
				buffer = new byte[bytes.Length];
			}

			System.Buffer.BlockCopy(bytes, 0, buffer, 0, bytes.Length);
			return bytes.Length;
		}

		public static T Deserialize<T>(byte[] bytes)
		{
			return MessagePackSerializer.Deserialize<T>(bytes, MessagePackSerializer.DefaultOptions);
		}
	}
}
