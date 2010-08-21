using System;
using System.Xml.Serialization;

namespace Clish.Library.Models
{
    /// <summary>
    ///*******************************************************
    ///* <VIEW> defines the contents of a specific CLI view.
    ///*
    ///* name   - a textual name for the view
    ///*
    ///* prompt - a textual definition of the prompt to be 
    ///*          used whilst in this view.
    ///*          NB. The prompt may contain environment
    ///*              or dynamic variables which are expanded
    ///*              before display. 
    ///********************************************************
    /// </summary>
    [Serializable]
    [XmlRoot("VIEW")]
    public class View
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute("name")]
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the prompt.
        /// </summary>
        /// <value>The prompt.</value>
        [XmlAttribute("prompt")]
        public String Prompt { get; set; }

        /// <summary>
        /// Gets or sets the commands.
        /// </summary>
        /// <value>The commands.</value>
        [XmlElement("COMMAND")]
        public Command[] Commands { get; set; }
    }
}