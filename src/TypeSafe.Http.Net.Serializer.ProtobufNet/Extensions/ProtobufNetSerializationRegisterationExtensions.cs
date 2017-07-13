using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	public static class ProtobufNetSerializationRegisterationExtensions
	{
		/// <summary>
		/// Registers the Protobuf-Net serializer in the serializer registeration service.
		/// </summary>
		/// <param name="builder">The builder being built.</param>
		/// <returns>The builder for fluent chaining.</returns>
		public static TSerializationRegisterationType RegisterProtobufNetSerializer<TSerializationRegisterationType>(this TSerializationRegisterationType builder)
			where TSerializationRegisterationType : ISerializationStrategyRegister
		{
			builder.Register<ProtobufBodyAttribute, ProtobufNetProtobufBodySerializerStrategy>(new ProtobufNetProtobufBodySerializerStrategy());

			//fluently return
			return builder;
		}
	}
}
