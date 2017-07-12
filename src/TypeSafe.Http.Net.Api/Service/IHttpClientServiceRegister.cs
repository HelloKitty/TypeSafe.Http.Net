using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for types that allow the registeration of a
	/// HTTP client implementation.
	/// </summary>
	public interface IHttpClientServiceRegister
	{
		/// <summary>
		/// Registers a HTTP client proxy.
		/// </summary>
		/// <param name="proxy">The proxy to register.</param>
		void Register(IHttpServiceProxy proxy);
	}
}
