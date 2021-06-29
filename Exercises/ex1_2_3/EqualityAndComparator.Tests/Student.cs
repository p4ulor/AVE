
namespace EqualityAndComparator.Test {
    public class Student {

        [Comparison(2)] public int Nr { get; }
        [Comparison(3)] public string Name { get; }
        public School School { get; }
        [Comparison(1)] public string Nationality { get; }
        
        public Student(int Nr, string Name, string Nationality) : this(Nr, Name, null, Nationality) {

        }
        
        public Student(int Nr, string Name, School School, string Nationality) {
            this.Nr = Nr;
            this.Name = Name;
            this.School = School;
            this.Nationality = Nationality;
        }
        public Student() { }

    }
}