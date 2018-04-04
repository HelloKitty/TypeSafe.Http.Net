using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Base <see cref="Exception"/> Type for internal request failure throwing.
	/// </summary>
	public abstract class FailedHttpRequestException : Exception
	{
		/// <summary>
		/// The context of the request.
		/// </summary>
		public IHttpClientRequestContext Request { get; }

		/// <summary>
		/// Collection of response headers.
		/// </summary>
		public abstract IEnumerable<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders { get; }

		/// <summary>
		/// The response's optional string explaination of the
		/// error.
		/// </summary>
		public abstract string ErrorReason { get; }

		/// <summary>
		/// The status code of the response.
		/// </summary>
		public abstract int StatusCode { get; }

		protected FailedHttpRequestException(IHttpClientRequestContext request)
		{
			if(request == null) throw new ArgumentNullException(nameof(request));

			Request = request;
		}

		protected FailedHttpRequestException(string message, IHttpClientRequestContext request) 
			: base(message)
		{
			if(request == null) throw new ArgumentNullException(nameof(request));

			Request = request;
		}

		protected FailedHttpRequestException(string message, Exception innerException, IHttpClientRequestContext request) 
			: base(message, innerException)
		{
			if(request == null) throw new ArgumentNullException(nameof(request));

			Request = request;
		}
	}
}
