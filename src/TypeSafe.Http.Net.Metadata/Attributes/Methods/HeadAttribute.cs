using System;
using System.Net.Http;
using TypeSafe.Http.Net;

namespace TypeSafe.HTTP.NET.Metadata.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class HeadAttribute : HttpBaseMethodAttribute
	{
		/// <inheritdoc />
		public HeadAttribute(string path)
			: base(path, HttpMethod.Head)
		{

		}
	}
}