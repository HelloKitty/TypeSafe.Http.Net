using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypeSafe.Http.Net
{
	public sealed class JsonNetJsonBodySerializerStrategy : IRequestSerializationStrategy, IResponseDeserializationStrategy, IContentTypeAssociable
	{
		//TODO: Hide array behind for more efficient content-type lookup
		/// <inheritdoc />
		public IEnumerable<string> AssociatedContentType { get; }

		public JsonNetJsonBodySerializerStrategy()
		{
			//See: http://www.ietf.org/rfc/rfc4627.txt
			AssociatedContentType = new string[] { @"application/json" };
		}

		/// <inheritdoc />
		public bool TrySerialize(object content, IRequestBodyWriter writer)
		{
			string json = JsonConvert.SerializeObject(content);

			if (String.IsNullOrWhiteSpace(json))
				return false;

			//At this point we have hopefully valid JSON and we should write it with the content type
			writer.Write(json, AssociatedContentType.First());

			return true;
		}

		/// <inheritdoc />
		public TReturnType Deserialize<TReturnType>(IResponseBodyReader reader)
		{
			string json = reader.ReadAsString();

			return JsonConvert.DeserializeObject<TReturnType>(json);
		}

		/// <inheritdoc />
		public async Task<TReturnType> DeserializeAsync<TReturnType>(IResponseBodyReader reader)
		{
			string json = await reader.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<TReturnType>(json);
		}
	}
}
