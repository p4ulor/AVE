using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class Validator<T> {
    
   
    private IValidation validation {get; set;}
    private string propertyNameToValidate {get; set;}
    
    private Validator<T> head {get; set;}
    private Validator<T> nextValidator {get; set;}
    private PropertyInfo prop;
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
            if(nextValidator==null){ //entao isto é a inicializaçao da cabeça
                propertyNameToValidate = prop;
                this.validation = validation;
                nextValidator = new Validator<T>(this, type);
                this.prop = type.GetProperty(prop);
                return this; //the head
            }
            Validator<T> getLastOnTheList = nextValidator;
            while(getLastOnTheList.nextValidator!=null){
               getLastOnTheList = getLastOnTheList.nextValidator;
            }

            getLastOnTheList.propertyNameToValidate = prop;
            getLastOnTheList.validation = validation;
            getLastOnTheList.nextValidator = new Validator<T>(getLastOnTheList.head, type);
            getLastOnTheList.prop = type.GetProperty(prop);
            return getLastOnTheList.head;
        }
        throw new ValidationException();
    }
   
    public Validator<T> AddValidation<W>(string s, Func<W, bool> method) {
        if(s==null || s.Length==0|| method==null) throw new ValidationException();
        IValidation validationMethod = new TakeInMethodToValidate<W>(method);
            
        if(nextValidator==null){ //entao isto é a inicializaçao da cabeça
            propertyNameToValidate = s;
            this.validation = validationMethod;
            nextValidator = new Validator<T>(this,type);
            this.prop = type.GetProperty(s);
            return this; //the head
        }
        Validator<T> getLastOnTheList = nextValidator;
        while(getLastOnTheList.nextValidator!=null){
            getLastOnTheList = getLastOnTheList.nextValidator;
        }

        getLastOnTheList.propertyNameToValidate = s;
        getLastOnTheList.validation = validationMethod;
        getLastOnTheList.nextValidator = new Validator<T>(getLastOnTheList.head, type);
        getLastOnTheList.prop = type.GetProperty(s);
        return getLastOnTheList.head;
        
        throw new ValidationException();
    } 

    public void Validate(T o) {
        if(o!=null){
           Validator<T> val = this;
           string s;
            while(val.propertyNameToValidate!=null){
                if(val.validation.Validate(val.prop.GetValue(o))){
                    s="Success";
                } else {
                   s="Failed";  //throw new ValidationException();
                }
                Console.WriteLine(s+" for prop: "+ val.propertyNameToValidate+" for validation: "+val.validation.ToString());
                val=val.nextValidator;
            }   
            return;
        }
        throw new ValidationException();
    }

    
}