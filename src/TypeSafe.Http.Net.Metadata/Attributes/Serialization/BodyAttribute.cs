using System;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Base attribute that controls how a body is serialized.
	/// You should inherit this attribute to enable serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter)]
	public abstract class BodyAttribute : Attribute
	{
		protected BodyAttribute()
		{

		}
	}
}