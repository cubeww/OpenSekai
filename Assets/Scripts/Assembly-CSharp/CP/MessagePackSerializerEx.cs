using MessagePack;

namespace CP
{
	public static class MessagePackSerializerEx
	{
		public static int Serialize<T>(T obj, byte[] buffer, int offset = 0)
		{
			return Serialize(obj, MessagePackSerializer.DefaultOptions, buffer, offset);
		}

		public static int Serialize<T>(T obj, IFormatterResolver resolver, byte[] buffer, int offset = 0)
		{
			return Serialize(obj, MessagePackSerializer.DefaultOptions.WithResolver(resolver), buffer, offset);
		}

		public static int Serialize<T>(T obj, MessagePackSerializerOptions options, byte[] buffer, int offset = 0)
		{
			byte[] bytes = MessagePackSerializer.Serialize(obj, options);
			System.Buffer.BlockCopy(bytes, 0, buffer, offset, bytes.Length);
			return bytes.Length;
		}
	}
}
