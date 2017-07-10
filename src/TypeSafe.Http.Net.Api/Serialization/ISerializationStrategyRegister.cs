using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for types that provide serialization registry.
	/// </summary>
	public interface ISerializationStrategyRegister
	{
		/// <summary>
		/// Registers a serialization service mapped to the provided attribute types.
		/// </summary>
		/// <typeparam name="TBodySerializerMetadataType"></typeparam>
		/// <typeparam name="TSerializerType">The serializer type.</typeparam>
		/// <param name="serializationService">The serialization service to map to the attributes.</param>
		/// <returns>True if the service was registered</returns>
		bool Register<TBodySerializerMetadataType, TSerializerType>(TSerializerType serializationService)
			where TBodySerializerMetadataType : BodyContentAttribute
			where TSerializerType : IResponseDeserializationStrategy, IRequestSerializationStrategy, IContentTypeAssociable;
	}
}
