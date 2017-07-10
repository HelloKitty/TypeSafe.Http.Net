using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Context for the body of the request.
	/// </summary>
	public interface IRequestBodyContext
	{
		/// <summary>
		/// Indicates if the request contains a body to write.
		/// </summary>
		bool HasBody { get; }

		/// <summary>
		/// The body for the request.
		/// </summary>
		object Body { get; }

		Type ContentAttributeType { get; }
	}
}
