using System;
using System.Reflection;


class NotNull : IValidation
{
    public bool Validate(object obj) {
        if(obj!=null) return true;
        return false;
    }
}