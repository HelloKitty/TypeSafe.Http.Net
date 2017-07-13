using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for HTTP service proxies that mediate between the HTTP application layer and the consuming user.
	/// </summary>
	public interface IHttpServiceProxy : IHttpService
	{
		/// <summary>
		/// Sends a request with the designated <see cref="TReturnType"/> with the provided context and the strategy
		/// for serializing the body content.
		/// </summary>
		/// <typeparam name="TReturnType">The return type to expect.</typeparam>
		/// <param name="requestContext">The context of the request.</param>
		/// <param name="serializer"></param>
		/// <param name="deserializationFactory"></param>
		/// <returns>The a future promise for a deserialized return data.</returns>
		Task<TReturnType> Send<TReturnType>(IHttpClientRequestContext requestContext, IRequestSerializationStrategy serializer, IDeserializationStrategyFactory deserializationFactory);

		/// <summary>
		/// Sends a request with the designated <see cref="TReturnType"/> with the provided context.
		/// </summary>
		/// <typeparam name="TReturnType">The return type to expect.</typeparam>
		/// <param name="requestContext">The context of the request.</param>
		/// <param name="deserializationFactory"></param>
		/// <returns>The a future promise for a deserialized return data.</returns>
		Task<TReturnType> Send<TReturnType>(IHttpClientRequestContext requestContext, IDeserializationStrategyFactory deserializationFactory);

		/// <summary>
		/// Sends a request with the provided context with no response data.
		/// </summary>
		/// <param name="requestContext">The context of the request.</param>
		/// <param name="serializer"></param>
		/// <returns>A future.</returns>
		Task Send(IHttpClientRequestContext requestContext, IRequestSerializationStrategy serializer);

		/// <summary>
		/// Sends a request with the provided context with no response data.
		/// </summary>
		/// <param name="requestContext">The context of the request.</param>
		/// <returns>A future.</returns>
		Task Send(IHttpClientRequestContext requestContext);
	}
}
