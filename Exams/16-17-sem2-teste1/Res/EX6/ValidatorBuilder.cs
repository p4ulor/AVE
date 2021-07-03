using System;

public class ValidatorBuilder{
    public static Validator<T> Build<T>() { //é só isto? lol
        return new Validator<T>();
    } 
}