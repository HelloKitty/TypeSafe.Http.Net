using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	public static class JsonNetSerializationRegisterationExtensions
	{
		/// <summary>
		/// Registers the Json.NET serializer in the serializer registeration service.
		/// </summary>
		/// <param name="builder">The builder being built.</param>
		/// <returns>The builder for fluent chaining.</returns>
		public static TSerializationRegisterationType RegisterJsonNetSerializer<TSerializationRegisterationType>(this TSerializationRegisterationType builder)
			where TSerializationRegisterationType : ISerializationStrategyRegister
		{
			builder.Register<JsonBodyAttribute, JsonNetJsonBodySerializerStrategy>(new JsonNetJsonBodySerializerStrategy());

			//fluently return
			return builder;
		}
	}
}
