using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	public sealed class HttpClientAsyncEndpointHttpServiceProxy : HttpClientHttpServiceProxy
	{
		/// <summary>
		/// Represents a future that will provide the base url.
		/// </summary>
		private Task<string> BaseUrlFuture { get; }

		/// <inheritdoc />
		public override string BaseUrl => BaseUrlFuture.Result;

		//TODO: This is bad design to leave this null even though we know we'll have it initialized before it's used.
		/// <inheritdoc />
		protected override HttpClient Client { get; }

		/// <summary>
		/// Interanl syncronization object.
		/// </summary>
		private readonly object SyncObj = new object();

		public HttpClientAsyncEndpointHttpServiceProxy(Task<string> baseUrlFuture)
			: base()
		{
			if (baseUrlFuture == null) throw new ArgumentNullException(nameof(baseUrlFuture));

			Client = new HttpClient();
			BaseUrlFuture = baseUrlFuture;
		}

		public HttpClientAsyncEndpointHttpServiceProxy(Task<string> baseUrlFuture, HttpMessageHandler messageHandler)
		{
			if (baseUrlFuture == null) throw new ArgumentNullException(nameof(baseUrlFuture));
			if (messageHandler == null) throw new ArgumentNullException(nameof(messageHandler));

			Client = new HttpClient(messageHandler);
			BaseUrlFuture = baseUrlFuture;
		}

		/// <inheritdoc />
		protected override async Task<HttpResponseMessage> SendBaseRequest(IHttpClientRequestContext requestContext, HttpRequestMessage request)
		{
			//TODO: C#7 can await in locks
			if (Client.BaseAddress == null)
			{
				string url = await BaseUrlFuture;
				lock (SyncObj)
				{
					if (Client.BaseAddress == null)
					{
						Client.BaseAddress = new Uri(url);
					}
				}
			}

			//At this point due to the above await we will have gotten the URI initialized and can send the request.
			return await base.SendBaseRequest(requestContext, request);
		}
	}
}
