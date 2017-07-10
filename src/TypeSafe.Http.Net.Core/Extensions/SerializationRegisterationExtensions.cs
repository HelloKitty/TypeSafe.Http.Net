using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Extension methods that help allow you to register serialization
	/// factories/provides in a much cleaner fashion
	/// </summary>
	public static class SerializationRegisterationExtensions
	{
		/// <summary>
		/// Registers all the default serializers that require no additional external dependencies such as string content
		/// and urlencoded body content and more.
		/// </summary>
		/// <param name="builder">The builder being built.</param>
		/// <returns>The builder for fluent chaining.</returns>
		public static TSerializationRegisterationType RegisterDefaultSerializers<TSerializationRegisterationType>(this TSerializationRegisterationType builder) 
			where TSerializationRegisterationType : ISerializationStrategyRegister
		{
			//The defaults are UrlEncodedBody and the String content.
			builder.Register<UrlEncodedBodyAttribute, UrlEncodedBodySerializerStrategy>(new UrlEncodedBodySerializerStrategy());
			builder.Register<StringBodyAttribute, StringBodySerializationStrategy>(new StringBodySerializationStrategy());

			//fluently return
			return builder;
		}
	}
}
