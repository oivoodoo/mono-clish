using System.IO;
using System.Xml;

namespace Clish.Library.Utilities
{
    public class NamespaceIgnorantXmlTextReader : XmlTextReader
    {
        public NamespaceIgnorantXmlTextReader(TextReader reader) : base(reader)
        {
        }

        public override string NamespaceURI
        {
            get { return ""; }
        }
    }
}