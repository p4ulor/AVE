using Xunit;

namespace EqualityAndComparator.Test {
    public class EqualityTest {
        public EqualityTest() { }

        [Fact]
        public void Test1() {
            Student s1 = new Student(5232, "nome", new School("escola", "ali"), "outro");
            Student s2 = new Student(5232, "nome", new School("escola", "ali"), "portuguesa");
            
            IEquality eq1 = new Equality(typeof(Student), "Nr", "Name", "School");
            bool res1 = eq1.AreEqual(s1, s2); // res1 = true se s1 e s2 tiverem o mesmo Nr, Name e School
            Assert.True(res1);

            IEquality eq2 = new Equality(typeof(Student), "Nr", "Nationality");
            bool res2 = eq2.AreEqual(s1, s2); // res2 = true se s1 e s2 tiverem o mesmo Nr e Nationality
            Assert.False(res2);

            Assert.True(eq2.AreEqual(null,null));
            Assert.False(eq2.AreEqual(null, s1));
        }

    }

}