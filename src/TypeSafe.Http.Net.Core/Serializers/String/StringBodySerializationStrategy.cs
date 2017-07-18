using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	public sealed class StringBodySerializationStrategy : IRequestSerializationStrategy, IResponseDeserializationStrategy, IContentTypeAssociable
	{
		/// <inheritdoc />
		public IEnumerable<string> AssociatedContentType { get; }

		public StringBodySerializationStrategy()
		{
			//The content type for Url encoded bodies is almost always ONLY that.
			AssociatedContentType = new string[] { @"text/plain" };
		}

		/// <inheritdoc />
		public bool TrySerialize(object content, IRequestBodyWriter writer)
		{
			if (content == null) throw new ArgumentNullException(nameof(content));

			writer.Write(content.ToString(), AssociatedContentType.First());

			return true;
		}

		/// <inheritdoc />
		public TReturnType Deserialize<TReturnType>(IResponseBodyReader reader)
		{
			//This is ugly but we should TRUST that the caller knows what they are doing
			//but it is very likely that the TReturnType is a string
			return (TReturnType)(object)reader.ReadAsString();
		}

		/// <inheritdoc />
		public async Task<TReturnType> DeserializeAsync<TReturnType>(IResponseBodyReader reader)
		{
			//This is ugly but we should TRUST that the caller knows what they are doing
			//but it is very likely that the TReturnType is a string
			return (TReturnType)(object)await reader.ReadAsStringAsync().ConfigureAwait(false);
		}
	}
}
