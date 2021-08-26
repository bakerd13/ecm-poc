using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DABTechs.eCommerce.Sales.Common
{
    public static class Serialization
    {
        /// <summary>
        /// Deserializes the specified data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string data)
        {
            if (data == null || string.IsNullOrWhiteSpace(data)) { return default; }

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using XmlTextReader textReader = new XmlTextReader(data);
            return (T)serializer.Deserialize(textReader);
        }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The object.</param>
        /// <returns></returns>
        public static string Serialize<T>(T item)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true,
                Encoding = Encoding.UTF8
            };

            using StringWriter textWriter = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
            {
                xmlSerializer.Serialize(xmlWriter, item);
            }
            return textWriter.ToString();
        }
    }
}