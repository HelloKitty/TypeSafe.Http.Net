using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	public interface IRequestHeader
	{
		/// <summary>
		/// Indicates the header type.
		/// </summary>
		string HeaderType { get; }

		/// <summary>
		/// The fully built header value containing one or more
		/// values with the correct delimiters.
		/// </summary>
		string HeaderValue { get; }
	}
}
