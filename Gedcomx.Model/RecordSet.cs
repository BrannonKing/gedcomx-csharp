// <auto-generated>
// 
//
// Generated by <a href="http://enunciate.codehaus.org">Enunciate</a>.
// </auto-generated>
using System;

namespace Gx.Records {

  /// <remarks>
  ///  The GEDCOM X bulk record media types are used to exchange bulk genealogical data sets, grouped into records.
  /// </remarks>
  /// <summary>
  ///  The GEDCOM X bulk record media types are used to exchange bulk genealogical data sets, grouped into records.
  /// </summary>
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gedcomx.org/bulk/v1/",TypeName="RecordSet")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://gedcomx.org/bulk/v1/",TypeName="RecordSet")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace="http://gedcomx.org/bulk/v1/",ElementName="records")]
  public partial class RecordSet : Gx.Links.HypermediaEnabledData {

    private string _lang;
    private Gx.Gedcomx _metadata;
    private System.Collections.Generic.List<Gx.Gedcomx> _records;
    /// <summary>
    ///  The language of the genealogical data.
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
    ///  Metadata about this record set; shared among all records in the set.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(ElementName="metadata",Namespace="http://gedcomx.org/bulk/v1/")]
    [System.Xml.Serialization.SoapElementAttribute(ElementName="metadata")]
    public Gx.Gedcomx Metadata {
      get {
        return this._metadata;
      }
      set {
        this._metadata = value;
      }
    }
    /// <summary>
    ///  The records included in this genealogical data set.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(ElementName="record",Namespace="http://gedcomx.org/bulk/v1/")]
    [System.Xml.Serialization.SoapElementAttribute(ElementName="record")]
    public System.Collections.Generic.List<Gx.Gedcomx> Records {
      get {
        return this._records;
      }
      set {
        this._records = value;
      }
    }
  }
}  
