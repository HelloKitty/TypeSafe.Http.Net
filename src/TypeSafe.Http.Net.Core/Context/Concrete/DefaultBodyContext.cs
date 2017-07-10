using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TypeSafe.Http.Net
{
	public sealed class DefaultBodyContext : IRequestBodyContext
	{
		/// <inheritdoc />
		public bool HasBody { get; } = true;

		/// <inheritdoc />
		public object Body { get; }

		public Type ContentAttributeType { get; }

		public DefaultBodyContext(object body, Type contentAttributeType)
		{
			if (contentAttributeType == null) throw new ArgumentNullException(nameof(contentAttributeType));

			//Also should ensure that the attribute inherits from the content body attribute
			if(!typeof(BodyContentAttribute).IsAssignableFrom(contentAttributeType))
				throw new ArgumentException($"Provided {nameof(BodyContentAttribute)} type does NOT inherit from {nameof(BodyContentAttribute)}.");

			//TODO: Should we check null?
			Body = body;
			ContentAttributeType = contentAttributeType;
		}
	}
}