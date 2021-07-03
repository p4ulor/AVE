using System;
using System.Reflection;

class TakeInMethodToValidate<W> : IValidation
{
    private Func<W, bool> func;
    public TakeInMethodToValidate (Func<W, bool> func){
        this.func = func;
    }
    public bool Validate(object obj) {
        if(obj is W) return func.Invoke((W)obj);
        //throw new TypeMismatchException(); nao existe?
        return false;
    }
}