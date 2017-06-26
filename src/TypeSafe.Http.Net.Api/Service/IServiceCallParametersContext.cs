using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net.Service
{
	/// <summary>
	/// Contract for a type that provides or manages the context of the parameters 
	/// for a particular service call.
	/// </summary>
	public interface IServiceCallParametersContext
	{
		/// <summary>
		/// Indicates if the service call has any parameters.
		/// If this is false it is likely <see cref="Parameters"/> is null or at least empty.
		/// </summary>
		bool HasParameters { get; }

		/// <summary>
		/// The parameters involved in the service call.
		/// </summary>
		object[] Parameters { get; }
	}
}
