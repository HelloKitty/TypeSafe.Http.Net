using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TypeSafe.Http.Net
{
	public sealed class DefaultHttpClientHttpServiceProxy : HttpClientHttpServiceProxy
	{
		/// <inheritdoc />
		public override string BaseUrl { get; }

		/// <inheritdoc />
		protected override HttpClient Client { get; }

		public DefaultHttpClientHttpServiceProxy(string baseUrl)
		{
			//TODO: Better checcking that this is a valid address
			if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentException($"Provided {nameof(baseUrl)} cannot be null or whitespace. It must be a valid address.", nameof(baseUrl));

			BaseUrl = baseUrl;
			Client = new HttpClient() { BaseAddress = new Uri(BaseUrl) };
		}

		public DefaultHttpClientHttpServiceProxy(string baseUrl, HttpMessageHandler messageHandler)
		{
			//TODO: Better checcking that this is a valid address
			if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentException($"Provided {nameof(baseUrl)} cannot be null or whitespace. It must be a valid address.", nameof(baseUrl));

			BaseUrl = baseUrl;
			Client = new HttpClient(messageHandler) { BaseAddress = new Uri(BaseUrl) };
		}
	}
}
