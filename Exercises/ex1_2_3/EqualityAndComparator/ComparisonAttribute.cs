using System;

namespace EqualityAndComparator {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ComparisonAttribute : Attribute {
        private IComparable val;
        private int n = 0;
        public ComparisonAttribute(Type type) {
            if(type==null) throw new ArgumentNullException("type is null");
            if(!type.IsAssignableTo(typeof(IComparable)))
                throw new ArgumentException(type + " must implement IComparable");
            
            this.val = type as IComparable;
        }

        public ComparisonAttribute(int n){
            this.n = n;
        }

        public IComparable getVal(){
            if(val!=null) return val;
            return n;
        }

    }
}
