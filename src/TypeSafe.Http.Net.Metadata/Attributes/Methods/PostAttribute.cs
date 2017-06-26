using System;
using System.Net.Http;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// The <see cref="HttpMethod"/>.Post version of the
	/// <see cref="HttpBaseMethodAttribute"/>.
	/// </summary>
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