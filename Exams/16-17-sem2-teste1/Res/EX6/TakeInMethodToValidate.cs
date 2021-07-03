using System;
using System.Reflection;
class TakeInMethodToValidate<W> : IValidation //c)
{
    private Func<W, bool> func;
    public TakeInMethodToValidate (Func<W, bool> func){
        this.func = func;
    }
    public bool Validate(object obj) {
        if(obj is W) //this Validate only validates objects whose types our stored method can accept
            return func.Invoke((W)obj);
        //throw new TypeMismatchException(); é pedido para meter isto, mas nao existe?
        return false;
    }
}