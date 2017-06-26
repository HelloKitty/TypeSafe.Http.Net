using System;
using System.Net.Http;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// The <see cref="HttpMethod"/>.Put version of the
	/// <see cref="HttpBaseMethodAttribute"/>.
	/// </summary>
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