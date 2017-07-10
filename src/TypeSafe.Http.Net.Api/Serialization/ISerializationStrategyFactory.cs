using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for services that provide serialization strategy creation.
	/// </summary>
	public interface ISerializationStrategyFactory
	{
		/// <summary>
		/// Creates a serialization strategy for the provided <see cref="contentType"/>.
		/// </summary>
		/// <param name="contentType">Non-null content type.</param>
		/// <returns>A request serialization strategy if found.</returns>
		IRequestSerializationStrategy SerializerFor(Type contentType);
	}
}
