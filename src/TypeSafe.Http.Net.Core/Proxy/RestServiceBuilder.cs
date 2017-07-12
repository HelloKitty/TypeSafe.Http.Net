using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;
using Castle.DynamicProxy.Contributors;

namespace TypeSafe.Http.Net
{
	public sealed class RestServiceBuilder<TRestServiceInterface> : IRestServiceProxyBuilder<TRestServiceInterface>, ISerializationStrategyRegister, IRestClientServiceRegister
		where TRestServiceInterface : class
	{
		/// <summary>
		/// The client to use.
		/// </summary>
		private IRestServiceProxy Client { get; set; }

		private ContentSerializationFactory SerializerFactory { get; }

		static RestServiceBuilder()
		{
			//Enforce that it is in an interface.
			if(!typeof(TRestServiceInterface).GetTypeInfo().IsInterface)
				throw new NotImplementedException($"Cannot create service proxy for non interfaces. Type: {typeof(TRestServiceInterface).FullName} is not an interface type.");
		}

		/// <summary>
		/// Creates a new service builder that can be configured for
		/// REST/HTTP/Web use.
		/// </summary>
		/// <returns>A new builder for the <typeparamref name="TRestServiceInterface"/> service interface.</returns>
		public static RestServiceBuilder<TRestServiceInterface> Create()
		{
			return new RestServiceBuilder<TRestServiceInterface>();
		}

		public RestServiceBuilder()
		{
			SerializerFactory = new ContentSerializationFactory();
		}

		/// <inheritdoc />
		public TRestServiceInterface Build()
		{
			//I can't think of a good reason we shouldn't allow multiple to be built.
			//so we won't prevent multiple calls to build.
			return new ProxyGenerator()
				.CreateInterfaceProxyWithoutTarget<TRestServiceInterface>(new RestServiceCallAsyncCallInterceptor(new RequestContextFactory(new HeaderServiceCallInterpreter()), Client, SerializerFactory, SerializerFactory).ToInterceptor());
		}

		/// <inheritdoc />
		public bool Register<TBodySerializerMetadataType, TSerializerType>(TSerializerType serializationService) 
			where TBodySerializerMetadataType : BodyContentAttribute 
			where TSerializerType : IResponseDeserializationStrategy, IRequestSerializationStrategy, IContentTypeAssociable
		{
			if (serializationService == null) throw new ArgumentNullException(nameof(serializationService));
			if (SerializerFactory == null) throw new InvalidOperationException($"Serialization factory is null and never should be.");

			//Just forward it to the factory
			return SerializerFactory.Register<TBodySerializerMetadataType, TSerializerType>(serializationService);
		}

		/// <inheritdoc />
		public void Register(IRestServiceProxy proxy)
		{
			//TODO: Should we throw if a client is already set?
			Client = proxy;
		}
	}
}
