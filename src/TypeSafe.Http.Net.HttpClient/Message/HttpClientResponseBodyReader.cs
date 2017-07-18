using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	public sealed class HttpClientResponseBodyReader : IResponseBodyReader
	{
		/// <summary>
		/// The HTTP response message.
		/// </summary>
		private HttpResponseMessage Response { get; }

		public HttpClientResponseBodyReader(HttpResponseMessage response)
		{
			if (response == null) throw new ArgumentNullException(nameof(response));

			Response = response;
		}

		/// <inheritdoc />
		public async Task<byte[]> ReadAsBytesAsync()
		{
			return await Response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<string> ReadAsStringAsync()
		{
			return await Response.Content.ReadAsStringAsync().ConfigureAwait(false);
		}

		/// <inheritdoc />
		public byte[] ReadAsBytes()
		{
			return Response.Content.ReadAsByteArrayAsync().Result;
		}

		/// <inheritdoc />
		public string ReadAsString()
		{
			return Response.Content.ReadAsStringAsync().Result;
		}
	}
}
