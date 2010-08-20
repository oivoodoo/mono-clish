using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Clish.Library.Models
{
    /// <summary>
    /// Command model wrapper for system actions.
    ///*******************************************************
    ///* <COMMAND> is used to define a command within the CLI.
    ///*
    ///* name           - a textual name for this command. (This may contain
    ///*                spaces e.g. "display acl")
    ///*
    ///* help           - a textual string which describes the purpose of the
    ///*                command.
    ///*
    ///* [view]         - defines the view which will be transitioned to, on 
    ///*                successful execution of any <ACTION> tag. By default the
    ///*                current view stays in scope.
    ///*
    ///* [viewid]       - defined the new value of the ${VIEWID} variable to
    ///*                be used if a transition to a new view occurs. By default
    ///*                the viewid will retain it's current value.
    ///* 
    ///* [access]       - defines the user group/level to which execution of this 
    ///*                command is restricted. By default there is no restriction.
    ///*                The exact interpretation of this field is dependant on the
    ///*                client of libclish but for example the clish and tclish 
    ///*                applications map this to the UNIX user groups.
    ///*
    ///* [escape_chars] - defines the characters which will be escaped (e.g. \$) before
    ///*                being expanded as a variable. By default the following
    ///*                characters will be escaped `|$<>&()#
    ///*
    ///* [args]         - defines a parameter name to be used to gather the rest of the
    ///*                command line after the formally defined parameters 
    ///*                (PARAM elements). The formatting of this parameter is a raw
    ///*                string containing as many words as there are on the rest of the
    ///*                command line.
    ///*
    ///* [args_help]    - a textual string which describes the purpose of the 'args' 
    ///*                parameter. If the "args" attribute is given then this MUST be
    ///*                given also.
    ///*
    ///*
    ///********************************************************
    /// </summary>
    [Serializable]
    [XmlRoot("COMMAND")]
    public class Command : CommandBase
    {
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        [XmlElement("ACTION")]
        public Action Action { get; set; }

        /// <summary>
        ///********************************************************
        ///* <DETAIL> specifies a textual description.
        ///* 
        ///* This may be used within the scope of a <COMMAND> 
        ///* element, in which case it would typically contain 
        ///* detailed usage instructions including examples.
        ///*
        ///* This may also be used within the scope of a <STARTUP>
        ///* element, in which case the text is used as the banner
        ///* details which are displayed on shell startup. This is
        ///* shown before any specified <ACTION> is executed.
        ///*
        ///* This text may also be used in the production of user manuals.
        ///********************************************************
        /// </summary>
        [XmlElement("DETAIL")]
        public String Detail { get; set; }

        /// <summary>
        /// Gets or sets the access.
        /// </summary>
        /// <value>The access.</value>
        [XmlAttribute("access")]
        public String Access { get; set; }

        /// <summary>
        /// Gets or sets the args.
        /// </summary>
        /// <value>The args.</value>
        [XmlAttribute("args")]
        public String Args { get; set; }

        /// <summary>
        /// Gets or sets the args help.
        /// </summary>
        /// <value>The args help.</value>
        [XmlAttribute("args_help")]
        public String ArgsHelp { get; set; }

        /// <summary>
        /// Gets or sets the escape chars.
        /// </summary>
        /// <value>The escape chars.</value>
        [XmlAttribute("escape_chars")]
        public String EscapeChars { get; set; }

        [XmlElement("PARAM")]
        public Param[] Params { get; set; }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>The view.</value>
        [XmlAttribute("view")]
        public String View { get; set; }

        /// <summary>
        /// Gets or sets the view id.
        /// </summary>
        /// <value>The view id.</value>
        [XmlAttribute("viewid")]
        public String ViewId { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has view.
        /// </summary>
        /// <value><c>true</c> if this instance has view; otherwise, <c>false</c>.</value>
        [XmlIgnore]
        public bool HasView
        {
            get
            {
                return !string.IsNullOrEmpty(View) ||
                          !string.IsNullOrEmpty(ViewId);
            }
        }
    }
}