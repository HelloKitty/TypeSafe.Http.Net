using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Base attribute that controls how a return value can be deserialized
	/// You should inherit this attribute to enable return value serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.ReturnValue)]
	public abstract class ResponseContentAttribute : Attribute
	{
		protected ResponseContentAttribute()
		{

		}
	}
}
