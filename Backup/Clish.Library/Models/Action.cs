using System;
using System.Xml.Serialization;

namespace Clish.Library.Models
{
    /// <summary>
    ///********************************************************
    ///* <ACTION> specifies the action to be taken for
    ///* a command. 
    ///* 
    ///* The textual contents of the tag are variable expanded
    ///* (environment, dynamic and parameter) the the resulting
    ///* text is interpreted by the client's script interpreter.
    ///*
    ///* In addition the optional 'builtin' attribute can specify
    ///* the name of an internal command which will be invoked
    ///* instead of the client's script handler.
    ///*
    ///* NB. for security reasons any special shell characters 
    ///* (e.g. $|<>`) are escaped before evaluation.
    ///*
    ///* [builtin] - specify the name of an internally registered
    ///*             function. The content of the ACTION tag is
    ///*             taken as the arguments to this builtin function.
    ///********************************************************
    /// </summary>
    [Serializable]
    [XmlRoot("ACTION")]
    public class Action
    {
        /// <summary>
        /// Gets or sets the built in.
        /// </summary>
        /// <value>The built in.</value>
        [XmlAttribute("builtin")]
        public String BuiltIn { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [XmlText]
        public String Text { get; set; }
    }
}