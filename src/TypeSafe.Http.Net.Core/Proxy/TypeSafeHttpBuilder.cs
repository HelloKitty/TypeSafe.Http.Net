using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Builder for the type safe HTTP service.
	/// </summary>
	/// <typeparam name="TRestServiceInterface">The service type.</typeparam>
	public static class TypeSafeHttpBuilder<TRestServiceInterface>
		where TRestServiceInterface : class
	{
		/// <summary>
		/// Creates a new service builder that can be configured for
		/// REST/HTTP/Web use.
		/// </summary>
		/// <returns>A new builder for the <typeparamref name="TRestServiceInterface"/> service interface.</returns>
		public static RestServiceBuilder<TRestServiceInterface> Create()
		{
#pragma warning disable 618
			return new RestServiceBuilder<TRestServiceInterface>();
#pragma warning restore 618
		}
	}
}
