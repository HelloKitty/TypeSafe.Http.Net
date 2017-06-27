using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Base attribute that controls how a body is serialized.
	/// You should inherit this attribute to enable serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.ReturnValue)]
	public abstract class ResponseContentAttribute : Attribute
	{
		protected ResponseContentAttribute()
		{

		}
	}
}
