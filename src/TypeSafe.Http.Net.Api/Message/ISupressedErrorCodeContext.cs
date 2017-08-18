using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Service interface that provides information about supressed error codes.
	/// </summary>
	public interface ISupressedErrorCodeContext
	{
		/// <summary>
		/// The supressed error codes.
		/// </summary>
		IReadOnlyDictionary<int, bool> SupressedErrorCodes { get; }
	}
}
