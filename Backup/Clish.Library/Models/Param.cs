using System;
using System.Xml.Serialization;

namespace Clish.Library.Models
{
    /// <summary>
    ///*******************************************************
    ///* This tag is used to define a parameter for a command.
    ///*
    ///* name      - a textual name for this parameter. 
    ///*
    ///* help      - a textual string which describes the purpose of the
    ///*           parameter.
    ///*
    ///* ptype     - Reference to a PTYPE name. This parameter will be 
    ///*           validated against the syntax specified for that type.
    ///*           The special value of "" indicates the parameter is a boolean flag.
    ///*           The verbatim presence of the texual name on the command line
    ///*           simply controls the conditional variable expansion for this 
    ///*           parameter.
    ///*
    ///* [prefix]  - defines the prefix for an optional parameter. A prefix
    ///*           with this value on the command line will signify the presence
    ///*           of an additional argument which will be validated as the
    ///*           value of this parameter.
    ///* 
    ///* [default] - defines a default value for a parameter. Any parameters
    ///*           at the end of command line which have default values need 
    ///*           not explicitly be entered.
    ///********************************************************
    /// </summary>
    [Serializable]
    [XmlRoot("PARAM")]
    public class Param : ModelBase
    {
        /// <summary>
        /// Gets or sets the type of the P.
        /// </summary>
        /// <value>The type of the P.</value>
        [XmlAttribute("ptype")]
        public String PType { get; set; }

        /// <summary>
        /// Gets or sets the default.
        /// </summary>
        /// <value>The default.</value>
        [XmlAttribute("default")]
        public String Default { get; set; }

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>The prefix.</value>
        [XmlAttribute("prefix")]
        public String Prefix { get; set; }
    }
}