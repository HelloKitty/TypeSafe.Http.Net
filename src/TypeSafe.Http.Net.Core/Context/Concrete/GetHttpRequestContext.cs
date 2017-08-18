using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TypeSafe.Http.Net
{
	public sealed class GetHttpRequestContext : HttpRequestContext
	{
		public GetHttpRequestContext(string builtActionPath, IEnumerable<IRequestHeader> headers, ISupressedErrorCodeContext supressedCodesContext)
			: base(HttpMethod.Get, builtActionPath, headers, NoBodyContext.Instance, supressedCodesContext)
		{

		}
	}
}
