// <auto-generated>
// 
//
// Generated by <a href="http://enunciate.codehaus.org">Enunciate</a>.
// </auto-generated>
using System;

namespace Gx.Source
{

    /// <remarks>
    ///  Represents a citation field -- its name and value.
    /// </remarks>
    /// <summary>
    ///  Represents a citation field -- its name and value.
    /// </summary>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://gedcomx.org/v1/", TypeName = "CitationField")]
    public partial class CitationField
    {

        private string _name;
        private string _value;
        /// <summary>
        ///  The citation field's name.
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "name")]
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        /// <summary>
        ///  The citation field's value.
        /// </summary>
        [System.Xml.Serialization.XmlTextAttribute()]
        [Newtonsoft.Json.JsonProperty("value")]
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }

        /**
         * Build out this citation field with a name.
         *
         * @param name the name.
         * @return this.
         */
        public CitationField SetName(String name)
        {
            Name = name;
            return this;
        }

        /**
         * Build out this citation field with a value.
         * @param value The value.
         * @return this.
         */
        public CitationField SetValue(String value)
        {
            Value = value;
            return this;
        }
    }
}
