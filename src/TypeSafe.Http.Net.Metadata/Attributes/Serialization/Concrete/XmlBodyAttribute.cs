using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Metadata marker that indicates an object should be serialized in the body
	/// using xml serialization semantics.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class XmlBodyAttribute : BodyContentAttribute
	{
		public XmlBodyAttribute()
			: base()
		{
			
		}
	}
}
