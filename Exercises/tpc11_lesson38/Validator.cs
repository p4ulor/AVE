using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class Validator<T> {
    
   
    private IValidation validation {get; set;}
    
    private Validator<T> head {get; set;}
    private Validator<T> nextValidator {get; set;}
    private PropertyInfo property;
    private Type type;

    public Validator () { 
        type=typeof(T);
    }
    public Validator (Validator<T> head, Type t) { 
        this.head = head;
        this.type=t;
    }


    public Validator<T> AddValidation(string prop, object obj){
        IValidation validation = obj as IValidation;
        if (validation!=null && prop!=null && prop.Length!=0) {
            if(head==null) head=this;
            this.validation = validation;
            PropertyInfo propertyInfo = type.GetProperty(prop);
            if(propertyInfo==null) throw new ValidationException();
            this.property = propertyInfo;
            nextValidator = new Validator<T>(head, type);
            return nextValidator;
        }
        throw new ValidationException();
    }
   
    public Validator<T> AddValidation<W>(string prop, Func<W, bool> method) {
        if(prop==null || prop.Length==0|| method==null) throw new ValidationException();
        IValidation validationMethod = new TakeInMethodToValidate<W>(method);
            
        if(head==null) head=this;
        this.validation = validationMethod;
        PropertyInfo propertyInfo = type.GetProperty(prop);
        if(propertyInfo==null) throw new ValidationException();
        this.property = propertyInfo;
        nextValidator = new Validator<T>(head, type);
        return nextValidator;
    } 

    public void Validate(T o) {
        if(o!=null){
           Validator<T> val = head; //this approach simplifies a lot, getting the head only at the evaluation
           string s;
            while(val.property!=null){
                if(val.validation.Validate(val.property.GetValue(o))){
                    s="Success";
                } else {
                   s="Failed";  //throw new ValidationException();
                }
                Console.WriteLine(s+" for prop: "+ val.property.Name+" for validation: "+val.validation.ToString());
                val=val.nextValidator;
            }   
            return;
        }
        throw new ValidationException();
    }

    
}