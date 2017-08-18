using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TypeSafe.Http.Net
{
	public static class DotNetXmlSerializationRegisterationExtensions
	{
		/// <summary>
		/// Registers the .NET Framework <see cref="XmlSerializer"/> in the serializer registeration service.
		/// </summary>
		/// <param name="builder">The builder being built.</param>
		/// <returns>The builder for fluent chaining.</returns>
		public static TSerializationRegisterationType RegisterDotNetXmlSerializer<TSerializationRegisterationType>(this TSerializationRegisterationType builder)
			where TSerializationRegisterationType : ISerializationStrategyRegister
		{
			builder.Register<XmlBodyAttribute, DotNetXmlBodySerializerStrategy>(new DotNetXmlBodySerializerStrategy());

			//fluently return
			return builder;
		}
	}
}
