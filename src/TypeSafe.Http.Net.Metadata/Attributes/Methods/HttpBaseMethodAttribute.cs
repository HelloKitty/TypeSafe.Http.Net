using System;
using System.Net.Http;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Base metadata type for all HTTP method types.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public abstract class HttpBaseMethodAttribute : Attribute
	{
		/// <summary>
		/// Indicates the HTTP method used.
		/// </summary>
		public HttpMethod Method { get; }

		/// <summary>
		/// The endpoint path. This should not include the base path.
		/// (Ex. /api/auth)
		/// </summary>
		public string Path { get; }

		protected HttpBaseMethodAttribute(string path, HttpMethod method)
		{
			if (method == null) throw new ArgumentNullException(nameof(method));
			if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException($"The path for the {GetType().FullName} attribute must not be null or empty.", nameof(path));

			Path = path;
			Method = method;
		}
	}
}