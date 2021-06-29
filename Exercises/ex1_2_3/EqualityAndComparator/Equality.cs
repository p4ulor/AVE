using System;
using System.Reflection;



namespace EqualityAndComparator {
    public class Equality : IEquality {
        private Type type;
        private PropertyInfo[] propertiesInfo;

        public Equality(Type type, params string[] properties) {
            if(type==null || properties==null) throw new ArgumentNullException("Contains null arguments");
            this.type = type;
            propertiesInfo = new PropertyInfo[properties.Length];
            for (ushort i = 0; i < properties.Length; i++) { 
                if(string.IsNullOrEmpty(properties[i])) throw new ArgumentException("Empty or null string at property index "+ i);
                propertiesInfo[i] = type.GetProperty(properties[i]);
                if(propertiesInfo[i]  == null) 
                    throw new ArgumentException("There's no property called " + properties[i]);
            } 
        }

        public bool AreEqual(object x, object y) { //only compares properties(and those set in propertiesInfo)
            if(x==null && y==null) return true;
            if(x==null || y==null) return false;
            if(x.GetHashCode()==y.GetHashCode()) return true; //or just ==
            
            if(x.GetType()==type && y.GetType()==type) {
                for(ushort i = 0; i < propertiesInfo.Length; i++) {
                    if (!propertiesInfo[i].GetValue(x).Equals(propertiesInfo[i].GetValue(y))) return false;
                }
                return true;
            }
            return false;
        }
    }
}