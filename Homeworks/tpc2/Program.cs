using System;
using System.Reflection;
namespace Program {
    class Program { 

        static void Main(string[] args) {
            Assembly asm = Assembly.LoadFrom("RestSharp.dll");
            Console.WriteLine("Information:{0}",asm.FullName);  

            Type[] types = asm.GetTypes();  
            foreach (Type tp in types) {  
                Console.WriteLine("Type:{0}", tp/*.ToString()*/); 

                MemberInfo[] mb = tp.GetMembers(); //only publics
                foreach(MemberInfo member in mb){
                    Console.WriteLine("\n   Method:{0}", member);
                }
                Console.WriteLine("\n");
            }
        }
    }
 
}
