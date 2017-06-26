using System;
using System.Net.Http;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// The <see cref="HttpMethod"/>.Delete version of the
	/// <see cref="HttpBaseMethodAttribute"/>.
	/// </summary>
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