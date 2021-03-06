using Gedcomx.Model.Rt;
using Gedcomx.Model.Util;
using Gx.Common;
using Gx.Conclusion;
using Newtonsoft.Json;
// <auto-generated>
// 
//
// Generated by <a href="http://enunciate.codehaus.org">Enunciate</a>.
// </auto-generated>
using System;
using System.Collections.Generic;

namespace Gx.Agent
{

    /// <remarks>
    ///  An agent, e.g. person, organization, or group. In genealogical research, an agent often
    ///  takes the role of a contributor.
    /// </remarks>
    /// <summary>
    ///  An agent, e.g. person, organization, or group. In genealogical research, an agent often
    ///  takes the role of a contributor.
    /// </summary>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://gedcomx.org/v1/", TypeName = "Agent")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://gedcomx.org/v1/", ElementName = "agent")]
    public partial class Agent : Gx.Links.HypermediaEnabledData
    {
        private System.Collections.Generic.List<Gx.Agent.OnlineAccount> _accounts;
        private System.Collections.Generic.List<Gx.Agent.Address> _addresses;
        private System.Collections.Generic.List<Gx.Common.ResourceReference> _emails;
        private Gx.Common.ResourceReference _homepage;
        private System.Collections.Generic.List<Gx.Conclusion.Identifier> _identifiers;
        private System.Collections.Generic.List<Gx.Common.TextValue> _names;
        private Gx.Common.ResourceReference _openid;
        private System.Collections.Generic.List<Gx.Common.ResourceReference> _phones;
        /// <summary>
        ///  The accounts that belong to this person or organization.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "account", Namespace = "http://gedcomx.org/v1/")]
        [Newtonsoft.Json.JsonProperty("accounts")]
        public System.Collections.Generic.List<Gx.Agent.OnlineAccount> Accounts
        {
            get
            {
                return this._accounts;
            }
            set
            {
                this._accounts = value;
            }
        }
        /// <summary>
        ///  The addresses that belong to this person or organization.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "address", Namespace = "http://gedcomx.org/v1/")]
        [Newtonsoft.Json.JsonProperty("addresses")]
        public System.Collections.Generic.List<Gx.Agent.Address> Addresses
        {
            get
            {
                return this._addresses;
            }
            set
            {
                this._addresses = value;
            }
        }
        /// <summary>
        ///  The emails that belong to this person or organization.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "email", Namespace = "http://gedcomx.org/v1/")]
        [Newtonsoft.Json.JsonProperty("emails")]
        public System.Collections.Generic.List<Gx.Common.ResourceReference> Emails
        {
            get
            {
                return this._emails;
            }
            set
            {
                this._emails = value;
            }
        }
        /// <summary>
        ///  The homepage.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "homepage", Namespace = "http://gedcomx.org/v1/")]
        [Newtonsoft.Json.JsonProperty("homepage")]
        public Gx.Common.ResourceReference Homepage
        {
            get
            {
                return this._homepage;
            }
            set
            {
                this._homepage = value;
            }
        }
        /// <summary>
        ///  The list of identifiers for the agent.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "identifier", Namespace = "http://gedcomx.org/v1/")]
        [Newtonsoft.Json.JsonProperty("identifiers")]
        [JsonConverter(typeof(JsonIdentifiersConverter))]
        public System.Collections.Generic.List<Gx.Conclusion.Identifier> Identifiers
        {
            get
            {
                return this._identifiers;
            }
            set
            {
                this._identifiers = value;
            }
        }
        /// <summary>
        ///  The list of names for the agent.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "name", Namespace = "http://gedcomx.org/v1/")]
        [Newtonsoft.Json.JsonProperty("names")]
        public System.Collections.Generic.List<Gx.Common.TextValue> Names
        {
            get
            {
                return this._names;
            }
            set
            {
                this._names = value;
            }
        }
        /// <summary>
        ///  The &lt;a href=&quot;http://openid.net/&quot;&gt;openid&lt;/a&gt; of the person or organization.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "openid", Namespace = "http://gedcomx.org/v1/")]
        [Newtonsoft.Json.JsonProperty("openid")]
        public Gx.Common.ResourceReference Openid
        {
            get
            {
                return this._openid;
            }
            set
            {
                this._openid = value;
            }
        }
        /// <summary>
        ///  The phones that belong to this person or organization.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "phone", Namespace = "http://gedcomx.org/v1/")]
        [Newtonsoft.Json.JsonProperty("phones")]
        public System.Collections.Generic.List<Gx.Common.ResourceReference> Phones
        {
            get
            {
                return this._phones;
            }
            set
            {
                this._phones = value;
            }
        }

