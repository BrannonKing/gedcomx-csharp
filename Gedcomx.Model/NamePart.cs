// <auto-generated>
// 
//
// Generated by <a href="http://enunciate.codehaus.org">Enunciate</a>.
// </auto-generated>
using System;

namespace Gx.Conclusion {

  /// <remarks>
  ///  A part of a name.
  /// </remarks>
  /// <summary>
  ///  A part of a name.
  /// </summary>
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gedcomx.org/v1/",TypeName="NamePart")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://gedcomx.org/v1/",TypeName="NamePart")]
  public sealed partial class NamePart : Gx.Common.ExtensibleData {

    private string _value;
    private string _type;
    private System.Collections.Generic.List<Gx.Common.EvidenceReference> _fieldValueReferences;
    private System.Collections.Generic.List<Gx.Common.Qualifier> _qualifiers;
    /// <summary>
    ///  The value of the name part.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(AttributeName="value")]
    [System.Xml.Serialization.SoapAttributeAttribute(AttributeName="value")]
    public string Value {
      get {
        return this._value;
      }
      set {
        this._value = value;
      }
    }
    /// <summary>
    ///  The type of the name part.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(AttributeName="type")]
    [System.Xml.Serialization.SoapAttributeAttribute(AttributeName="type")]
    public string Type {
      get {
        return this._type;
      }
      set {
        this._type = value;
      }
    }

    /// <summary>
    ///  Convenience property for treating Type as an enum. See Gx.Types.NamePartTypeQNameUtil for details on getter/setter functionality.
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public Gx.Types.NamePartType KnownType {
      get {
        return Gx.Types.NamePartTypeQNameUtil.ConvertFromKnownQName(this._type);
      }
      set {
        this._type = Gx.Types.NamePartTypeQNameUtil.ConvertToKnownQName(value);
      }
    }
    /// <summary>
    ///  The references to the record field values being used as evidence.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(ElementName="fieldValue",Namespace="http://gedcomx.org/v1/")]
    [System.Xml.Serialization.SoapElementAttribute(ElementName="fieldValue")]
    public System.Collections.Generic.List<Gx.Common.EvidenceReference> FieldValueReferences {
      get {
        return this._fieldValueReferences;
      }
      set {
        this._fieldValueReferences = value;
      }
    }
    /// <summary>
    ///  The qualifiers associated with this name part.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(ElementName="qualifier",Namespace="http://gedcomx.org/v1/")]
    [System.Xml.Serialization.SoapElementAttribute(ElementName="qualifier")]
    public System.Collections.Generic.List<Gx.Common.Qualifier> Qualifiers {
      get {
        return this._qualifiers;
      }
      set {
        this._qualifiers = value;
      }
    }
  }
}  
