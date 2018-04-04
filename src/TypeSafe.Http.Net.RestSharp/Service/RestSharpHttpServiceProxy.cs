using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace TypeSafe.Http.Net
{
	public abstract class RestSharpHttpServiceProxy : IHttpServiceProxy
	{
		/// <inheritdoc />
		public abstract string BaseUrl { get; }

		/// <summary>
		/// Managed <see cref="HttpClient"/> to use for HTTP service communication.
		/// </summary>
		protected abstract RestSharp.RestClient Client { get; }

		/// <inheritdoc />
		public async Task<TReturnType> Send<TReturnType>(IHttpClientRequestContext requestContext, IRequestSerializationStrategy serializer, IDeserializationStrategyFactory deserializationFactory)
		{
			if (requestContext == null) throw new ArgumentNullException(nameof(requestContext));
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			IRestResponse response = await SendHttpRequest(requestContext, serializer).ConfigureAwait(false);

			return await FindValidDeserializer(deserializationFactory, response)
				.DeserializeAsync<TReturnType>(new RestSharpResponseBodyReader(response))
				.ConfigureAwait(false);
		}

		private IResponseDeserializationStrategy FindValidDeserializer(IDeserializationStrategyFactory deserializationFactory, IRestResponse response)
		{
			if (deserializationFactory == null) throw new ArgumentNullException(nameof(deserializationFactory));
			if (response == null) throw new ArgumentNullException(nameof(response));

			//TODO: Restsharp doesn't split up content type and encoding. We need to maybe step through each value.
			//We must try to find a serializer based on the content type header in the response
			return deserializationFactory.DeserializerFor(response.ContentType.Split(';').FirstOrDefault());
		}

		/// <inheritdoc />
		public async Task<TReturnType> Send<TReturnType>(IHttpClientRequestContext requestContext, IDeserializationStrategyFactory deserializationFactory)
		{
			if (requestContext == null) throw new ArgumentNullException(nameof(requestContext));

			IRestResponse response = await SendHttpRequest(requestContext).ConfigureAwait(false);

			return await FindValidDeserializer(deserializationFactory, response)
				.DeserializeAsync<TReturnType>(new RestSharpResponseBodyReader(response))
				.ConfigureAwait(false);
		}

		/// <inheritdoc />
		public Task Send(IHttpClientRequestContext requestContext, IRequestSerializationStrategy serializer)
		{
			//Make sure to dipose the response
			return SendHttpRequest(requestContext, serializer);
		}

		/// <inheritdoc />
		public Task Send(IHttpClientRequestContext requestContext)
		{
			//Send a request WITHOUT serialization which means no body.
			//Make sure to dipose the response
			return SendHttpRequest(requestContext);
		}

		//TODO: Document
		private Task<IRestResponse> SendHttpRequest(IHttpClientRequestContext requestContext, IRequestSerializationStrategy serializer)
		{
			if (requestContext == null) throw new ArgumentNullException(nameof(requestContext));
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			//Build a request message targeted towards the base URL and the action path built by the caller.
			RestRequest request = new RestRequest(ConvertToRestSharpMethod(requestContext.RequestMethod));
			request.Resource = BuildFullUrlPath(requestContext);

			WriteBodyContent(requestContext, serializer, request);
			WriteHeaders(requestContext, request);
			return SendBaseRequest(requestContext, request);
		}

		private Method ConvertToRestSharpMethod(HttpMethod httpMethod)
		{
			//TODO: Cache
			return (Method)Enum.Parse(typeof(Method), httpMethod.Method.ToUpper());
		}

		//TODO: Document
		private Task<IRestResponse> SendHttpRequest(IHttpClientRequestContext requestContext)
		{
			if (requestContext == null) throw new ArgumentNullException(nameof(requestContext));

			//Build a request message targeted towards the base URL and the action path built by the caller.
			RestRequest request = new RestRequest(ConvertToRestSharpMethod(requestContext.RequestMethod));
			request.Resource = BuildFullUrlPath(requestContext);

			WriteHeaders(requestContext, request);
			return SendBaseRequest(requestContext, request);
		}

		private string BuildFullUrlPath(IHttpClientRequestContext requestContext)
		{
			//TODO: Cache this
			return requestContext.ActionPath.TrimStart('/');
		}

		//Made overridable so implementations can change how the requet is sent.
		/// <summary>
		/// This method sends a request through the <see cref="Client"/> using the provided built
		/// <see cref="HttpRequestMessage"/> and the <see cref="IHttpClientRequestContext"/>.
		/// </summary>
		/// <param name="requestContext"></param>
		/// <param name="request"></param>
		/// <returns>A future response.</returns>
		protected virtual async Task<IRestResponse> SendBaseRequest(IHttpClientRequestContext requestContext, RestRequest request)
		{
			if (Client == null)
				throw new InvalidOperationException($"The {nameof(HttpClient)} property {nameof(Client)} is null.");

			IRestResponse response = await Client.ExecuteTaskAsync(request)
				.ConfigureAwait(false);

			//This indicates that the request encountered a network error: https://github.com/restsharp/RestSharp/wiki/Getting-Started
			if (response.ResponseStatus != ResponseStatus.Completed)
				throw new InvalidOperationException($"Request failed with Code: {response.StatusCode} with Context: {requestContext}.");

			//It's possible it's still a failed request
			if (!IsSuccessStatusCode(response.StatusCode) && !requestContext.SupressedErrorCodesContext.SupressedErrorCodes[(int)response.StatusCode])
				throw new FailedRestSharpRequestException($"Request failed with Code: {response.StatusCode} Reason: {response.ErrorMessage}", response.ErrorException, requestContext, response);

			return response;
		}

		//From: https://searchcode.com/codesearch/view/28600545/
		public bool IsSuccessStatusCode(HttpStatusCode statusCode)
		{
			return ((int)statusCode >= 200) && ((int)statusCode <= 299);
		}

		private static void WriteHeaders(IHttpClientRequestContext requestContext, RestRequest request)
		{
			//We need to add the headers. We expect that they are already constructed with any formatted or inserted values already inserted.
			foreach (IRequestHeader header in requestContext.RequestHeaders)
			{
				request.AddHeader(header.HeaderType, header.HeaderValue);
			}
		}

		private static void WriteBodyContent(IHttpClientRequestContext requestContext, IRequestSerializationStrategy serializer, RestRequest request)
		{
			//Write the body first to avoid messing with the headers
			if (requestContext.BodyContext.HasBody)
				serializer.TrySerialize(requestContext.BodyContext.Body, new RestSharpRequestBodyWriter(request));
		}
	}
}
