using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for all rest services.
	/// </summary>
	public interface IHttpService
	{
		/// <summary>
		/// The base endpoint of the REST service.
		/// </summary>
		string BaseUrl { get; }
	}
}
