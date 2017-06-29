using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// <see cref="HttpClient"/> implementation of the <see cref="IRestServiceProxy"/>.
	/// </summary>
	public sealed class HttpClientRestServiceProxy : IRestServiceProxy, IDisposable
	{
		/// <inheritdoc />
		public string BaseUrl { get; }

		/// <summary>
		/// Managed <see cref="HttpClient"/> to use for REST service communication.
		/// </summary>
		private HttpClient Client { get; }

		public HttpClientRestServiceProxy(string baseUrl)
		{
			//TODO: Better checcking that this is a valid address
			if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentException($"Provided {nameof(baseUrl)} cannot be null or whitespace. It must be a valid address.", nameof(baseUrl));

			BaseUrl = baseUrl;
			Client = new HttpClient();
		}

		/// <inheritdoc />
		public async Task<TReturnType> Send<TReturnType>(IRestClientRequestContext requestContext, IResponseDeserializationStrategy deserializer)
		{
			using (HttpResponseMessage response = await SendHttpRequest(requestContext))
			{
				return await deserializer.DeserializeAsync<TReturnType>(new HttpClientResponseBodyReader(response));
			}
		}

		/// <inheritdoc />
		public async Task Send(IRestClientRequestContext requestContext)
		{
			//Make sure to dipose the response
			(await SendHttpRequest(requestContext)).Dispose();
		}

		//TODO: Document
		private async Task<HttpResponseMessage> SendHttpRequest(IRestClientRequestContext requestContext)
		{
			if (requestContext == null) throw new ArgumentNullException(nameof(requestContext));

			//Build a request message targeted towards the base URL and the action path built by the caller.
			using (HttpRequestMessage request = new HttpRequestMessage(requestContext.RequestMethod, $"{BaseUrl}{requestContext.ActionPath}"))
			{
				//Write the body first to avoid messing with the headers
				if (requestContext.HasBody)
					requestContext.WriteBody(new HttpClientRequestBodyWriter(request));

				//We need to add the headers. We expect that they are already constructed with any formatted or inserted values already inserted.
				foreach (IRequestHeader header in requestContext.RequestHeaders)
				{
					if (!request.Headers.TryAddWithoutValidation(header.HeaderType, header.HeaderValue))
						throw new InvalidOperationException($"Request failed to add HeaderType: {header.HeaderType} with Value: {header.HeaderValue} with Context: {requestContext}.");
				}

				//Don't use a using here, the caller has to be responsible for diposing it because they own it, not us. They may need to use it.
				HttpResponseMessage response = await Client.SendAsync(request);

				//Throw a debug viable message if the response is not successful.
				if (!response.IsSuccessStatusCode)
					throw new InvalidOperationException($"Request failed with Code: {response.StatusCode} with Context: {requestContext}.");

				return response;
			}
		}

		/// <inheritdoc />
		public void Dispose()
		{
			Client?.Dispose();
		}
	}
}
