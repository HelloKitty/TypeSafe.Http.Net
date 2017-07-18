using System;
using System.Threading.Tasks;
using RestSharp;

namespace TypeSafe.Http.Net
{
	public class RestSharpResponseBodyReader : IResponseBodyReader
	{
		public IRestResponse Response { get; }

		public RestSharpResponseBodyReader(IRestResponse response)
		{
			Response = response;
		}

		/// <inheritdoc />
		public Task<byte[]> ReadAsBytesAsync()
		{
			return Task.FromResult(ReadAsBytes());
		}

		/// <inheritdoc />
		public Task<string> ReadAsStringAsync()
		{
			return Task.FromResult(ReadAsString());
		}

		/// <inheritdoc />
		public byte[] ReadAsBytes()
		{
			return Response.RawBytes;
		}

		/// <inheritdoc />
		public string ReadAsString()
		{
			return Response.Content;
		}
	}
}