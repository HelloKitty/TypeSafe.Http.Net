using System;
using System.Net.Http;
using TypeSafe.Http.Net;

namespace TypeSafe.HTTP.NET.Metadata.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class PutAttribute : HttpBaseMethodAttribute
	{
		/// <inheritdoc />
		public PutAttribute(string path)
			: base(path, HttpMethod.Put)
		{

		}
	}
}