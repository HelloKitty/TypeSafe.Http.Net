using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace TypeSafe.Http.Net
{
	public sealed class HttpMethodReader
	{
		/// <summary>
		/// The HTTP method read
		/// </summary>
		public HttpMethod ReadHttpMethod => CallContext.ServiceMethod
			.GetCustomAttribute<HttpBaseMethodAttribute>()?.Method;

		public IServiceCallContext CallContext { get; }

		public static HttpMethodReader Create(IServiceCallContext callContext)
		{
			return new HttpMethodReader(callContext);
		}

		public HttpMethodReader(IServiceCallContext callContext)
		{
			if (callContext == null) throw new ArgumentNullException(nameof(callContext));
			CallContext = callContext;
		}
	}
}
