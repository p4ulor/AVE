using System;
using System.Reflection;
using System.Collections.Generic;
using Comparison = EqualityAndComparator.ComparisonAttribute; //replaces System.Comparison 


namespace EqualityAndComparator {
    public class Comparator : IComparer {
        private Type type;
        SortedDictionary<IComparable, FieldInfo> order = new SortedDictionary<IComparable, FieldInfo>();
        public Comparator(Type type) {
            if(type==null) throw new ArgumentNullException("type is null");
            this.type = type;
            setFieldComparisionOrder();
        }

        private void setFieldComparisionOrder(){ //(and check if its comparable and contains the custom attribute Comparison)
            FieldInfo[] fields = type.GetFields();
            for(ushort i = 0; i<fields.Length; i++){
                if(fields[i].FieldType.IsAssignableTo(typeof(IComparable))) { 
                    Comparison attrb = (Comparison) Attribute.GetCustomAttribute(fields[i], typeof(Comparison));
                    if(attrb!=null) { 
                        try { 
                            order.Add(attrb.getVal(), fields[i]); //automatically orders by the key after adding
                        } catch(ArgumentException) {
                            throw new ArgumentException("Duplicate key / duplicate custom attribute value = "+attrb.getVal());
                        }   
                    }
                } else throw new ArgumentException("The field "+fields[i].Name+" isn't compatible with IComparable");
            }
        }
        public int Compare(object x, object y) { //compare in proper order and only fields with the attribute Comparison
            if(x==null && y==null) return 0;
            if(x==null) return 1;
            if(y==null) return -1;
            if(x.GetHashCode()==y.GetHashCode()) return 0;

            if(x.GetType()==type && y.GetType()==type) {
                foreach(KeyValuePair<IComparable, FieldInfo> kvp in order){
                    FieldInfo f = kvp.Value;
                    IComparable xcomp = f.GetValue(x) as IComparable;
                    IComparable ycomp = f.GetValue(y) as IComparable;
                    if(xcomp==null && ycomp==null) return 0; //without this if, there could be a NPE afterwards
                    int retval = xcomp.CompareTo(ycomp);
                    if(retval!=0) return retval;
                }  
                return 0;
            }
            return -1;
        }
    }
}