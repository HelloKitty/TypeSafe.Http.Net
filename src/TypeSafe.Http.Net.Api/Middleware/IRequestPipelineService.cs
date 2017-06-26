using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for types that act on <see cref="IServiceCallContext"/> and <see cref="IServiceCallParametersContext"/>
	/// to produce results or interpret the information contained within them.
	/// </summary>
	public interface IRequestPipelineService<out TResultType>
	{
		/// <summary>
		/// Pipeline service that reads the provided <see cref="serviceContext"/> along with the provided <see cref="parameters"/>
		/// to create the specified <typeparamref name="TResultType"/>.
		/// </summary>
		/// <param name="serviceContext">The service context for the call.</param>
		/// <param name="parameters">The context of the parameters for this service call.</param>
		/// <returns>A non-null <typeparamref name="TResultType"/>.</returns>
		TResultType ProduceFromContext(IServiceCallContext serviceContext, IServiceCallParametersContext parameters);
	}
}
