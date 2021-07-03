using System;
using Xunit;

namespace ResTests {
    public class Group1Test {
        [Fact]
        public void Test1() { //(run in debug to see the console.write prints)
            Res.Group1.group1(); //since Group1 has namespace Res, we need to write Res.(...) to reference the class Group1  
        }
    }
}
