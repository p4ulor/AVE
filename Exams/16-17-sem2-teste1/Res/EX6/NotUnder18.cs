using System;
using System.Reflection;

public class NotUnder18 : IValidation {
    public bool Validate(object obj) {
        if(obj is int){
            if((int)obj>18) return true;
        }
        if(obj is string){
            try{
            int n = Int32.Parse((string)obj);
            if(n>18) return true;
            } catch {}
        }
        return false;
    }
}