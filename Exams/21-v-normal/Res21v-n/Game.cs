using System;
using System.Reflection;
public class Game {
    [Sammy(Nr =71)]
    public static void Foo(){
        MethodInfo m = typeof(Game).GetMethod("Foo");
        Sammy sam = (Sammy) m.GetCustomAttribute(typeof(Sammy), false);
        Console.WriteLine(sam.Nr);
        sam.Nr++;
        Console.WriteLine(sam.Nr);
    }
}

class Sammy : Attribute {
    public int Nr {get; set;}
}