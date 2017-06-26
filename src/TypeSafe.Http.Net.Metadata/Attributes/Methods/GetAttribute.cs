using System;
using System.Net.Http;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// The <see cref="HttpMethod"/>.Get version of the
	/// <see cref="HttpBaseMethodAttribute"/>.
	/// </summary>
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