using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	public sealed class DefaultErrorCodeSupressedContext : ISupressedErrorCodeContext
	{
		/// <inheritdoc />
		public IReadOnlyDictionary<int, bool> SupressedErrorCodes { get; }

		public DefaultErrorCodeSupressedContext(IReadOnlyDictionary<int, bool> supressedErrorCodes)
		{
			if (supressedErrorCodes == null) throw new ArgumentNullException(nameof(supressedErrorCodes));

			SupressedErrorCodes = supressedErrorCodes;
		}
	}
}
