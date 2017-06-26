using System;
using System.Net.Http;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// The <see cref="HttpMethod"/>.Head version of the
	/// <see cref="HttpBaseMethodAttribute"/>.
	/// </summary>
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