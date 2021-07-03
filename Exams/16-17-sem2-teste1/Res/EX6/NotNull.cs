using System;
using System.Reflection;


public class NotNull : IValidation
{
    public bool Validate(object obj) {
        if(obj!=null) return true;
        return false;
    }
}