using System;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Metadata attribute that marks a parameter or property to be the specified name
	/// instead of the name of the attribute or property as-is.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class AliasAsAttribute : Attribute
	{
		/// <summary>
		/// The name to alias to.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Creates a new marker for changing the name of a parameter or property name
		/// to the specified <see cref="name"/>
		/// </summary>
		/// <param name="name">The new name to change the property or parmeter to.</param>
		public AliasAsAttribute(string name)
		{
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException($"You must specified a valid {nameof(name)} for the {nameof(AliasAsAttribute)} attribute.", nameof(name));

			Name = name;
		}
	}
}