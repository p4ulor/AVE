using System;
using System.Reflection;

class TakeInMethodToValidate<W> : IValidation
{
    private Func<W, bool> func;
    public TakeInMethodToValidate (Func<W, bool> func){
        this.func = func;
    }
    public bool Validate(object obj) {
        if(obj!=null){
            return func.Invoke((W)obj);
        }
        return false;
    }
}