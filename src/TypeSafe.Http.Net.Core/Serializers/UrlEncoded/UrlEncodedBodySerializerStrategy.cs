using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	public sealed class UrlEncodedBodySerializerStrategy : IRequestSerializationStrategy, IResponseDeserializationStrategy, IContentTypeAssociable
	{
		private IDictionary<Type, IEnumerable<UrlEncodedMember>> ModelTypeToReflectionMediatorCollection { get; }

		/// <inheritdoc />
		public IEnumerable<string> AssociatedContentType { get; }

		public UrlEncodedBodySerializerStrategy()
		{
			//The content type for Url encoded bodies is almost always ONLY that.
			AssociatedContentType = new string[] { @"application/x-www-form-urlencoded" };
			ModelTypeToReflectionMediatorCollection = new ConcurrentDictionary<Type, IEnumerable<UrlEncodedMember>>();
		}

		/// <inheritdoc />
		public bool TrySerialize(object content, IRequestBodyWriter writer)
		{
			if (content == null) throw new ArgumentNullException(nameof(content));

			//If we don't have a mediator collection prepared for the type we should create one.
			if (!ModelTypeToReflectionMediatorCollection.ContainsKey(content.GetType()))
			{
				ModelTypeToReflectionMediatorCollection[content.GetType()] = BuildUrlEncodedMemberCollection(content);

				return TrySerialize(content, writer);
			}

			UrlEncodedMemberKeyValueCollectionAdapter urlEncocedCollection = new UrlEncodedMemberKeyValueCollectionAdapter(ModelTypeToReflectionMediatorCollection[content.GetType()], content);

			writer.Write(new FormUrlEncodedContent(urlEncocedCollection).ReadAsStringAsync().Result, AssociatedContentType.First());

			return true;
		}

		private static IEnumerable<UrlEncodedMember> BuildUrlEncodedMemberCollection(object content)
		{
			return content.GetType()
				.GetMembers(BindingFlags.Public | BindingFlags.Instance)
				.Where(m => m is FieldInfo || m is PropertyInfo)
				.Select(m => new UrlEncodedMember(Activator.CreateInstance(typeof(MemberReflectionTypeMediator<>).MakeGenericType(content.GetType()), m) as MemberReflectionTypeMediator, m.GetCustomAttribute<AliasAsAttribute>()?.Name ?? m.Name));
		}

		/// <inheritdoc />
		public TReturnType Deserialize<TReturnType>(IResponseBodyReader reader)
		{
			throw new NotImplementedException("TODO: Urlencoded response is not supported as a response type.");
		}

		/// <inheritdoc />
		public Task<TReturnType> DeserializeAsync<TReturnType>(IResponseBodyReader reader)
		{
			throw new NotImplementedException("TODO: Urlencoded response is not supported as a response type.");
		}
	}
}
