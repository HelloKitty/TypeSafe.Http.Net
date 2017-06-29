using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for a factory that produces request contexts.
	/// </summary>
	public interface IRequestContextFactory
	{
		/// <summary>
		/// Creates a <see cref="IRestClientRequestContext"/> based on the context of the provided service call and parameters.
		/// </summary>
		/// <param name="callContext">The calling context.</param>
		/// <param name="parameterContext">The parameter context.</param>
		/// <returns></returns>
		IRestClientRequestContext CreateContext(IServiceCallContext callContext, IServiceCallParametersContext parameterContext);
	}
}
