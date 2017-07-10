using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TypeSafe.Http.Net
{
	public sealed class GetRestRequestContext : RestRequestContext
	{
		public GetRestRequestContext(string builtActionPath, IEnumerable<IRequestHeader> headers)
			: base(HttpMethod.Get, builtActionPath, headers, NoBodyContext.Instance)
		{

		}
	}
}
