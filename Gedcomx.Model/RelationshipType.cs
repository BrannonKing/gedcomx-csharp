// <auto-generated>
// 
//
// Generated by <a href="http://enunciate.codehaus.org">Enunciate</a>.
// </auto-generated>
using System;

namespace Gx.Types {

  /// <remarks>
  ///  Enumeration of standard relationship types.
  /// </remarks>
  /// <summary>
  ///  Enumeration of standard relationship types.
  /// </summary>
  public enum RelationshipType {

    /// <summary>
    ///  Unspecified enum value.
    /// </summary>
    [System.Xml.Serialization.XmlEnumAttribute(Name="__NULL__")]
    [System.Xml.Serialization.SoapEnumAttribute(Name="__NULL__")]
    NULL,

    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    Couple,

    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    ParentChild,

    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    OTHER
  }

  /// <remarks>
  /// Utility class for converting to/from the QNames associated with RelationshipType.
  /// </remarks>
  /// <summary>
  /// Utility class for converting to/from the QNames associated with RelationshipType.
  /// </summary>
  public static class RelationshipTypeQNameUtil {

    /// <summary>
    /// Get the known RelationshipType for a given QName. If the QName isn't a known QName, RelationshipType.OTHER will be returned.
    /// </summary>
    public static RelationshipType ConvertFromKnownQName(string qname) {
      if (qname != null) {
        if ("http://gedcomx.org/Couple".Equals(qname)) {
          return RelationshipType.Couple;
        }
        if ("http://gedcomx.org/ParentChild".Equals(qname)) {
          return RelationshipType.ParentChild;
        }
      }
      return RelationshipType.OTHER;
    }

    /// <summary>
    /// Convert the known RelationshipType to a QName. If RelationshipType.OTHER, an ArgumentException will be thrown.
    /// </summary>
    public static string ConvertToKnownQName(RelationshipType known) {
      switch (known) {
        case RelationshipType.Couple:
          return "http://gedcomx.org/Couple";
        case RelationshipType.ParentChild:
          return "http://gedcomx.org/ParentChild";
        default:
          throw new System.ArgumentException("No known QName for: " + known, "known");
      }
    }
  }
}
