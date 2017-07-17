using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// <see cref="HttpClient"/> implementation of the <see cref="IHttpServiceProxy"/>.
	/// </summary>
	public abstract class HttpClientHttpServiceProxy : IHttpServiceProxy, IDisposable
	{
		/// <inheritdoc />
		public abstract string BaseUrl { get; }

		/// <summary>
		/// Managed <see cref="HttpClient"/> to use for HTTP service communication.
		/// </summary>
		protected abstract HttpClient Client { get; }

		/// <inheritdoc />
		public async Task<TReturnType> Send<TReturnType>(IHttpClientRequestContext requestContext, IRequestSerializationStrategy serializer, IDeserializationStrategyFactory deserializationFactory)
		{
			if (requestContext == null) throw new ArgumentNullException(nameof(requestContext));
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			using (HttpResponseMessage response = await SendHttpRequest(requestContext, serializer))
			{
				return await FindValidDeserializer(deserializationFactory, response).DeserializeAsync<TReturnType>(new HttpClientResponseBodyReader(response));
			}
		}

		private IResponseDeserializationStrategy FindValidDeserializer(IDeserializationStrategyFactory deserializationFactory, HttpResponseMessage response)
		{
			if (deserializationFactory == null) throw new ArgumentNullException(nameof(deserializationFactory));
			if (response == null) throw new ArgumentNullException(nameof(response));
			
			//We must try to find a serializer based on the content type header in the response
			return deserializationFactory.DeserializerFor(response.Content.Headers.ContentType.MediaType);
		}

		/// <inheritdoc />
		public async Task<TReturnType> Send<TReturnType>(IHttpClientRequestContext requestContext, IDeserializationStrategyFactory deserializationFactory)
		{
			if (requestContext == null) throw new ArgumentNullException(nameof(requestContext));

			using (HttpResponseMessage response = await SendHttpRequest(requestContext))
			{
				return await FindValidDeserializer(deserializationFactory, response).DeserializeAsync<TReturnType>(new HttpClientResponseBodyReader(response));
			}
		}

		/// <inheritdoc />
		public async Task Send(IHttpClientRequestContext requestContext, IRequestSerializationStrategy serializer)
		{
			//Make sure to dipose the response
			(await SendHttpRequest(requestContext, serializer)).Dispose();
		}

		/// <inheritdoc />
		public async Task Send(IHttpClientRequestContext requestContext)
		{
			//Send a request WITHOUT serialization which means no body.
			//Make sure to dipose the response
			(await SendHttpRequest(requestContext)).Dispose();
		}

		//TODO: Document
		private async Task<HttpResponseMessage> SendHttpRequest(IHttpClientRequestContext requestContext, IRequestSerializationStrategy serializer)
		{
			if (requestContext == null) throw new ArgumentNullException(nameof(requestContext));
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			//Build a request message targeted towards the base URL and the action path built by the caller.
			using (HttpRequestMessage request = new HttpRequestMessage(requestContext.RequestMethod, BuildFullUrlPath(requestContext)))
			{
				WriteBodyContent(requestContext, serializer, request);
				WriteHeaders(requestContext, request);
				return await SendBaseRequest(requestContext, request);
			}
		}

		//TODO: Document
		private async Task<HttpResponseMessage> SendHttpRequest(IHttpClientRequestContext requestContext)
		{
			if (requestContext == null) throw new ArgumentNullException(nameof(requestContext));

			//Build a request message targeted towards the base URL and the action path built by the caller.
			using (HttpRequestMessage request = new HttpRequestMessage(requestContext.RequestMethod, BuildFullUrlPath(requestContext)))
			{
				WriteHeaders(requestContext, request);
				return await SendBaseRequest(requestContext, request);
			}
		}

		private string BuildFullUrlPath(IHttpClientRequestContext requestContext)
		{
			return requestContext.ActionPath;
		}

		//Made overridable so implementations can change how the requet is sent.
		/// <summary>
		/// This method sends a request through the <see cref="Client"/> using the provided built
		/// <see cref="HttpRequestMessage"/> and the <see cref="IHttpClientRequestContext"/>.
		/// </summary>
		/// <param name="requestContext"></param>
		/// <param name="request"></param>
		/// <returns>A future response.</returns>
		protected virtual async Task<HttpResponseMessage> SendBaseRequest(IHttpClientRequestContext requestContext, HttpRequestMessage request)
		{
			if(Client == null)
				throw new InvalidOperationException($"The {nameof(HttpClient)} property {nameof(Client)} is null.");

			//Don't use a using here, the caller has to be responsible for diposing it because they own it, not us. They may need to use it.
			HttpResponseMessage response = await Client.SendAsync(request);

			//Throw a debug viable message if the response is not successful.
			if (!response.IsSuccessStatusCode)
				throw new InvalidOperationException($"Request failed with Code: {response.StatusCode} with Context: {requestContext}.");

			return response;
		}

		private static void WriteHeaders(IHttpClientRequestContext requestContext, HttpRequestMessage request)
		{
			//We need to add the headers. We expect that they are already constructed with any formatted or inserted values already inserted.
			foreach (IRequestHeader header in requestContext.RequestHeaders)
			{
				if (!request.Headers.TryAddWithoutValidation(header.HeaderType, header.HeaderValue))
					throw new InvalidOperationException($"Request failed to add HeaderType: {header.HeaderType} with Value: {header.HeaderValue} with Context: {requestContext}.");
			}
		}

		private static void WriteBodyContent(IHttpClientRequestContext requestContext, IRequestSerializationStrategy serializer, HttpRequestMessage request)
		{
			//Write the body first to avoid messing with the headers
			if (requestContext.BodyContext.HasBody)
				serializer.TrySerialize(requestContext.BodyContext.Body, new HttpClientRequestBodyWriter(request));
		}

		/// <inheritdoc />
		public void Dispose()
		{
			Client?.Dispose();
		}
	}
}
