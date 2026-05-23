using System.Collections.Generic;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using UnityEngine;

namespace Sekai
{
	public static class AssetBundleInfoMessagePackResolver
	{
		private static readonly IMessagePackFormatter[] Formatters =
		{
			AssetBundleInfoFormatter.Instance,
			AssetBundleElementFormatter.Instance
		};

		private static bool registered;

		public static MessagePackSerializerOptions Options
		{
			get
			{
				EnsureRegistered();
				return MessagePackSerializer.DefaultOptions;
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void RuntimeInitialize()
		{
			EnsureRegistered();
		}

		public static void EnsureRegistered()
		{
			if (registered)
			{
				return;
			}

			MessagePackSerializerOptions baseOptions = MessagePackSerializer.DefaultOptions ?? MessagePackSerializerOptions.Standard;
			IFormatterResolver resolver = CompositeResolver.Create(
				Formatters,
				new IFormatterResolver[]
				{
					baseOptions.Resolver,
					StandardResolver.Instance
				});
			MessagePackSerializer.DefaultOptions = baseOptions.WithResolver(resolver);
			registered = true;
		}
	}

	internal sealed class AssetBundleInfoFormatter : IMessagePackFormatter<AssetBundleInfo>
	{
		public static readonly AssetBundleInfoFormatter Instance = new AssetBundleInfoFormatter();

		private AssetBundleInfoFormatter()
		{
		}

		public void Serialize(ref MessagePackWriter writer, AssetBundleInfo value, MessagePackSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNil();
				return;
			}

			writer.WriteMapHeader(2);
			writer.Write("version");
			writer.Write(value.version);
			writer.Write("bundles");
			WriteBundles(ref writer, value.bundles, options);
		}

		public AssetBundleInfo Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
		{
			if (reader.TryReadNil())
			{
				return null;
			}

			int count = reader.ReadMapHeader();
			AssetBundleInfo result = new AssetBundleInfo();
			for (int i = 0; i < count; i++)
			{
				string key = ReadString(ref reader);
				switch (key)
				{
					case "version":
						result.version = ReadString(ref reader);
						break;
					case "bundles":
						result.bundles = ReadBundles(ref reader, options);
						break;
					default:
						reader.Skip();
						break;
				}
			}

			if (result.bundles == null)
			{
				result.bundles = new Dictionary<string, AssetBundleElement>();
			}

			return result;
		}

		private static Dictionary<string, AssetBundleElement> ReadBundles(ref MessagePackReader reader, MessagePackSerializerOptions options)
		{
			if (reader.TryReadNil())
			{
				return null;
			}

			int count = reader.ReadMapHeader();
			Dictionary<string, AssetBundleElement> bundles = new Dictionary<string, AssetBundleElement>(count);
			for (int i = 0; i < count; i++)
			{
				string key = ReadString(ref reader);
				AssetBundleElement value = AssetBundleElementFormatter.Instance.Deserialize(ref reader, options);
				if (!string.IsNullOrEmpty(key))
				{
					bundles[key] = value;
				}
			}

			return bundles;
		}

		private static void WriteBundles(ref MessagePackWriter writer, Dictionary<string, AssetBundleElement> bundles, MessagePackSerializerOptions options)
		{
			if (bundles == null)
			{
				writer.WriteNil();
				return;
			}

			writer.WriteMapHeader(bundles.Count);
			foreach (KeyValuePair<string, AssetBundleElement> bundle in bundles)
			{
				writer.Write(bundle.Key);
				AssetBundleElementFormatter.Instance.Serialize(ref writer, bundle.Value, options);
			}
		}

		private static string ReadString(ref MessagePackReader reader)
		{
			return reader.TryReadNil() ? null : reader.ReadString();
		}
	}

	internal sealed class AssetBundleElementFormatter : IMessagePackFormatter<AssetBundleElement>
	{
		public static readonly AssetBundleElementFormatter Instance = new AssetBundleElementFormatter();

		private AssetBundleElementFormatter()
		{
		}

		public void Serialize(ref MessagePackWriter writer, AssetBundleElement value, MessagePackSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNil();
				return;
			}

			writer.WriteMapHeader(11);
			writer.Write("bundleName");
			writer.Write(value.bundleName);
			writer.Write("cacheFileName");
			writer.Write(value.cacheFileName);
			writer.Write("cacheDirectoryName");
			writer.Write(value.cacheDirectoryName);
			writer.Write("hash");
			writer.Write(value.hash);
			writer.Write("category");
			writer.Write(value.category);
			writer.Write("crc");
			writer.Write(value.crc);
			writer.Write("fileSize");
			writer.Write(value.fileSize);
			writer.Write("dependencies");
			WriteStringArray(ref writer, value.dependencies);
			writer.Write("paths");
			WriteStringArray(ref writer, value.paths);
			writer.Write("isBuiltin");
			writer.Write(value.isBuiltin);
			writer.Write("isRelocate");
			writer.Write(value.isRelocate);
		}

		public AssetBundleElement Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
		{
			if (reader.TryReadNil())
			{
				return null;
			}

			int count = reader.ReadMapHeader();
			AssetBundleElement result = new AssetBundleElement();
			for (int i = 0; i < count; i++)
			{
				string key = ReadString(ref reader);
				switch (key)
				{
					case "bundleName":
						result.bundleName = ReadString(ref reader);
						break;
					case "cacheFileName":
						result.cacheFileName = ReadString(ref reader);
						break;
					case "cacheDirectoryName":
						result.cacheDirectoryName = ReadString(ref reader);
						break;
					case "hash":
						result.hash = ReadString(ref reader);
						break;
					case "category":
						result.category = ReadString(ref reader);
						break;
					case "crc":
						result.crc = reader.ReadUInt32();
						break;
					case "fileSize":
						result.fileSize = reader.ReadInt64();
						break;
					case "dependencies":
						result.dependencies = ReadStringArray(ref reader);
						break;
					case "paths":
						result.paths = ReadStringArray(ref reader);
						break;
					case "isBuiltin":
						result.isBuiltin = reader.ReadBoolean();
						break;
					case "isRelocate":
						result.isRelocate = reader.ReadBoolean();
						break;
					default:
						reader.Skip();
						break;
				}
			}

			result.OnAfterDeserialize();
			return result;
		}

		private static string[] ReadStringArray(ref MessagePackReader reader)
		{
			if (reader.TryReadNil())
			{
				return null;
			}

			int count = reader.ReadArrayHeader();
			string[] result = new string[count];
			for (int i = 0; i < count; i++)
			{
				result[i] = ReadString(ref reader);
			}

			return result;
		}

		private static void WriteStringArray(ref MessagePackWriter writer, string[] values)
		{
			if (values == null)
			{
				writer.WriteNil();
				return;
			}

			writer.WriteArrayHeader(values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				writer.Write(values[i]);
			}
		}

		private static string ReadString(ref MessagePackReader reader)
		{
			return reader.TryReadNil() ? null : reader.ReadString();
		}
	}
}
