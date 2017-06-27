using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Context for a REST service request.
	/// </summary>
	public interface IRestClientRequestContext
	{
		/// <summary>
		/// The action path to use.
		/// Will contain the contextual query string parameters too if the request
		/// requires query string parameters.
		/// (ex. /api/auth)
		/// </summary>
		string ActionPath { get; }

		/// <summary>
		/// The method for the request.
		/// </summary>
		HttpMethod RequestMethod { get; }

		/// <summary>
		/// Collection of the request headers to be used.
		/// </summary>
		IEnumerable<IRequestHeader> RequestHeaders { get; }

		/// <summary>
		/// Indicates if the request contains a body to write.
		/// </summary>
		bool HasBody { get; }

		//Visit this method to get the body written.
		/// <summary>
		/// Attempts to write the body to the request body.
		/// </summary>
		/// <param name="writer">The writer to use to write the body data.</param>
		/// <returns>True if the body was successfully written.</returns>
		bool WriteBody(IRequestBodyWriter writer);
	}
}
