using System;

namespace EqualityAndComparator {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ComparisonV2Attribute : Attribute {
        private IComparer val;
        public ComparisonV2Attribute(Type type) {
            if(type==null) throw new ArgumentNullException("type is null");
            if(!type.IsAssignableTo(typeof(IComparer)))
                throw new ArgumentException(type + " must implement IComparer");
            
            this.val = type as IComparer;
        }
        public IComparer getVal(){
            return val;
        }

    }
}
