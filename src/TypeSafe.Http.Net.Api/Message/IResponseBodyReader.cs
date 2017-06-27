using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for types that can read response bodies.
	/// </summary>
	public interface IResponseBodyReader
	{
		/// <summary>
		/// Reads the response body asyncronously as a <see cref="byte[]"/>
		/// </summary>
		/// <returns>The read <see cref="byte[]"/></returns>
		Task<byte[]> ReadAsBytesAsync();

		/// <summary>
		/// Reads the response body asyncronously as a <see cref="string"/>
		/// </summary>
		/// <returns>The read <see cref="string"/></returns>
		Task<string> ReadAsStringAsync();

		/// <summary>
		/// Reads the response body as a <see cref="byte[]"/>
		/// </summary>
		/// <returns>The read <see cref="byte[]"/></returns>
		byte[] ReadAsBytes();

		/// <summary>
		/// Reads the response body as a <see cref="string"/>
		/// </summary>
		/// <returns>The read <see cref="string"/></returns>
		string ReadAsString();
	}
}
