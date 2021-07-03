using System;
using Xunit;
namespace ResTests{
    public class Ex6Test{
        [Fact] public void Test1() {
            Student s = new Student(); s.Age = 20; s.Name = "Anacleto";
            Validator<Student> validator = ValidatorBuilder.Build<Student>()
                                        .AddValidation("Age", new NotUnder18())
                                        .AddValidation("Name", new NotNull());
            validator.Validate(s);
            
            //c)
            Validator<Student> validator2 = ValidatorBuilder.Build<Student>()
                                        .AddValidation<String>("Name", UtilMethods.Max50Chars)
                                        .AddValidation<String>("Name", UtilMethods.Max40Chars);
            validator2.Validate(s);
        }
    }
}

class Student {
  public int Age {get; set; }
  public String Name {get; set; }
}

class UtilMethods { 
    public static bool Max50Chars(String s) {    
        if(s.Length<51) return true;
        return false;
    }

    public static bool Max40Chars(String s) {    
        if(s.Length<41) return true;
        return false;
    }
}