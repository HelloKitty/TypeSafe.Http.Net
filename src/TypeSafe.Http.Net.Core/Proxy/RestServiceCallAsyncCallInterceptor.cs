using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace TypeSafe.Http.Net
{
	public sealed class RestServiceCallAsyncCallInterceptor : IAsyncInterceptor
	{
		/// <summary>
		/// The factory for building contexts.
		/// </summary>
		private IRequestContextFactory RequestContextFactory { get; }

		private IRestServiceProxy ProxyClient { get; }

		public RestServiceCallAsyncCallInterceptor(IRequestContextFactory requestContextFactory, IRestServiceProxy proxyClient)
		{
			if (requestContextFactory == null) throw new ArgumentNullException(nameof(requestContextFactory));
			if (proxyClient == null) throw new ArgumentNullException(nameof(proxyClient));

			RequestContextFactory = requestContextFactory;
			ProxyClient = proxyClient;
		}

		/// <inheritdoc />
		public void InterceptSynchronous(IInvocation invocation)
		{
			throw new InvalidOperationException($"TypeSafe.Http.Net only supports async. Type: {invocation.TargetType.FullName} Method: {invocation.Method.Name} must have a return type of Task or Task<T>.");
		}

		/// <inheritdoc />
		public void InterceptAsynchronous(IInvocation invocation)
		{
			//Per documentation of https://github.com/JSkimming/Castle.Core.AsyncInterceptor this will be called for Task based return type methods.
			//It will ONLY be called if it is of type Task and not Task<T> where no return value is expected.
			invocation.ReturnValue = AsyncNoReturn(invocation);
		}

		public async Task AsyncNoReturn(IInvocation invocation)
		{
			IRestClientRequestContext context = RequestContextFactory.CreateContext(new CastleCoreInvocationCallContextAdapter(invocation),
				new CastleCoreInvocationParametersContextAdapter(invocation));

			//Because we don't need to get any returned information we can just send it
			await ProxyClient.Send(context);
		}

		public async Task AsyncWithReturn<TResult>(IInvocation invocation)
		{
			//TODO: Handle return data deserialization.
			IRestClientRequestContext context = RequestContextFactory.CreateContext(new CastleCoreInvocationCallContextAdapter(invocation),
				new CastleCoreInvocationParametersContextAdapter(invocation));

			//Because we don't need to get any returned information we can just send it
			await ProxyClient.Send(context);
		}

		/// <inheritdoc />
		public void InterceptAsynchronous<TResult>(IInvocation invocation)
		{
			//Per documentation of https://github.com/JSkimming/Castle.Core.AsyncInterceptor this will be called for Task based return type methods
			//that are of type Task<T>
			invocation.ReturnValue = AsyncWithReturn<TResult>(invocation);
		}
	}
}
