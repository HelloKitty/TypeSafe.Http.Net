using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	public sealed class NoBodyContext : IRequestBodyContext
	{
		/// <summary>
		/// Static referencable instance of the <see cref="NoBodyContext"/>.
		/// </summary>
		public static IRequestBodyContext Instance { get; } = new NoBodyContext();

		/// <inheritdoc />
		public bool HasBody { get; } = false;

		/// <inheritdoc />
		public object Body { get; } = null;

		/// <inheritdoc />
		public Type ContentAttributeType { get; } = null;
	}
}