        /**
         * Accept a visitor.
         *
         * @param visitor The visitor.
         */
        public void Accept(IGedcomxModelVisitor visitor)
        {
            visitor.VisitAgent(this);
        }

        public Agent SetName(String name)
        {
            return SetName(new TextValue(name));
        }

        /**
         * Build up this agent with a name.
         * 
         * @param name The name.
         * @return this.
         */
        public Agent SetName(TextValue name)
        {
            AddName(name);
            return this;
        }

        /**
         * Build up this agent with an identifier.
         * 
         * @param identifier The identifier.
         * @return this.
         */
        public Agent SetIdentifier(Identifier identifier)
        {
            AddIdentifier(identifier);
            return this;
        }

        /**
         * Build up this agent with a home page.
         * @param homepage The home page of the agent.
         * @return this.
         */
        public Agent SetHomepage(ResourceReference homepage)
        {
            this.Homepage = homepage;
            return this;
        }

        /**
         * Build up this agent with a open id.
         * @param openid The open id of the agent.
         * @return this.
         */
        public Agent SetOpenid(ResourceReference openid)
        {
            this.Openid = openid;
            return this;
        }

        /**
         * Build up this agent with an online account.
         * @param account The account.
         * @return this.
         */
        public Agent SetAccount(OnlineAccount account)
        {
            AddAccount(account);
            return this;
        }

        /**
         * Build up this agent with an email address.
         *
         * @param email The email address.
         * @return this.
         */
        public Agent SetEmail(String email)
        {
            return SetEmail(new ResourceReference("mailto:" + email));
        }

        /**
         * Build up this agent with an email address.
         *
         * @param email The email address.
         * @return this.
         */
        public Agent SetEmail(ResourceReference email)
        {
            AddEmail(email);
            return this;
        }

        /**
         * Build up this agent with a phone number.
         * @param phone The phone number.
         * @return this.
         */
        public Agent SetPhone(ResourceReference phone)
        {
            AddPhone(phone);
            return this;
        }

        /**
         * Build up this agent with an address.
         * @param address The address.
         * @return this.
         */
        public Agent SetAddress(Address address)
        {
            AddAddress(address);
            return this;
        }

        /**
         * Add a name.
         * 
         * param name The name.
         */
        public void AddName(TextValue name)
        {
            if (this._names == null)
            {
                this._names = new List<TextValue>();
            }

            this._names.Add(name);
        }

        /**
         * Add an identifier.
         * 
         * @param identifier The identifier to add.
         */
        public void AddIdentifier(Identifier identifier)
        {
            if (this._identifiers == null)
            {
                this._identifiers = new List<Identifier>();
            }

            this._identifiers.Add(identifier);
        }

        /**
         * Add an account.
         *
         * @param account The account to add.
         */
        public void AddAccount(OnlineAccount account)
        {
            if (this._accounts == null)
            {
                this._accounts = new List<OnlineAccount>();
            }

            this._accounts.Add(account);
        }

        /**
         * Add an email.
         *
         * @param email The email.
         */
        public void AddEmail(ResourceReference email)
        {
            if (this._emails == null)
            {
                this._emails = new List<ResourceReference>();
            }

            this._emails.Add(email);
        }

        /**
         * Add a phone.
         *
         * @param phone The phone to add.
         */
        public void AddPhone(ResourceReference phone)
        {
            if (this._phones == null)
            {
                this._phones = new List<ResourceReference>();
            }

            this._phones.Add(phone);
        }

        /**
         * Add an address.
         *
         * @param address The address to add.
         */
        public void AddAddress(Address address)
        {
            if (this._addresses == null)
            {
                this._addresses = new List<Address>();
            }

            this._addresses.Add(address);
        }
    }
}
