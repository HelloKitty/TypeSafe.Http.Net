using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	public static class RegisterHttpClientProviderExtensions
	{
		/// <summary>
		/// Registers the .NET 4.5 <see cref="HttpClient"/> as the underlying Http engine.
		/// </summary>
		/// <typeparam name="TClientRegisterationType"></typeparam>
		/// <param name="builder"></param>
		/// <param name="baseUrl">The base URL of the service.</param>
		/// <returns></returns>
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

		/// <summary>
		/// Registers the .NET 4.5 <see cref="HttpClient"/> as the underlying Http engine.
		/// </summary>
		/// <typeparam name="TClientRegisterationType"></typeparam>
		/// <param name="builder"></param>
		/// <param name="baseUrlFuture">The <see cref="Task"/> based URL future for the service.</param>
		/// <returns></returns>
		public static TClientRegisterationType RegisterDotNetHttpClient<TClientRegisterationType>(this TClientRegisterationType builder, Task<string> baseUrlFuture)
			where TClientRegisterationType : IHttpClientServiceRegister
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));
			if (baseUrlFuture == null) throw new ArgumentNullException(nameof(baseUrlFuture));

			//Just register the HttpClientAsyncBaseUrl in the non-fluent API.
			builder.Register(new HttpClientAsyncEndpointHttpServiceProxy(baseUrlFuture));

			//fluently return
			return builder;
		}
	}
}
