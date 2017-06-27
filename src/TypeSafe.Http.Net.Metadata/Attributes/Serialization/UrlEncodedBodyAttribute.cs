using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Metadata marker that indicates an object should be serialized in the body
	/// using url encoded semantics.
	/// </summary>
	public sealed class UrlEncodedBodyAttribute : BodyContentAttribute
	{
		public UrlEncodedBodyAttribute()
			: base()
		{
			
		}
	}
}
