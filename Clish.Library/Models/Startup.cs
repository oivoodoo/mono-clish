using System;
using System.Xml.Serialization;

namespace Clish.Library.Models
{
    /// <summary>
    ///*******************************************************
    ///* <STARTUP> is used to define what happens when the CLI
    ///* is started. Any text held in a <DETAIL> sub-element will
    ///* be used as banner text, then any defined <ACTION> will be 
    ///* executed. This action may provide Message Of The Day (MOTD)
    ///* type behaviour.
    ///*
    ///* view   - defines the view which will be transitioned to, on 
    ///*          successful execution of any <ACTION> tag.
    ///*
    ///* [viewid] - defined the new value of the ${VIEWID} variable to
    ///*          be used if a transition to a new view occurs.
    ///********************************************************
    /// </summary>
    [Serializable]
    [XmlRoot("STARTUP")]
    public class Startup
    {
        /// <summary>
        /// View name for startup application.
        /// </summary>
        /// <value>The view.</value>
        [XmlAttribute("view")]
        public String View { get; set; }

        /// <summary>
        /// View Id is optional value for startup view.
        /// </summary>
        /// <value>The view id.</value>
        [XmlAttribute("viewid")]
        public String ViewId { get; set; }

        /// <summary>
        /// Contains action values.
        /// </summary>
        /// <value>The action.</value>
        [XmlElement("ACTION")]
        public Action Action { get; set; }

        /// <summary>
        /// Contains details information showing in the startup
        /// application in the top of terminal
        /// </summary>
        /// <value>The detail.</value>
        [XmlElement("DETAIL")]
        public String Detail { get; set; }
    }
}