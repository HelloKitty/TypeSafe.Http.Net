using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	public sealed class ContentSerializationFactory : ISerializationStrategyRegister, ISerializationStrategyFactory, IDeserializationStrategyFactory
	{
		private IDictionary<Type, IRequestSerializationStrategy> SerializationContentTypeAttributeMap { get; }

		public ContentSerializationFactory()
		{
			SerializationContentTypeAttributeMap = new Dictionary<Type, IRequestSerializationStrategy>();
		}

		/// <inheritdoc />
		public bool Register<TBodySerializerMetadataType, TSerializerType>(TSerializerType serializationService)
			where TBodySerializerMetadataType : BodyContentAttribute 
			where TSerializerType : IResponseDeserializationStrategy, IRequestSerializationStrategy, IContentTypeAssociable
		{
			//TODO: Should we throw encountering registeration conflicts?
			//We take the approach of overriding registeration
			//We can't personally resolve registeration conflicts but MAYBE we should throw? Undecided
			//SerializationContentTypeAttributeMap[typeof(TBodySerializerMetadataType)]

			return true;
		}

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
			throw new NotImplementedException();
		}
	}
}
