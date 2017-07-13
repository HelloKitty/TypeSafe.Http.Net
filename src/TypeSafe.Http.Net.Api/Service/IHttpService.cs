using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for all HTTP services.
	/// </summary>
	public interface IHttpService
	{
		/// <summary>
		/// The base endpoint of the HTTP service.
		/// </summary>
		string BaseUrl { get; }
	}
}
