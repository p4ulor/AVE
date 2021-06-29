using System;

namespace tpc11_lesson38
{
    class Program
    {
        static void Main(string[] args)
        {
            Student s = new Student(); s.Age = 20; s.Name = "Anacleto";
            Validator<Student> validator = new Validator<Student>()
                            .AddValidation("Age", new Above18())
                            .AddValidation("Name", new NotNull());
            validator.Validate(s);

            Validator<Student> validator1 = new Validator<Student>().AddValidation("Age", new Above18());
            Validator<Student> validator2 = validator1.AddValidation("Name", new NotNull());
            Student s1 = new Student(); s1.Age = 20; s1.Name = null;
            validator1.Validate(s1); // => succesful 
            Console.WriteLine();
            validator2.Validate(s1); // => ValidationException
            //ex2
             Console.WriteLine("\nEx2:\n");
            Validator<Student> validator3 = new Validator<Student>().AddValidation<String>("Name", UtilMethods.Max50Chars);
            validator3.AddValidation<String>("Name", UtilMethods.Max40Chars);
            validator3.Validate(s); // => succesful 

        }
    }
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
