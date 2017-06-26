using System;
using System.Collections.Generic;
using System.Text;

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
		void Write(byte[] bytes);

		/// <summary>
		/// Writes text/string data as the content of the request body.
		/// </summary>
		/// <param name="content"></param>
		void Write(string content);
	}
}
