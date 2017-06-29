using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Represents the context of a service call.
	/// (Ex. user calls serviceProxy.Authenticate(username, password) on its custom made IAuthServer interface
	/// so we package up contextual information for the service call so it is easily extended and passed around as the current 
	/// service request/call).
	/// </summary>
	public interface IServiceCallContext
	{
		Type ServiceType { get; }

		//The reason we have this instead of a raw string is that
		//if we don't provide the method info then we have to likely resolve the MethodInfo
		//down the line multiple times AND resolve it while considering potential overloads
		MethodInfo ServiceMethod { get; }
	}
}
