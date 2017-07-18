using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace TypeSafe.Http.Net
{
	public sealed class DefaultRestSharpHttpServiceProxy : RestSharpHttpServiceProxy
	{
		/// <inheritdoc />
		public override string BaseUrl { get; }

		/// <inheritdoc />
		protected override RestClient Client { get; }

		public DefaultRestSharpHttpServiceProxy(string baseUrl)
		{
			if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(baseUrl));

			BaseUrl = baseUrl;
			Client = new RestClient(baseUrl);
			Client.DefaultParameters.Clear();
		}
	}
}
