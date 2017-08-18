using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	public sealed class NoErrorCodesSupressedContext : ISupressedErrorCodeContext
	{
		/// <inheritdoc />
		public IReadOnlyDictionary<int, bool> SupressedErrorCodes { get; } = new InternalEmptySupressionCodeDictionary();

		private class InternalEmptySupressionCodeDictionary : IReadOnlyDictionary<int, bool>
		{
			private Dictionary<int, bool> InternallyManagedDictionary { get; }

			public InternalEmptySupressionCodeDictionary()
			{
				InternallyManagedDictionary = new Dictionary<int, bool>();
			}

			/// <inheritdoc />
			public IEnumerator<KeyValuePair<int, bool>> GetEnumerator()
			{
				return InternallyManagedDictionary.GetEnumerator();
			}

			/// <inheritdoc />
			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			/// <inheritdoc />
			public int Count => 0;

			/// <inheritdoc />
			public bool ContainsKey(int key)
			{
				return true;
			}

			/// <inheritdoc />
			public bool TryGetValue(int key, out bool value)
			{
				value = false;
				return true;
			}

			/// <inheritdoc />
			public bool this[int key] => false;

			/// <inheritdoc />
			public IEnumerable<int> Keys => InternallyManagedDictionary.Keys;

			/// <inheritdoc />
			public IEnumerable<bool> Values => InternallyManagedDictionary.Values;
		}
	}
}
