using Gedcomx.Model.Rt;
using Gedcomx.Model.Util;
using Gx.Common;
using Gx.Types;
// <auto-generated>
// 
//
// Generated by <a href="http://enunciate.codehaus.org">Enunciate</a>.
// </auto-generated>
using System;

namespace Gx.Conclusion
{

    /// <remarks>
    ///  A role that a specific person plays in an event.
    /// </remarks>
    /// <summary>
    ///  A role that a specific person plays in an event.
    /// </summary>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://gedcomx.org/v1/", TypeName = "EventRole")]
    public partial class EventRole : Gx.Conclusion.Conclusion
    {

        private string _type;
        private Gx.Common.ResourceReference _person;
        private string _details;
        /// <summary>
        ///  The role type.
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "type")]
        [Newtonsoft.Json.JsonProperty("type")]
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
        ///  Convenience property for treating Type as an enum. See Gx.Types.EventRoleTypeQNameUtil for details on getter/setter functionality.
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        [Newtonsoft.Json.JsonIgnore]
        public Gx.Types.EventRoleType KnownType
        {
            get
            {
                return XmlQNameEnumUtil.GetEnumValue<EventRoleType>(this._type);
            }
            set
            {
                this._type = XmlQNameEnumUtil.GetNameValue(value);
            }
        }
        /// <summary>
        ///  Reference to the person playing the role in the event.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "person", Namespace = "http://gedcomx.org/v1/")]
        [Newtonsoft.Json.JsonProperty("person")]
        public Gx.Common.ResourceReference Person
        {
            get
            {
                return this._person;
            }
            set
            {
                this._person = value;
            }
        }
        /// <summary>
        ///  Details about the role of the person in the event.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "details", Namespace = "http://gedcomx.org/v1/")]
        [Newtonsoft.Json.JsonProperty("details")]
        public string Details
        {
            get
            {
                return this._details;
            }
            set
            {
                this._details = value;
            }
        }

        /**
         * Accept a visitor.
         *
         * @param visitor The visitor.
         */
        public void Accept(IGedcomxModelVisitor visitor)
        {
            visitor.VisitEventRole(this);
        }

        /**
         * Build up this event role with a person.
         * @param person The person.
         * @return this.
         */
        public EventRole SetPerson(Person person)
        {
            if (person.Id == null)
            {
                throw new ArgumentException("Cannot reference person: no id.");
            }
            SetPerson(new ResourceReference("#" + person.Id));
            return this;
        }

        /**
         * Build up this event role with a person.
         * @param person The person.
         * @return this.
         */
        public EventRole SetPerson(ResourceReference person)
        {
            Person = person;
            return this;
        }

        /**
         * Build up this role with a type.
         *
         * @param type The type.
         * @return this.
         */
        public EventRole SetType(String type)
        {
            Type = type;
            return this;
        }

        /**
         * Build up this role with a type.
         *
         * @param type The type.
         * @return this.
         */
        public EventRole SetType(EventRoleType type)
        {
            KnownType = type;
            return this;
        }

        /**
         * Build up this event role with details.
         *
         *
         * @param details The details.
         * @return this.
         */
        public EventRole SetDetails(String details)
        {
            Details = details;
            return this;
        }
    }
}
