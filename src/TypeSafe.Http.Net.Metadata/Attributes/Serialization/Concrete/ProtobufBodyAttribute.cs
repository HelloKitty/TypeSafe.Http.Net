using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Metadata marker that indicates an object should be serialized in the body
	/// using Google Protocol Buffers serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class ProtobufBodyAttribute : BodyContentAttribute
	{
		public ProtobufBodyAttribute()
			: base()
		{

		}
	}
}
