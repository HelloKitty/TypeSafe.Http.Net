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
		Task<TReturnType> Send<TReturnType>(IRestClientRequestContext requestContext);
	}
}
