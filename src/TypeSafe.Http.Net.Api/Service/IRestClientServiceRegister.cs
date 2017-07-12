using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for types that allow the registeration of a
	/// REST client implementation.
	/// </summary>
	public interface IRestClientServiceRegister
	{
		/// <summary>
		/// Registers a REST client proxy.
		/// </summary>
		/// <param name="proxy">The proxy to register.</param>
		void Register(IHttpServiceProxy proxy);
	}
}
