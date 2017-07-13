using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	public static class RegisterHttpClientProviderExtensions
	{
		public static TClientRegisterationType RegisterDotNetHttpClient<TClientRegisterationType>(this TClientRegisterationType builder, string baseUrl)
			where TClientRegisterationType : IHttpClientServiceRegister
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));
			if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(baseUrl));

			//Just register the HttpClient in the non-fluent API.
			builder.Register(new HttpClientHttpServiceProxy(baseUrl));

			//fluently return
			return builder;
		}
	}
}
