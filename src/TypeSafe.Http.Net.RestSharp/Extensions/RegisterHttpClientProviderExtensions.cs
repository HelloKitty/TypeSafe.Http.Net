using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace TypeSafe.Http.Net
{
	public static class RegisterRestSharpProviderExtensions
	{
		/// <summary>
		/// Registers the <see cref="RestClient"/> as the underlying Http engine.
		/// </summary>
		/// <typeparam name="TClientRegisterationType"></typeparam>
		/// <param name="builder"></param>
		/// <param name="baseUrl">The base URL of the service.</param>
		/// <returns></returns>
		public static TClientRegisterationType RegisterRestSharpClient<TClientRegisterationType>(this TClientRegisterationType builder, string baseUrl)
			where TClientRegisterationType : IHttpClientServiceRegister
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));
			if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(baseUrl));

			//Just register the HttpClient in the non-fluent API.
			builder.Register(new DefaultRestSharpHttpServiceProxy(baseUrl));

			//fluently return
			return builder;
		}
/*
		/// <summary>
		/// Registers the .NET 4.5 <see cref="HttpClient"/> as the underlying Http engine.
		/// </summary>
		/// <typeparam name="TClientRegisterationType"></typeparam>
		/// <param name="builder"></param>
		/// <param name="baseUrl">The base URL of the service.</param>
		/// <param name="messageHandler"></param>
		/// <returns></returns>
		public static TClientRegisterationType RegisterDotNetHttpClient<TClientRegisterationType>(this TClientRegisterationType builder, string baseUrl, HttpMessageHandler messageHandler)
			where TClientRegisterationType : IHttpClientServiceRegister
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));
			if (messageHandler == null) throw new ArgumentNullException(nameof(messageHandler));
			if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(baseUrl));

			//Just register the HttpClient in the non-fluent API.
			builder.Register(new DefaultHttpClientHttpServiceProxy(baseUrl, messageHandler));

			//fluently return
			return builder;
		}

#if !NET45

		/// <summary>
		/// Registers the .NET 4.5 <see cref="HttpClient"/> as the underlying Http engine.
		/// </summary>
		/// <typeparam name="TClientRegisterationType"></typeparam>
		/// <param name="builder"></param>
		/// <param name="baseUrl">The base URL of the service.</param>
		/// <param name="messageHandler"></param>
		/// <returns></returns>
		public static TClientRegisterationType RegisterDotNetHttpClient<TClientRegisterationType>(this TClientRegisterationType builder, string baseUrl, SslProtocols supportedSslProtocols)
			where TClientRegisterationType : IHttpClientServiceRegister
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));
			if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(baseUrl));
			if (!Enum.IsDefined(typeof(SslProtocols), supportedSslProtocols)) throw new ArgumentOutOfRangeException(nameof(supportedSslProtocols), "Value should be defined in the SslProtocols enum.");

			//Just register the HttpClient in the non-fluent API.
			builder.Register(new DefaultHttpClientHttpServiceProxy(baseUrl, new HttpClientHandler() { SslProtocols = supportedSslProtocols }));

			//fluently return
			return builder;
		}
#endif

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

		/// <summary>
		/// Registers the .NET 4.5 <see cref="HttpClient"/> as the underlying Http engine.
		/// </summary>
		/// <typeparam name="TClientRegisterationType"></typeparam>
		/// <param name="builder"></param>
		/// <param name="baseUrlFuture">The <see cref="Task"/> based URL future for the service.</param>
		/// <param name="messageHandler"></param>
		/// <returns></returns>
		public static TClientRegisterationType RegisterDotNetHttpClient<TClientRegisterationType>(this TClientRegisterationType builder, Task<string> baseUrlFuture, HttpMessageHandler messageHandler)
			where TClientRegisterationType : IHttpClientServiceRegister
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));
			if (baseUrlFuture == null) throw new ArgumentNullException(nameof(baseUrlFuture));
			if (messageHandler == null) throw new ArgumentNullException(nameof(messageHandler));

			//Just register the HttpClientAsyncBaseUrl in the non-fluent API.
			builder.Register(new HttpClientAsyncEndpointHttpServiceProxy(baseUrlFuture, messageHandler));

			//fluently return
			return builder;
		}

#if !NET45

		/// <summary>
		/// Registers the .NET 4.5 <see cref="HttpClient"/> as the underlying Http engine.
		/// </summary>
		/// <typeparam name="TClientRegisterationType"></typeparam>
		/// <param name="builder"></param>
		/// <param name="baseUrlFuture">The <see cref="Task"/> based URL future for the service.</param>
		/// <param name="messageHandler"></param>
		/// <param name="supportedSslProtocols"></param>
		/// <returns></returns>
		public static TClientRegisterationType RegisterDotNetHttpClient<TClientRegisterationType>(this TClientRegisterationType builder, Task<string> baseUrlFuture, SslProtocols supportedSslProtocols)
			where TClientRegisterationType : IHttpClientServiceRegister
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));
			if (baseUrlFuture == null) throw new ArgumentNullException(nameof(baseUrlFuture));
			if (!Enum.IsDefined(typeof(SslProtocols), supportedSslProtocols)) throw new ArgumentOutOfRangeException(nameof(supportedSslProtocols), "Value should be defined in the SslProtocols enum.");

			//Just register the HttpClientAsyncBaseUrl in the non-fluent API.
			builder.Register(new HttpClientAsyncEndpointHttpServiceProxy(baseUrlFuture, new HttpClientHandler(){ SslProtocols = supportedSslProtocols }));

			//fluently return
			return builder;
		}
#endif*/
	}
}
