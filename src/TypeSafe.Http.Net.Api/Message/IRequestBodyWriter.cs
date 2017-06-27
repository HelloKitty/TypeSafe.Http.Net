using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for types that can write body data.
	/// </summary>
	public interface IRequestBodyWriter
	{
		/// <summary>
		/// Writes binary bytes as the content of the request body.
		/// </summary>
		/// <param name="bytes">The bytes to write.</param>
		/// <param name="contentTypeValue">The value to use as the content type.</param>
		void Write(byte[] bytes, string contentTypeValue);

		/// <summary>
		/// Writes text/string data as the content of the request body.
		/// </summary>
		/// <param name="content"></param>
		/// <param name="contentTypeValue">The value to use as the content type.</param>
		void Write(string content, string contentTypeValue);

		/// <summary>
		/// Writes binary bytes asyncronously as the content of the request body.
		/// </summary>
		/// <param name="bytes">The bytes to write.</param>
		/// <param name="contentTypeValue">The value to use as the content type.</param>
		Task WriteAsync(byte[] bytes, string contentTypeValue);

		/// <summary>
		/// Writes text/string data asyncronously as the content of the request body.
		/// </summary>
		/// <param name="content"></param>
		/// <param name="contentTypeValue">The value to use as the content type.</param>
		Task WriteAsync(string content, string contentTypeValue);
	}
}
