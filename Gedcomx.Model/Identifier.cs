using Gedcomx.Model.Util;
using Newtonsoft.Json;
// <auto-generated>
// 
//
// Generated by <a href="http://enunciate.codehaus.org">Enunciate</a>.
// </auto-generated>
using System;

namespace Gx.Conclusion
{

    /// <remarks>
    ///  An identifier for a resource.
    /// </remarks>
    /// <summary>
    ///  An identifier for a resource.
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://gedcomx.org/v1/", TypeName = "Identifier")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://gedcomx.org/v1/", TypeName = "Identifier")]
    [JsonConverter(typeof(JsonIdentifiersConverter))]
    public sealed partial class Identifier
    {

        private string _type;
        private string _value;
        /// <summary>
        ///  The type of the id.
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "type")]
        [System.Xml.Serialization.SoapAttributeAttribute(AttributeName = "type")]
        public string Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        /// <summary>
        ///  Convenience property for treating Type as an enum. See Gx.Types.IdentifierTypeQNameUtil for details on getter/setter functionality.
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Gx.Types.IdentifierType KnownType
        {
            get
            {
                return Gx.Types.IdentifierTypeQNameUtil.ConvertFromKnownQName(this._type);
            }
            set
            {
                this._type = Gx.Types.IdentifierTypeQNameUtil.ConvertToKnownQName(value);
            }
        }
        /// <summary>
        ///  The id value.
        /// </summary>
        [System.Xml.Serialization.XmlTextAttribute()]
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
    }
}
