
namespace EqualityAndComparator.Test {
    public class Student2 {

        [Comparison(2)] public int Nr;
        [Comparison(3)] public string Name;
        [Comparison(1)] public string Nationality;
        
        public Student2(int Nr, string Name, string Nationality) {
            this.Nr = Nr;
            this.Name = Name;
            this.Nationality = Nationality;
        }
        
    
        public Student2() { }

    }
}