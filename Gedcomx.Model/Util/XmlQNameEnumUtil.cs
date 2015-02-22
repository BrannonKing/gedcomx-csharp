using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gedcomx.Model.Util
{
    public static class XmlQNameEnumUtil
    {
        public static String GetNameValue(Enum value)
        {
            var attribute = value.GetType().GetTypeInfo().GetDeclaredField(value.ToString()).GetCustomAttribute<XmlEnumAttribute>();
            String result;

            if (attribute != null)
            {
                result = attribute.Name;
            }
            else
            {
                result = value.ToString();
            }

            return result;
        }

        public static T GetEnumValue<T>(String value)
        {

            var found = Enum.GetValues(typeof(T)).Cast<Enum>().FirstOrDefault(x => GetNameValue(x) == value);
            T result = default(T);

            if (found is T)
            {
                result = (T)(object)found;
            }

            return result;
        }
    }
}
