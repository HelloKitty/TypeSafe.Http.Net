using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	public sealed class ProtobufNetProtobufBodySerializerStrategy : IRequestSerializationStrategy, IResponseDeserializationStrategy, IContentTypeAssociable
	{
		//Protobuf isn't standardized with a content/MIME so most use x-protobuf but sometimes protobuf.
		/// <inheritdoc />
		public IEnumerable<string> AssociatedContentType { get; } = new string[] { "application/x-protobuf", "application/protobuf" };

		/// <inheritdoc />
		public bool TrySerialize(object content, IRequestBodyWriter writer)
		{
			if (content == null)
				return false;

			using (MemoryStream ms = new MemoryStream())
			{
				ProtoBuf.Serializer.Serialize(ms, content);
				writer.Write(ms.ToArray(), AssociatedContentType.First());
			}

			return true;
		}

		/// <inheritdoc />
		public TReturnType Deserialize<TReturnType>(IResponseBodyReader reader)
		{
			using (MemoryStream ms = new MemoryStream(reader.ReadAsBytes()))
			{
				return ProtoBuf.Serializer.Deserialize<TReturnType>(ms);
			}
		}

		/// <inheritdoc />
		public async Task<TReturnType> DeserializeAsync<TReturnType>(IResponseBodyReader reader)
		{
			using (MemoryStream ms = new MemoryStream(await reader.ReadAsBytesAsync()))
			{
				return ProtoBuf.Serializer.Deserialize<TReturnType>(ms);
			}
		}
	}
}
