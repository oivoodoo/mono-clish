using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Clish.Library.Models
{
    public enum PreProcessType
    {
        [XmlEnum("none")] None,

        [XmlEnum("toupper")] ToUpper,

        [XmlEnum("tolower")] ToLower
    }

    public enum MethodType
    {
        [XmlEnum("regexp")] Regexp,

        [XmlEnum("integer")] Integer,

        [XmlEnum("select")] Select
    }

    ///<summary>
    ///*******************************************************
    ///* <PTYPE> is used to define the syntax for a parameter type.
    ///*
    ///* name    - a textual name for this type. This name can be used to
    ///*         reference this type within a <PARAM? ptype attribute. 
    ///*
    ///* help    - a textual string which describes the syntax of this type.
    ///*         When a parameter is filled out incorrectly on the CLI, this
    ///*         text will be used to prompt the user to fill out the value 
    ///*         correctly.
    ///*
    ///* pattern     - A regular expression which defines the syntax of the type.
    ///*
    ///* method      - The means by which the pattern is interpreted.
    ///*
    ///*               "regexp" [default] - A POSIX regular expression.
    ///*
    ///*               "integer"          - A numeric definition "min..max"
    ///*
    ///*               "select"           - A list of possible values. 
    ///*               The syntax of the string is of the form: 
    ///*                 "valueOne(ONE) valueTwo(TWO) valueThree(THREE)"
    ///*               where the text before the parethesis defines the syntax 
    ///*               that the user must use, and the value within the parenthesis 
    ///*               is the result expanded as a parameter value. 
    ///*
    ///* preprocess  - An optional directive to process the value entered before
    ///*               validating it. This can greatly simplify the regular expressions
    ///*               needed to match case insensitive values.
    ///*
    ///*               "none" [default] - do nothing
    ///*
    ///*               "toupper"        - before validation convert to uppercase.
    ///*
    ///*               "tolower"        - before validation convert to lowercase.
    ///*
    ///********************************************************
    /// </summary>
    [Serializable]
    [XmlRoot("PTYPE")]
    public class PType : ModelBase
    {
        [NonSerialized] private const String SelectionPattern = @"(\(\d+\))";
        [NonSerialized] private String m_helpPattern = string.Empty;
        [NonSerialized] private List<String> m_numbers = new List<string>();
        [NonSerialized] private List<String> m_selections = new List<string>();

        /// <summary>
        /// Gets or sets the numbers.
        /// </summary>
        /// <value>The numbers.</value>
        [XmlIgnore]
        public List<String> Numbers
        {
            get
            {
                if (m_numbers.Count == 0 && Method == MethodType.Integer)
                {
                    ParseNumbers();
                }
                return m_numbers;
            }
            protected set { m_numbers = value; }
        }

        /// <summary>
        /// Gets or sets the selections.
        /// </summary>
        /// <value>The selections.</value>
        [XmlIgnore]
        public List<String> Selections
        {
            get
            {
                if (m_selections.Count == 0 && Method == MethodType.Select)
                {
                    ParseSelection();
                }
                return m_selections;
            }
            protected set { m_selections = value; }
        }

        /// <summary>
        /// Gets or sets the help pattern.
        /// </summary>
        /// <value>The help pattern.</value>
        [XmlIgnore]
        public String HelpPattern
        {
            get
            {
                if (String.IsNullOrEmpty(m_helpPattern))
                {
                    switch (Method)
                    {
                        case MethodType.Integer:
                            m_helpPattern = Help + " " + Pattern;
                            break;
                        case MethodType.Regexp:
                            m_helpPattern = Help;
                            break;
                        case MethodType.Select:
                            Selections.ForEach(s => m_helpPattern += s + " ");
                            break;
                    }
                }
                return m_helpPattern;
            }
            protected set { m_helpPattern = value; }
        }

        /// <summary>
        /// Gets or sets the pattern.
        /// </summary>
        /// <value>The pattern.</value>
        [XmlAttribute("pattern")]
        public String Pattern { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        [XmlAttribute("method")]
        public MethodType Method { get; set; }

        /// <summary>
        /// Gets or sets the pre process.
        /// </summary>
        /// <value>The pre process.</value>
        [XmlAttribute("preprocess")]
        public PreProcessType PreProcess { get; set; }

        /// <summary>
        /// Parses the selection.
        /// </summary>
        private void ParseSelection()
        {
            m_selections = new List<String>();
            foreach (string match in
                Pattern.Split(new[] {' '}).Select(p => Regex.Replace(p, SelectionPattern, String.Empty)))
            {
                m_selections.Add(match);
            }
        }

        /// <summary>
        /// Parses the numbers.
        /// </summary>
        private void ParseNumbers()
        {
            if (Pattern.Contains(".."))
            {
                string[] values = Pattern.Split(new[] {".."}, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length > 1)
                {
                    for (int i = Convert.ToInt32(values[0]); i <= Convert.ToInt32(values[1]); i++)
                    {
                        m_numbers.Add(i.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Searches the in selections.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        public List<String> SearchInSelections(string p)
        {
            return Selections.Where(list => list.IndexOf(p) == 0 || String.IsNullOrEmpty(list)).ToList();
        }
    }
}