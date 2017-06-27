using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for rest service proxies that mediate between the HTTP application layer and the consuming user.
	/// </summary>
	/// <typeparam name="TProxyInterface">The interface proxy type.</typeparam>
	public interface IRestServiceProxy<TProxyInterface> : IRestService
		where TProxyInterface : class //really we should be constraining to interface but that's not possible.
	{
		/// <summary>
		/// Sends a request with the designated <see cref="TReturnType"/> with the provided context and the strategy
		/// for deserializing the response.
		/// </summary>
		/// <typeparam name="TReturnType">The return type to expect.</typeparam>
		/// <param name="requestContext">The context of the request.</param>
		/// <param name="deserializer">The deserialization strategy.</param>
		/// <returns>The a future promise for a deserialized return data.</returns>
		Task<TReturnType> Send<TReturnType>(IRestClientRequestContext requestContext, IResponseDeserializationStrategy<TReturnType> deserializer);

		/// <summary>
		/// Sends a request with the provided context with no response data.
		/// </summary>
		/// <param name="requestContext">The context of the request.</param>
		/// <returns>A future.</returns>
		Task Send(IRestClientRequestContext requestContext);
	}
}
