using System;
using System.Xml.Serialization;

namespace Clish.Library.Models
{
    /// <summary>
    /// Class impelements entry point to xml configuration.
    /// 
    /// <CLISH_MODULE> is the top level container.
    /// Any commands which are defined within this tag are global in scope 
    /// i.e. they are visible from all views.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "CLISH_MODULE")]
    public class ClishModule
    {
        /// <summary>
        /// Collection contains types for commands.
        /// </summary>
        /// <value>The P types.</value>
        [XmlElement("PTYPE")]
        public PType[] PTypes { get; set; }

        /// <summary>
        /// Collection contains commands for clish terminal.
        /// </summary>
        /// <value>The commands.</value>
        [XmlElement("COMMAND")]
        public Command[] Commands { get; set; }

        /// <summary>
        ///********************************************************
        ///* <OVERVIEW> specifies a textual description of the shell.
        ///* 
        ///* This should provide instructions about key bindings and
        ///* escape sequences which can be used in the CLI.
        ///*
        ///********************************************************
        /// </summary>
        [XmlElement("OVERVIEW")]
        public String Overview { get; set; }

        /// <summary>
        /// Gets or sets the startup.
        /// </summary>
        /// <value>The startup.</value>
        [XmlElement("STARTUP")]
        public Startup Startup { get; set; }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>The view.</value>
        [XmlElement("VIEW")]
        public View[] Views { get; set; }
    }
}