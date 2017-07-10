using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Serializer/Deserializer factory that takes in initial registerations.
	/// </summary>
	public sealed class ContentSerializationFactory : ISerializationStrategyRegister, ISerializationStrategyFactory, IDeserializationStrategyFactory
	{
		/// <summary>
		/// Map containing the serializers.
		/// </summary>
		private IDictionary<Type, IRequestSerializationStrategy> SerializationContentTypeAttributeMap { get; }

		/// <summary>
		/// Map containing the deserializers.
		/// </summary>
		private IDictionary<string, IResponseDeserializationStrategy> DeserializationContentTypeStringMap { get; }

		public ContentSerializationFactory()
		{
			SerializationContentTypeAttributeMap = new Dictionary<Type, IRequestSerializationStrategy>();
			DeserializationContentTypeStringMap = new Dictionary<string, IResponseDeserializationStrategy>();
		}

		/// <inheritdoc />
		public bool Register<TBodySerializerMetadataType, TSerializerType>(TSerializerType serializationService)
			where TBodySerializerMetadataType : BodyContentAttribute 
			where TSerializerType : IResponseDeserializationStrategy, IRequestSerializationStrategy, IContentTypeAssociable
		{
			//TODO: Should we throw encountering registeration conflicts?
			//We take the approach of overriding registeration
			//We can't personally resolve registeration conflicts but MAYBE we should throw? Undecided
			SerializationContentTypeAttributeMap[typeof(TBodySerializerMetadataType)] = serializationService;

			foreach (string contentType in serializationService.AssociatedContentType)
				DeserializationContentTypeStringMap[contentType] = serializationService;

			return true;
		}

		//TODO: Generalize loading
		/// <inheritdoc />
		public IRequestSerializationStrategy SerializerFor(Type contentType)
		{
			if(!SerializationContentTypeAttributeMap.ContainsKey(contentType))
				throw new InvalidOperationException($"Requested serializer for ContentType: {contentType.FullName} but none were registered.");

			return SerializationContentTypeAttributeMap[contentType];
		}

		/// <inheritdoc />
		public IResponseDeserializationStrategy DeserializerFor(string contentType)
		{
			if (!DeserializationContentTypeStringMap.ContainsKey(contentType))
				throw new InvalidOperationException($"Requested deserializer for ContentType: {contentType} but none were registered.");

			return DeserializationContentTypeStringMap[contentType];
		}
	}
}
