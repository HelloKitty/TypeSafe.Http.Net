using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Similar to ASP Core's QueryString attribut that indicates a parameter is coming from a query string this metadata
	/// marker will indicate that a parameter should be inserted into the the querystring.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter)]
	public class QueryStringParameterAttribute : Attribute
	{
		//If you want to modify the name you should use the Alias attribute.
		//The functionality for binding the parameter to a specified named value
		//is not controlled exclusively by this attribute.
	}
}
