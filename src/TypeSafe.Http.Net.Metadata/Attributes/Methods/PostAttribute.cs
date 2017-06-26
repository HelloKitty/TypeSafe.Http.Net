using System;
using System.Net.Http;
using TypeSafe.Http.Net;

namespace TypeSafe.HTTP.NET.Metadata.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class PostAttribute : HttpBaseMethodAttribute
	{
		/// <inheritdoc />
		public PostAttribute(string path)
			: base(path, HttpMethod.Post)
		{

		}
	}
}