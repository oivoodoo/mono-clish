using System;
using System.Xml.Serialization;

namespace Clish.Library.Models
{
    [Serializable]
    public class ModelBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute("name")]
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the help.
        /// </summary>
        /// <value>The help.</value>
        [XmlAttribute("help")]
        public String Help { get; set; }
    }
}