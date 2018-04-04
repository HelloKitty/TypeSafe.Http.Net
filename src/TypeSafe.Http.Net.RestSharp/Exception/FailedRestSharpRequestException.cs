using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using RestSharp;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// The .NET <see cref="RestSharp"/> implementation of the <see cref="FailedHttpRequestException"/>.
	/// </summary>
	public sealed class FailedRestSharpRequestException : FailedHttpRequestException
	{
		//TODO: Implementat restsharp headers in exception
		//TODO: Are these unique? Can we expose this as a readonly dictionary?
		/// <inheritdoc />
		public override IEnumerable<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders => Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>();

		/// <inheritdoc />
		public override string ErrorReason => Response.ErrorMessage;

		/// <inheritdoc />
		public override int StatusCode => (int)Response.StatusCode;

		/// <summary>
		/// The response associated with the error.
		/// </summary>
		public IRestResponse Response { get; }

		/// <inheritdoc />
		public FailedRestSharpRequestException(IHttpClientRequestContext request, IRestResponse response) 
			: base(request)
		{
			if(response == null) throw new ArgumentNullException(nameof(response));

			Response = response;
		}

		/// <inheritdoc />
		public FailedRestSharpRequestException(string message, IHttpClientRequestContext request, IRestResponse response) 
			: base(message, request)
		{
			if(response == null) throw new ArgumentNullException(nameof(response));

			Response = response;
		}

		/// <inheritdoc />
		public FailedRestSharpRequestException(string message, Exception innerException, IHttpClientRequestContext request, IRestResponse response) 
			: base(message, innerException, request)
		{
			if(response == null) throw new ArgumentNullException(nameof(response));

			Response = response;
		}
	}
}
