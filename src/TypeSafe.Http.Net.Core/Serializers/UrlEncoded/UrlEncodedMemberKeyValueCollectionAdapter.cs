using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	public class UrlEncodedMemberKeyValueCollectionAdapter : IEnumerable<KeyValuePair<string, string>>
	{
		private IEnumerable<UrlEncodedMember> EncodedMembers { get; }

		public object Model { get; }

		public UrlEncodedMemberKeyValueCollectionAdapter(IEnumerable<UrlEncodedMember> encodedMembers, object model)
		{
			if (encodedMembers == null) throw new ArgumentNullException(nameof(encodedMembers));
			if (model == null) throw new ArgumentNullException(nameof(model));

			EncodedMembers = encodedMembers;
			Model = model;
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			//Enumerate the encoded members in the format of a keyvalue dictionary.
			foreach(UrlEncodedMember m in EncodedMembers)
				yield return new KeyValuePair<string, string>(m.MemberName, m.ReflectionMediator.Access(Model)?.ToString());
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
