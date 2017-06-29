using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Contract for a REST service builder.
	/// </summary>
	/// <typeparam name="TRestInterface">The rest service type.</typeparam>
	public interface IRestServiceProxyBuilder<out TRestInterface>
		where TRestInterface : class

	{
		/// <summary>
		/// Builds the REST service <see cref="TRestInterface"/>.
		/// </summary>
		/// <returns>An instance of the constructed REST service.</returns>
		TRestInterface Build();
	}
}
