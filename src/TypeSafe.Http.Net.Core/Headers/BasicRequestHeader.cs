using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	public sealed class BasicRequestHeader : IRequestHeader
	{
		/// <inheritdoc />
		public string HeaderType { get; }

		/// <inheritdoc />
		public string HeaderValue { get; }

		public BasicRequestHeader(string headerType, string headerValue)
		{
			if (string.IsNullOrWhiteSpace(headerType)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(headerType));
			if (string.IsNullOrWhiteSpace(headerValue)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(headerValue));

			HeaderType = headerType;
			HeaderValue = headerValue;
		}
	}
}
