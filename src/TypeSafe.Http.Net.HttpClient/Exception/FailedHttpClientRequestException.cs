using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// The .NET <see cref="HttpClient"/> implementation of the <see cref="FailedHttpRequestException"/>.
	/// </summary>
	public sealed class FailedHttpClientRequestException : FailedHttpRequestException
	{
		//TODO: Are these unique? Can we expose this as a readonly dictionary?
		/// <inheritdoc />
		public override IEnumerable<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders => Response.Headers;

		/// <inheritdoc />
		public override string ErrorReason => Response.ReasonPhrase;

		/// <inheritdoc />
		public override int StatusCode => (int)Response.StatusCode;

		/// <summary>
		/// The response associated with the error.
		/// </summary>
		public HttpResponseMessage Response { get; }

		/// <inheritdoc />
		public FailedHttpClientRequestException(IHttpClientRequestContext request, HttpResponseMessage response) 
			: base(request)
		{
			if(response == null) throw new ArgumentNullException(nameof(response));

			Response = response;
		}

		/// <inheritdoc />
		public FailedHttpClientRequestException(string message, IHttpClientRequestContext request, HttpResponseMessage response) 
			: base(message, request)
		{
			if(response == null) throw new ArgumentNullException(nameof(response));

			Response = response;
		}

		/// <inheritdoc />
		public FailedHttpClientRequestException(string message, Exception innerException, IHttpClientRequestContext request, HttpResponseMessage response) 
			: base(message, innerException, request)
		{
			if(response == null) throw new ArgumentNullException(nameof(response));

			Response = response;
		}
	}
}
