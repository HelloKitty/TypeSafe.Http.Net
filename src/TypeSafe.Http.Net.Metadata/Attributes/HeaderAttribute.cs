using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Metadata marker that indicates a specified header should be added to the request.
	/// Multiple header attributes will be combined on interfaces or methods. This fits the functionality of MOST header behaviors
	/// in the HTTP specification. These attributes work like key value pairs where the <see cref="HeaderType"/> is the key and the <see cref="_Values"/> 
	/// is a value collection of header values.
	/// 
	/// Multiple values on a header key type will be seperated with commas. Use a more specific attribute or
	/// inherit from this attribute to implement seperate seperator functionality.
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class HeaderAttribute : Attribute
	{
		/// <summary>
		/// The type of the header.
		/// </summary>
		public string HeaderType { get; }

		/// <summary>
		/// Internally managed mutable collection of header values.
		/// </summary>
		private string[] _Values { get; }

		/// <summary>
		/// The comma non-delimited collection of values.
		/// </summary>
		public IEnumerable<string> Values => _Values;

		//To avoid virutal calls in constructors we lazy evaluate
		private Lazy<string> _ValueString { get; }

		/// <summary>
		/// The built value string for the header.
		/// </summary>
		public string ValueString => _ValueString.Value;

		public HeaderAttribute(string headerType, params string[] values)
		{
			if (values == null) throw new ArgumentNullException(nameof(values));
			if (!values.Any()) throw new ArgumentException($"Provided {nameof(values)} for {nameof(HeaderAttribute)} did not contain any values.");
			if (string.IsNullOrWhiteSpace(headerType)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(headerType));

			HeaderType = headerType;
			_Values = values;
			_ValueString = new Lazy<string>(BuildValueString);
		}

		/// <summary>
		/// Overriding method that builds the value string with the delimiter specified.
		/// This can be overriden to apply more advanced concatenation for the format of more complex
		/// or non-traditional headers.
		/// </summary>
		/// <returns>The built value string.</returns>
		protected virtual string BuildValueString()
		{
			//It is always ensured that the collection has SOME values.
			//Don't check for none

			//Always apprend the first one so we can easily add delimiters
			return Values.Skip<string>(1)
				.Aggregate(new StringBuilder().Append(Values.First<string>()), (stringBuilder, s) => stringBuilder.Append($",{s}"))
				.ToString();
		}
	}
}