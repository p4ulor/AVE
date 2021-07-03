using System;
using System.Collections.Generic; //for LinkedList<T>
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class Validator<T> { //when creating a structure do represent the Validator list, the head is null. 
    private IValidation validation {get; set;}
    private PropertyInfo property;
    private Type type;

    private LinkedList<Validator<T>> list = new LinkedList<Validator<T>>();


    public Validator () { 
        type=typeof(T); //aqui podia-se inicializar um campo que seria um array com todas as propriedades para evitar GetProperties repetidos
    }
    public Validator (IValidation validation, PropertyInfo prop, Type type) { 
        this.validation = validation;
        this.property=prop;
        this.type = type; //provavelmente desnecessario mas whatever
    }

    public Validator<T> AddValidation(string prop, object obj){

        IValidation validation = obj as IValidation;
        if (validation!=null && prop!=null && prop.Length!=0) {
            PropertyInfo propInfo = type.GetProperty(prop);
            if(propInfo==null)  throw new ValidationException(); // property doesnt exist
            list.AddLast(new Validator<T>(validation, propInfo, type));
            return this;
        }
        throw new ValidationException();
    }
   
    public Validator<T> AddValidation<W>(string prop, Func<W, bool> method) {
        if(prop==null || prop.Length==0|| method==null) throw new ValidationException();
        IValidation validationMethod = new TakeInMethodToValidate<W>(method);

        PropertyInfo propInfo = type.GetProperty(prop);
        if(propInfo==null)  throw new ValidationException(); //of property doesnt exist
        
        list.AddLast(new Validator<T>(validationMethod, propInfo, type));
        return this;
    }

    public void Validate(T o) {
        if(o!=null && o.GetType()==type){
            String s;
            foreach(Validator<T> val in list){
                if(val.validation.Validate(val.property.GetValue(o))){
                    s="Success";
                } else {
                   s="Failed";  //throw new ValidationException();
                }
                Console.WriteLine(s+" for prop: "+ val.property.Name+" for validation: "+val.validation.ToString());
            }   
            return;
        }
        throw new ValidationException();
    }

    
}