using Xunit;

namespace EqualityAndComparator.Test {
    public class ComparatorTest {
        public ComparatorTest() { }

        [Fact]
        public void Test1() {
            Student2 s1 = new Student2(12000, "Ana", "pt");
            Student2 s2 = new Student2(14000, "Ana", "pt");
            Student2 s3 = new Student2(11000, "Ana", "en");
            Student2 s4 = new Student2(11000, "Ana", "en");
            IComparer cmp = new Comparator(typeof(Student2)); // Usa como critérios: 1o nationality e 2o nr
            int res1 = cmp.Compare(s1, s2); // res < 0 porque têm a mesma nationality e 12000 é menor que 14000
            Assert.True(res1<0);
            
            int res1_1 = cmp.Compare(s1, s1); //same instance
            Assert.True(res1_1==0);
            
            int res2 = cmp.Compare(s2, s3); // res > 0 porque “pt” é maior que “en"
            Assert.True(res2>0);
            
            int res3 = cmp.Compare(s3, s4); // res == 0 porque "Ana" é igual a "Ana"
            Assert.True(res3==0);  

            Student2 s5 = new Student2(3, "", "");
            Student2 s6 = new Student2(3, "", "");
            Assert.True(cmp.Compare(s5,s6)==0);

            Student2 s7 = new Student2(3, null, null);
            Student2 s8 = new Student2(3, null, null);
            Assert.True(cmp.Compare(s7,s8)==0);

            Assert.True(cmp.Compare(s5,s7)>0);
        }

        [Fact]
        public void Test2() {
            IComparer cmp = new Comparator(typeof(Student2));

            //ordem de comparaçao      2º  3º  1º
            Student2 s9 = new Student2(3, "A", "");
            Student2 s10 = new Student2(3, "B", "");
            Assert.True(cmp.Compare(s9,s10)<0); //A<B   s9<s10

            Student2 s11 = new Student2(3, "A", "");
            Student2 s12 = new Student2(2, "B", ""); //embora A<B, avalia-se o Nr antes disso
            Assert.True(cmp.Compare(s11,s12)>0); //3>2   s9>s10

            Assert.True(cmp.Compare(null,null)==0);
            Assert.True(cmp.Compare(null, s12)>0);
        }

    }

}