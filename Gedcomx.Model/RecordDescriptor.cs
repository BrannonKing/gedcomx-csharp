// <auto-generated>
// 
//
// Generated by <a href="http://enunciate.codehaus.org">Enunciate</a>.
// </auto-generated>
using System;

namespace Gx.Records {

  /// <remarks>
  ///  A descriptor for a common set of records.
  /// </remarks>
  /// <summary>
  ///  A descriptor for a common set of records.
  /// </summary>
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gedcomx.org/v1/",TypeName="RecordDescriptor")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://gedcomx.org/v1/",TypeName="RecordDescriptor")]
  public partial class RecordDescriptor : Gx.Links.HypermediaEnabledData {

    private string _lang;
    private System.Collections.Generic.List<Gx.Records.FieldDescriptor> _fields;
    /// <summary>
    ///  The language of this record description.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(AttributeName="lang",Namespace="http://www.w3.org/XML/1998/namespace")]
    [System.Xml.Serialization.SoapAttributeAttribute(AttributeName="lang",Namespace="http://www.w3.org/XML/1998/namespace")]
    public string Lang {
      get {
        return this._lang;
      }
      set {
        this._lang = value;
      }
    }
    /// <summary>
    ///  The fields that are applicable to this record.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(ElementName="field",Namespace="http://gedcomx.org/v1/")]
    [System.Xml.Serialization.SoapElementAttribute(ElementName="field")]
    public System.Collections.Generic.List<Gx.Records.FieldDescriptor> Fields {
      get {
        return this._fields;
      }
      set {
        this._fields = value;
      }
    }
  }
}  
