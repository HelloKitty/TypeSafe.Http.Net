using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// This metadata indicates that response error codes from responses. Usecases may include wanting
	/// to deserialize JSON error response messages when a request fails. The default behaviour of throwing on response
	/// codes that are in the error range may want to be surpressed.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class SupressResponseErrorCodesAttribute : Attribute
	{
		/// <summary>
		/// Map that indicates if a code is supressed.
		/// All values are always initialized.
		/// </summary>
		public IReadOnlyDictionary<int, bool> SupressedCodes { get; }

		public SupressResponseErrorCodesAttribute(params int[] codesToSurpress)
		{
			//Just create a special dictionary that will indicate if a code is supressed or not
			//then add the supressed codes
			ErrorCodeSupressionDictionary tempSupressedCodes = new ErrorCodeSupressionDictionary();

			foreach(int i in codesToSurpress)
				tempSupressedCodes[i] = true;

			SupressedCodes = tempSupressedCodes;
		}

		private class ErrorCodeSupressionDictionary : IDictionary<int, bool>, IReadOnlyDictionary<int, bool>
		{
			private IDictionary<int, bool> InternallyManagedDictionary { get; }

			public ErrorCodeSupressionDictionary()
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
				return ((IEnumerable) InternallyManagedDictionary).GetEnumerator();
			}

			/// <inheritdoc />
			public void Add(KeyValuePair<int, bool> item)
			{
				InternallyManagedDictionary.Add(item);
			}

			/// <inheritdoc />
			public void Clear()
			{
				InternallyManagedDictionary.Clear();
			}

			/// <inheritdoc />
			public bool Contains(KeyValuePair<int, bool> item)
			{
				return InternallyManagedDictionary.Contains(item);
			}

			/// <inheritdoc />
			public void CopyTo(KeyValuePair<int, bool>[] array, int arrayIndex)
			{
				InternallyManagedDictionary.CopyTo(array, arrayIndex);
			}

			/// <inheritdoc />
			public bool Remove(KeyValuePair<int, bool> item)
			{
				return InternallyManagedDictionary.Remove(item);
			}

			/// <inheritdoc />
			public int Count => InternallyManagedDictionary.Count;

			/// <inheritdoc />
			public bool IsReadOnly => InternallyManagedDictionary.IsReadOnly;

			/// <inheritdoc />
			public void Add(int key, bool value)
			{
				InternallyManagedDictionary.Add(key, value);
			}

			/// <inheritdoc />
			public bool ContainsKey(int key)
			{
				//overide this to indicate we always have the key
				return true;
			}

			/// <inheritdoc />
			public bool Remove(int key)
			{
				return InternallyManagedDictionary.Remove(key);
			}

			/// <inheritdoc />
			public bool TryGetValue(int key, out bool value)
			{
				if(InternallyManagedDictionary.ContainsKey(key))
					return InternallyManagedDictionary.TryGetValue(key, out value);

				value = false;
				return true;
			}

			/// <inheritdoc />
			public bool this[int key]
			{
				get => InternallyManagedDictionary.ContainsKey(key) && InternallyManagedDictionary[key];
				set => InternallyManagedDictionary[key] = value;
			}

			/// <inheritdoc />
			IEnumerable<int> IReadOnlyDictionary<int, bool>.Keys => Keys;

			/// <inheritdoc />
			IEnumerable<bool> IReadOnlyDictionary<int, bool>.Values => Values;

			/// <inheritdoc />
			public ICollection<int> Keys => InternallyManagedDictionary.Keys;

			/// <inheritdoc />
			public ICollection<bool> Values => InternallyManagedDictionary.Values;
		}
	}
}
