using System;
using System.Reflection;
using Comparison = EqualityAndComparator.ComparisonAttribute; //replaces System.Comparison 
using System.Text; //for StringBuilder
using ComparisonV2 = EqualityAndComparator.ComparisonV2Attribute;
namespace EqualityAndComparator {
    public class ComparatorV2 : IComparer {
        private Type type;
        private FieldInfo[] fields;
        public ComparatorV2(Type type) {
            if(type==null) throw new ArgumentNullException("type is null");
            this.type = type;

            fields = type.GetFields();
            for(ushort i = 0; i<fields.Length; i++){
                StringBuilder s = new StringBuilder("doesn't implement IComparable");
                if(fields[i].FieldType.IsAssignableTo(typeof(IComparable))) { }    
                else if(!Attribute.IsDefined(fields[i], typeof(Comparison))==true) {
                    s.Append(" and does not contain the ComparisonAttribute");
                    throw new ArgumentException("The field "+fields[i].Name+" "+s);
                    //(ComparisonAttribute already makes sure that the value it stores IsAssignableTo typeof(IComparable)), so, no need to check
                } else throw new ArgumentException("The field "+fields[i].Name+" "+s);   
            }
        }
        public int Compare(object x, object y) { //compares fields if it contains IComparable OR field has the ComparisonAttribute
            if(x==null && y==null) return 0;
            if(x==null) return 1;
            if(y==null) return -1;
            if(x.GetHashCode()==y.GetHashCode()) return 0;
            
            if(x.GetType()==type && y.GetType()==type) {
                for(ushort i = 0; i < fields.Length; i++) {
                    int retval = 0;
                    FieldInfo f = fields[i];
                    if(f.FieldType.IsAssignableTo(typeof(IComparable))) { 
                        IComparable xcomp = f.GetValue(x) as IComparable;
                        IComparable ycomp = f.GetValue(y) as IComparable;
                        if(xcomp==null && ycomp==null) return 0; //without this if, there could be a NPE afterwards
                        retval = xcomp.CompareTo(ycomp);
                        if(retval!=0) return retval;
                    } else {
                        ComparisonV2 attrb = (ComparisonV2) Attribute.GetCustomAttribute(f, typeof(ComparisonV2));
                        retval = attrb.getVal().Compare(f.GetValue(x), f.GetValue(y));
                        if(retval!=0) return retval;
                    }
                }
                return 0;
            }
            return -1;
        }
    }
}