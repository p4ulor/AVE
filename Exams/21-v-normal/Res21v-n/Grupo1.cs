using System;
using System.Collections; 

public class Grupo1 {
    public static void Main(){
        Student s = new Student();
        Console.WriteLine(s.name);
    }
}
[CompareWith(new String[] {"Birth"})]
public class Student{
    public string name {get; set;}
    public Birth Birth{get; set;} //bruh moment part1

    public IComparable CalculateGradeAvg(){
        return null;
    }
    
}

public class Birth : IComparable  { //bruh moment parte2
    public int Year;
    //public string Year;
    public int CompareTo(object obj) {
        try{
            int n = Int32.Parse((string)obj);
            return n.CompareTo(Year);
        } catch {}
        throw new ArgumentException();
    }

    /* public int CompareTo(object obj) {
        string s = obj as string;
        if(s!=null){
            return s.CompareTo(Year);
        }
        throw new ArgumentException();
    } */
} 
