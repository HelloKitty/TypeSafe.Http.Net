using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	public interface IDeserializationStrategyFactory
	{
		/// <summary>
		/// Creates a serialization strategy for the provided <see cref="contentType"/>.
		/// </summary>
		/// <param name="contentType">Non-null non-empty content type.</param>
		/// <returns>A request serialization strategy if found.</returns>
		IResponseDeserializationStrategy DeserializerFor(string contentType);
	}
}
