using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestSharp;

namespace TypeSafe.Http.Net
{
	internal class RestSharpRequestBodyWriter : IRequestBodyWriter
	{
		public RestRequest Request { get; }

		public RestSharpRequestBodyWriter(RestRequest request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));

			Request = request;
		}

		/// <inheritdoc />
		public void Write(byte[] bytes, string contentTypeValue)
		{
			Request.AddParameter(contentTypeValue, bytes, ParameterType.RequestBody);
		}

		/// <inheritdoc />
		public void Write(string content, string contentTypeValue)
		{
			Request.AddParameter(contentTypeValue, content, ParameterType.RequestBody);
		}

		/// <inheritdoc />
		public async Task WriteAsync(byte[] bytes, string contentTypeValue)
		{
			Request.AddParameter(contentTypeValue, bytes, ParameterType.RequestBody);

#if NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NET46
			return Task.CompletedTask;
#endif

#if NET45
			return Task.FromResult(0);
#endif
		}

		/// <inheritdoc />
		public async Task WriteAsync(string content, string contentTypeValue)
		{
			Request.AddParameter(contentTypeValue, content, ParameterType.RequestBody);

#if NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NET46
			return Task.CompletedTask;
#endif

#if NET45
			return Task.FromResult(0);
#endif
		}
	}
}