using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Clish.Library.Models
{
    public class CommandBase : ModelBase
    {
        public CommandBase()
        {
        }

        public CommandBase(Session session)
        {
            Session = session;
        }

        [XmlIgnore]
        public Session Session { get; set; }

        [XmlIgnore]
        public String ToRun { get; set; }

        [XmlIgnore]
        public List<String> ParsedParams { get; set; }
    }
}