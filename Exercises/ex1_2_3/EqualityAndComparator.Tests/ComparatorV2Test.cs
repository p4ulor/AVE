using Xunit;

namespace EqualityAndComparator.Test {
    public class ComparatorV2Test {
        public ComparatorV2Test() { }

        [Fact]
        public void Test1() {
            Student3 s1 = new Student3(14000, "Ana", new Address("Rua Amarela", 24), new Account(9900));
            Student3 s2 = new Student3(14000, "Ana", new Address("Rua Rosa", 24), new Account(9900));
            Student3 s3 = new Student3(14000, "Ana", new Address("Rua Rosa", 24), new Account(100));
            Student3 s4 = new Student3(14000, "Ana", new Address("Rua Rosa", 97), new Account(100));
            IComparer cmp = new ComparatorV2(typeof(Student3)); 
            int res1 = cmp.Compare(s1, s2); // res < 0 porque Rua Amarela é menor que Rua Rosa
            int res2 = cmp.Compare(s2, s3); // res > 0 porque 9900 é maior que 100
            int res3 = cmp.Compare(s3, s4); // res = 0 porque os critérios de comparação de todos os campos dão 0 
        }

    }

}