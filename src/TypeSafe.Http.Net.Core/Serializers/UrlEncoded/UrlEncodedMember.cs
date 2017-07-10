using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	public sealed class UrlEncodedMember
	{
		public string MemberName { get; }

		public MemberReflectionTypeMediator ReflectionMediator { get; }

		public UrlEncodedMember(MemberReflectionTypeMediator reflectionMediator, string memberName)
		{
			if (reflectionMediator == null) throw new ArgumentNullException(nameof(reflectionMediator));
			if (string.IsNullOrWhiteSpace(memberName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(memberName));

			ReflectionMediator = reflectionMediator;
			MemberName = memberName;
		}
	}
}
