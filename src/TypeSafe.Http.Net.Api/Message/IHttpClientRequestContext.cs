using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Context for a HTTP service request.
	/// </summary>
	public interface IHttpClientRequestContext
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
		/// Represents the context of the body.
		/// </summary>
		IRequestBodyContext BodyContext { get; }

		/// <summary>
		/// Dictionary of supressed codes. All values are initialized.
		/// </summary>
		ISupressedErrorCodeContext SupressedErrorCodesContext { get; }
	}
}
