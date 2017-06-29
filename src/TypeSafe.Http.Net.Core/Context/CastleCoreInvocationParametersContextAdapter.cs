using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Adapter for the castle core <see cref="IInvocation"/> adapted to the
	/// <see cref="IServiceCallParametersContext"/> interface.
	/// </summary>
	public sealed class CastleCoreInvocationParametersContextAdapter : IServiceCallParametersContext
	{
		/// <inheritdoc />
		public bool HasParameters => Invocation.Arguments.Any();

		/// <inheritdoc />
		public object[] Parameters => Invocation.Arguments;

		/// <summary>
		/// The managed castle core invocation.
		/// </summary>
		private IInvocation Invocation { get; }

		public CastleCoreInvocationParametersContextAdapter(IInvocation invocation)
		{
			if (invocation == null) throw new ArgumentNullException(nameof(invocation));

			Invocation = invocation;
		}
	}
}
