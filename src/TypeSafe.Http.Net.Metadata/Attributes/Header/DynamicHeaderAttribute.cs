using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Metadata marker that indicates a specified header should be added to the request and that the value is not known until service call.
	/// Multiple header attributes will be combined on interfaces or methods. This fits the functionality of MOST header behaviors
	/// in the HTTP specification. These attributes work like key value pairs where the <see cref="HeaderType"/> is the key and the <see cref="_Values"/> 
	/// is a value collection of header values.
	/// 
	/// Multiple values on a header key type will be seperated with commas. Use a more specific attribute or
	/// inherit from this attribute to implement seperate seperator functionality.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter, Inherited = true)]
	public class DynamicHeaderAttribute : Attribute
	{
		/// <summary>
		/// The type of the header.
		/// </summary>
		public string HeaderType { get; }

		/// <summary>
		/// Dynamic header attribute that will get the header value from the parameter it is marked on.
		/// </summary>
		/// <param name="headerType">The header type.</param>
		public DynamicHeaderAttribute(string headerType)
		{
			if (string.IsNullOrWhiteSpace(headerType)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(headerType));

			HeaderType = headerType;
		}
	}
}