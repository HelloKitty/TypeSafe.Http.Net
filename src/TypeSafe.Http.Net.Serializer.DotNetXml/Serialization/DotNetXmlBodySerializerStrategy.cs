using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Body serializer/deserializer for XML using the default .NET Framework XML serializer.
	/// </summary>
	public sealed class DotNetXmlBodySerializerStrategy : IRequestSerializationStrategy, IResponseDeserializationStrategy, IContentTypeAssociable
	{
		//TODO: Hide array behind for more efficient content-type lookup
		/// <inheritdoc />
		public IEnumerable<string> AssociatedContentType { get; }

		public DotNetXmlBodySerializerStrategy()
		{
			//TODO: Implement and handle other XML types
			//See: https://www.ietf.org/rfc/rfc3023.txt
			AssociatedContentType = new string[] { @"text/xml", @"application/xml" };
		}

		/// <inheritdoc />
		public bool TrySerialize(object content, IRequestBodyWriter writer)
		{
			string xml = null;

			XmlSerializer serializer = new XmlSerializer(content.GetType());
			using (StringWriter stringWriter = new StringWriter())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
				{
					serializer.Serialize(xmlWriter, content);
					xml = stringWriter.ToString();
				}
			}

			if (String.IsNullOrWhiteSpace(xml))
				return false;

			//At this point we have hopefully valid XML and we should write it with the content type
			writer.Write(xml, AssociatedContentType.First());

			return true;
		}

		/// <inheritdoc />
		public TReturnType Deserialize<TReturnType>(IResponseBodyReader reader)
		{
			string xml = reader.ReadAsString();

			XmlSerializer serializer = new XmlSerializer(typeof(TReturnType));

			using (StringReader stringReader = new StringReader(xml))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader))
				{
					return (TReturnType)serializer.Deserialize(xmlReader);
				}
			}
		}

		/// <inheritdoc />
		public async Task<TReturnType> DeserializeAsync<TReturnType>(IResponseBodyReader reader)
		{
			string xml = await reader.ReadAsStringAsync();

			//TODO: Can or should this be done async too?
			XmlSerializer serializer = new XmlSerializer(typeof(TReturnType));

			using (StringReader stringReader = new StringReader(xml))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader))
				{
					return (TReturnType)serializer.Deserialize(xmlReader);
				}
			}
		}
	}
}
