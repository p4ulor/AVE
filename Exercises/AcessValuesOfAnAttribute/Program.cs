using System;
using System.Reflection;

//https://stackoverflow.com/questions/6538366/access-to-the-value-of-a-custom-attribute/46341017
namespace AcessValuesOfAnAttribute {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            Type[] types = new Type[1];
            Type t = typeof(ok);
            ConstructorInfo[] cf = t.GetConstructors();
            foreach(ConstructorInfo construc in cf){
                Notes note = (Notes) construc.GetCustomAttribute(typeof (Notes), false);
                if(note!=null){
                    Console.WriteLine(note.Value);
                }
            }
        }
    }
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple=false, Inherited = true)]
class Notes : Attribute {   
    public string Value{ get; private set; }
    public Notes (string value) {
        this.Value= value;
    }

}

public class ok {
    [Notes("2")]
    public ok(){

    }
}