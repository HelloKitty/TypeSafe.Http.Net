using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TypeSafe.Http.Net
{
	public sealed class GetRestRequestContext : IRestClientRequestContext
	{
		/// <inheritdoc />
		public string ActionPath { get; }

		/// <inheritdoc />
		public HttpMethod RequestMethod => HttpMethod.Get;

		/// <inheritdoc />
		public IEnumerable<IRequestHeader> RequestHeaders { get; }

		/// <inheritdoc />
		public bool HasBody => false;

		public GetRestRequestContext(string builtActionPath, IEnumerable<IRequestHeader> headers)
		{
			if (headers == null) throw new ArgumentNullException(nameof(headers));
			if (string.IsNullOrWhiteSpace(builtActionPath)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(builtActionPath));

			ActionPath = builtActionPath;
			RequestHeaders = headers;
		}

		/// <inheritdoc />
		public bool WriteBody(IRequestBodyWriter writer)
		{
			//We don't do anything to a body with a get request.
			return false;
		}
	}
}
