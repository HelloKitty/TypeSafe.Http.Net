using System;
using System.Net.Http;
using TypeSafe.Http.Net;

namespace TypeSafe.HTTP.NET.Metadata.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class DeleteAttribute : HttpBaseMethodAttribute
	{
		/// <inheritdoc />
		public DeleteAttribute(string path)
			: base(path, HttpMethod.Delete)
		{

		}
	}
}