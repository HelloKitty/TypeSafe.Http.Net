using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	//This may seem like there is a lot of indirection here but we need to make it this way because
	//both the method in which the content is put into a request AND the way in which the content is serialized must be abstracted from eachother AND from
	//the core functions itself to be pluggable replacements for serializers and HTTP clients.
	/// <summary>
	/// Contract for types that provide serialization services.
	/// </summary>
	public interface IRequestSerializationStrategy
	{
		/// <summary>
		/// Tries to serialize the content
		/// </summary>
		/// <param name="content">Content to try to serialize into the writer.</param>
		/// <param name="writer">The writer to write the serialized content into.</param>
		/// <returns></returns>
		bool TrySerialize(object content, IRequestBodyWriter writer);
	}
}
