using System;
using System.Reflection;

class Above18 : IValidation
{
    public bool Validate(object obj) {
        if(obj!=null){
            if(obj is int){
                if((int)obj>18) return true;
            }
        }
        return false;
    }
}