using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// <see cref="HttpClient"/> implementation of the request body writer.
	/// </summary>
	public sealed class HttpClientRequestBodyWriter : IRequestBodyWriter
	{
		public HttpRequestMessage RequestMessage { get; }

		public HttpClientRequestBodyWriter(HttpRequestMessage requestMessage)
		{
			if (requestMessage == null) throw new ArgumentNullException(nameof(requestMessage));

			RequestMessage = requestMessage;
		}

		/// <inheritdoc />
		public void Write(byte[] bytes, string contentTypeValue)
		{
			//TODO: Should we worry about the content type it might set?
			RequestMessage.Content = new ByteArrayContent(bytes);
			RequestMessage.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentTypeValue);
		}

		/// <inheritdoc />
		public void Write(string content, string contentTypeValue)
		{
			RequestMessage.Content = new StringContent(content);
			RequestMessage.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentTypeValue);
		}

		/// <inheritdoc />
		public Task WriteAsync(byte[] bytes, string contentTypeValue)
		{
			//TODO: Does this capture exceptions still?
			//TODO: Should we worry about the content type it might set?
			RequestMessage.Content = new ByteArrayContent(bytes);
			RequestMessage.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentTypeValue);

#if NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NET46 || NETSTANDARD2_0
			return Task.CompletedTask;
#endif

#if NET45
			return Task.FromResult(0);
#endif
		}

		/// <inheritdoc />
		public Task WriteAsync(string content, string contentTypeValue)
		{
			//TODO: Does this capture exceptions still?
			RequestMessage.Content = new StringContent(content);
			RequestMessage.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentTypeValue);

#if NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NET46 || NETSTANDARD2_0
			return Task.CompletedTask;
#endif

#if NET45
			return Task.FromResult(0);
#endif
		}
	}
}
