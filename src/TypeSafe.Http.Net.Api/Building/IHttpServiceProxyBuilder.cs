using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for a HTTP service builder.
	/// </summary>
	/// <typeparam name="THttpServiceInterface">The http service type.</typeparam>
	public interface IHttpServiceProxyBuilder<out THttpServiceInterface>
		where THttpServiceInterface : class

	{
		/// <summary>
		/// Builds the HTTP service <see cref="THttpServiceInterface"/>.
		/// </summary>
		/// <returns>An instance of the constructed HTTP service.</returns>
		THttpServiceInterface Build();
	}
}
