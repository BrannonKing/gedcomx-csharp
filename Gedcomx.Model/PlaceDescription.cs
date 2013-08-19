// <auto-generated>
// 
//
// Generated by <a href="http://enunciate.codehaus.org">Enunciate</a>.
// </auto-generated>
using System;

namespace Gx.Conclusion {

  /// <remarks>
  ///  A PlaceDescription is used to describe the details of a place in terms of its name
  ///  and possibly its type, time period, and/or a geospatial description -- a description
  ///  of a place as a snapshot in time.
  /// </remarks>
  /// <summary>
  ///  A PlaceDescription is used to describe the details of a place in terms of its name
  ///  and possibly its type, time period, and/or a geospatial description -- a description
  ///  of a place as a snapshot in time.
  /// </summary>
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gedcomx.org/v1/",TypeName="PlaceDescription")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://gedcomx.org/v1/",TypeName="PlaceDescription")]
  public partial class PlaceDescription : Gx.Conclusion.Subject {

    private string _type;
    private System.Collections.Generic.List<Gx.Common.TextValue> _names;
    private Gx.Conclusion.DateInfo _temporalDescription;
    private double? _latitude;
    private bool _latitudeSpecified;
    private double? _longitude;
    private bool _longitudeSpecified;
    private Gx.Common.ResourceReference _spatialDescription;
    /// <summary>
    ///  An implementation-specific uniform resource identifier (URI) used to identify the type of a place (e.g., address, city, county, province, state, country, etc.).
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
    ///  An ordered list of standardized (or normalized), fully-qualified (in terms of what is known of the applicable jurisdictional hierarchy) names for this place that are applicable to this description of this place.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(ElementName="name",Namespace="http://gedcomx.org/v1/")]
    [System.Xml.Serialization.SoapElementAttribute(ElementName="name")]
    public System.Collections.Generic.List<Gx.Common.TextValue> Names {
      get {
        return this._names;
      }
      set {
        this._names = value;
      }
    }
    /// <summary>
    ///  A description of the time period to which this place description is relevant.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(ElementName="temporalDescription",Namespace="http://gedcomx.org/v1/")]
    [System.Xml.Serialization.SoapElementAttribute(ElementName="temporalDescription")]
    public Gx.Conclusion.DateInfo TemporalDescription {
      get {
        return this._temporalDescription;
      }
      set {
        this._temporalDescription = value;
      }
    }
    /// <summary>
    ///  Degrees north or south of the Equator.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(ElementName="latitude",Namespace="http://gedcomx.org/v1/")]
    [System.Xml.Serialization.SoapElementAttribute(ElementName="latitude")]
    public double Latitude {
      get {
        return this._latitude.GetValueOrDefault();
      }
      set {
        this._latitude = value;
        this._latitudeSpecified = true;
      }
    }

    /// <summary>
    ///  Property for the XML serializer indicating whether the "Latitude" property should be included in the output.
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    [System.Xml.Serialization.SoapIgnoreAttribute]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool LatitudeSpecified {
      get {
        return this._latitudeSpecified;
      }
      set {
        this._latitudeSpecified = value;
      }
    }

    /// <summary>
    ///  Angular distance in degrees, relative to the Prime Meridian.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(ElementName="longitude",Namespace="http://gedcomx.org/v1/")]
    [System.Xml.Serialization.SoapElementAttribute(ElementName="longitude")]
    public double Longitude {
      get {
        return this._longitude.GetValueOrDefault();
      }
      set {
        this._longitude = value;
        this._longitudeSpecified = true;
      }
    }

    /// <summary>
    ///  Property for the XML serializer indicating whether the "Longitude" property should be included in the output.
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    [System.Xml.Serialization.SoapIgnoreAttribute]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool LongitudeSpecified {
      get {
        return this._longitudeSpecified;
      }
      set {
        this._longitudeSpecified = value;
      }
    }

    /// <summary>
    ///  A reference to a geospatial description of this place.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(ElementName="spatialDescription",Namespace="http://gedcomx.org/v1/")]
    [System.Xml.Serialization.SoapElementAttribute(ElementName="spatialDescription")]
    public Gx.Common.ResourceReference SpatialDescription {
      get {
        return this._spatialDescription;
      }
      set {
        this._spatialDescription = value;
      }
    }
  }
}  
