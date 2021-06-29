using System;
//tambem ha [AttributeUsage(AttributeTargets.All, , AllowMultiple=true)]
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, AllowMultiple=true)] //allows multiple attribute usage
public class ToLogAttribute : Attribute {
    private string label ="";
    public ToLogAttribute(String label)
    {
        this.label = label;
    }

    public ToLogAttribute()
    {
        
    }

    public string getLabel(){
        return label;
    }

    
    /*public override string ToString(){
        getLabel();
    }*/

}