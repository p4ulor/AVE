using System;


public class Grupo1 {
    public static void Main(){
        Student s = new Student();
        Console.WriteLine(s.name);
    }
}

struct Student{
    public string name {get; set;}
    
}
