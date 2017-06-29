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

		static RestServiceBuilder()
		{
			//Enforce that it is in an interface.
			if(!typeof(TRestServiceInterface).GetTypeInfo().IsInterface)
				throw new NotImplementedException($"Cannot create service proxy for non interfaces. Type: {typeof(TRestServiceInterface).FullName} is not an interface type.");
		}

		public RestServiceBuilder()
		{
			
		}

		/// <inheritdoc />
		public TRestServiceInterface Build()
		{
			//I can't think of a good reason we shouldn't allow multiple to be built.
			//so we won't prevent multiple calls to build.
			return new ProxyGenerator()
				.CreateInterfaceProxyWithoutTarget<TRestServiceInterface>(new RestServiceCallAsyncCallInterceptor(new RequestContextFactory(new HeaderServiceCallInterpreter()), Client).ToInterceptor());
		}

		/// <inheritdoc />
		public bool Register<TBodySerializerMetadataType, TResponseDeserializerMetadataType, TSerializerType>(TSerializerType serializationService) 
			where TBodySerializerMetadataType : BodyContentAttribute 
			where TResponseDeserializerMetadataType : ResponseContentAttribute 
			where TSerializerType : IResponseDeserializationStrategy, IRequestSerializationStrategy
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void Register(IRestServiceProxy proxy)
		{
			//TODO: Should we throw if a client is already set?
			Client = proxy;
		}
	}
}
