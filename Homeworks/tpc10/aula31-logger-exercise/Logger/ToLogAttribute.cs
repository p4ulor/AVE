using System;
using System.Reflection;
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, AllowMultiple=true)]
public class ToLogAttribute : Attribute {
    readonly IFormatter form;//{ get; private set; }
    readonly String label;
    public ToLogAttribute(Type formatterType, params string[] args) {
        if(typeof(IFormatter).IsAssignableFrom(formatterType)){
            if(args.Length==0){
                form = (IFormatter) Activator.CreateInstance(formatterType, args);
            } else {
               form = (IFormatter) Activator.CreateInstance(formatterType);
            }
        }
    }

    public ToLogAttribute(String label) {
        this.label=label;
    }

    public ToLogAttribute() {
        
    }

}