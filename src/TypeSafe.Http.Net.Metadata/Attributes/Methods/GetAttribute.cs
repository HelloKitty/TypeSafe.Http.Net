using System;
using System.Net.Http;
using TypeSafe.Http.Net;

namespace TypeSafe.HTTP.NET.Metadata.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class GetAttribute : HttpBaseMethodAttribute
	{
		/// <inheritdoc />
		public GetAttribute(string path) 
			: base(path, HttpMethod.Get)
		{

		}
	}
}