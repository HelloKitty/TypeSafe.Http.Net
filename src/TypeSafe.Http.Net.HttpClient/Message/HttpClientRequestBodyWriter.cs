using System;
using System.Collections.Generic;
using System.Net.Http;
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
		public void Write(byte[] bytes)
		{
			//TODO: Should we worry about the content type it might set?
			RequestMessage.Content = new ByteArrayContent(bytes);
			RequestMessage.Content.Headers.Clear();
		}

		/// <inheritdoc />
		public void Write(string content)
		{
			RequestMessage.Content = new StringContent(content);
			RequestMessage.Content.Headers.Clear();
		}

		/// <inheritdoc />
		public Task WriteAsync(byte[] bytes)
		{
			//TODO: Does this capture exceptions still?
			//TODO: Should we worry about the content type it might set?
			RequestMessage.Content = new ByteArrayContent(bytes);
			RequestMessage.Content.Headers.Clear();

			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Task WriteAsync(string content)
		{
			//TODO: Does this capture exceptions still?
			RequestMessage.Content = new StringContent(content);
			RequestMessage.Content.Headers.Clear();

			return Task.CompletedTask;
		}
	}
}
