using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Builder for the type safe HTTP service.
	/// </summary>
	/// <typeparam name="THttpServiceInterface">The service type.</typeparam>
	public static class TypeSafeHttpBuilder<THttpServiceInterface>
		where THttpServiceInterface : class
	{
		/// <summary>
		/// Creates a new service builder that can be configured for
		/// REST/HTTP/Web use.
		/// </summary>
		/// <returns>A new builder for the <typeparamref name="THttpServiceInterface"/> service interface.</returns>
		public static RestServiceBuilder<THttpServiceInterface> Create()
		{
#pragma warning disable 618
			return new RestServiceBuilder<THttpServiceInterface>();
#pragma warning restore 618
		}
	}
}
