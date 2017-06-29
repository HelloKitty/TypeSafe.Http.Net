using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Adapter for the castle core <see cref="IInvocation"/> adapted to the
	/// <see cref="IServiceCallContext"/> interface.
	/// </summary>
	public sealed class CastleCoreInvocationCallContextAdapter : IServiceCallContext
	{
		//TODO: Why do we have to do this? TargetType is null, but shouldn't we have a target?
		/// <inheritdoc />
		public Type ServiceType => Invocation.Method.DeclaringType;

		/// <inheritdoc />
		public MethodInfo ServiceMethod => Invocation.Method;

		/// <summary>
		/// The managed castle core invocation.
		/// </summary>
		private IInvocation Invocation { get; }

		public CastleCoreInvocationCallContextAdapter(IInvocation invocation)
		{
			if (invocation == null) throw new ArgumentNullException(nameof(invocation));
			//if (invocation.TargetType == null) throw new InvalidOperationException("Provided invocation does not contain a valid TargetType.");

			Invocation = invocation;
		}
	}
}
