using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Strategy for deserializing an instance of <typeparamref name="TReturnType"/> from
	/// a provided <see cref="IResponseBodyReader"/>.
	/// </summary>
	/// <typeparam name="TReturnType"></typeparam>
	public interface IResponseDeserializationStrategy<TReturnType>
	{
		/// <summary>
		/// Deserializes the response from the provided response body reader.
		/// </summary>
		/// <param name="reader">A reader service that can interpret the response.</param>
		/// <returns>A non-null <typeparamref name="TReturnType"/> deserialized from the body.</returns>
		TReturnType Deserialize(IResponseBodyReader reader);

		/// <summary>
		/// Deserializes the response asyncronously from the provided response body reader.
		/// </summary>
		/// <param name="reader">A reader service that can interpret the response.</param>
		/// <returns>A non-null <typeparamref name="TReturnType"/> deserialized from the body.</returns>
		Task<TReturnType> DeserializeAsync(IResponseBodyReader reader);
	}
}
