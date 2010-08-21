using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Clish.Library.Utilities
{
    /// <summary>
    /// Class implements helper methods for serialization.
    /// </summary>
    public static class XmlUtility
    {
        /// <summary>
        /// Deserialize object from xml.
        /// </summary>
        public static T LoadFromXml<T>(String xml)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    writer.Write(xml);
                    writer.Flush();
                    stream.Seek(0, SeekOrigin.Begin);
                    var serializer = new XmlSerializer(typeof (T));
                    return (T) serializer.Deserialize(stream);
                }
            }
        }

        /// <summary>
        /// Serialize object to xml.
        /// </summary>
        public static String GetXml<T>(T value)
        {
            return GetXml(value, null);
        }

        /// <summary>
        /// Serialize object to xml.
        /// </summary>
        public static String GetXml<T>(T value, XmlSerializerNamespaces ns)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    var serializer = new XmlSerializer(typeof (T));
                    serializer.Serialize(writer, value, ns);
                    stream.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public static String GetXmlWithoutNamespace<T>(T value)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            return GetXml(value, ns);
        }

        public static T LoadFromXmlWithoutNamespaces<T>(String xml)
        {
            using (var stream = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof (T));
                return (T) serializer.Deserialize(new NamespaceIgnorantXmlTextReader(stream));
            }
        }

        /// <summary>
        /// Remove ' ', '\r', '\n' characters.
        /// </summary>
        public static String EscapeXml(String xml)
        {
            return xml.Replace("\r", "").Replace("\n", "");
        }
    }
}