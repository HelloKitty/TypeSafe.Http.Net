using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for types that are associated with a 0 to n
	/// many content types.
	/// </summary>
	public interface IContentTypeAssociable
	{
		/// <summary>
		/// The associated types.
		/// Possibly empty but never null.
		/// </summary>
		IEnumerable<string> AssociatedContentType { get; }
	}
}
