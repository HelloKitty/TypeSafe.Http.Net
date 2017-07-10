using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeSafe.Http.Net
{
	public sealed class NoParametersContext : IServiceCallParametersContext
	{
		/// <inheritdoc />
		public bool HasParameters => false;

		//We should mostly assume nobody will call this if we indicate that there are no parameters.
		/// <inheritdoc />
		public object[] Parameters => Enumerable.Empty<object>().ToArray();
	}
}
