using System;
using System.Reflection;
using System.Text;

namespace Logger
{
    public class Log
    {

        private readonly IPrinter printer; 
        public Log(IPrinter p)
        {

            printer = p;
        }

        public Log() : this(new ConsolePrinter()) //this calls the constructor on top (super())
        {
            
        }

        public void Info(object o) {
            string output = Inspect(o);
            printer.Print(output);
        }

        private string Inspect(object o)
        {
            string membersStr = LogMembers(o);

            return membersStr;
        }


        private string LogMembers(object o)
        {
            Type t = o.GetType();
                
            StringBuilder str = new StringBuilder();
            MemberInfo[] members = t.GetMembers();
            
            foreach (MemberInfo member in members){
                

                if (ShouldLog(member))
                {

                    str.Append(member.Name);
                    str.Append(": ");
                    str.Append(GetValue(o, member));
                    //str.Append(field.GetValue(o));
                    str.Append(", ");
                }
            }
            return str.ToString();

        }

        private bool ShouldLog(MemberInfo m) //if its a field or a method with 0 param, we print
        {
            return (m.MemberType == MemberTypes.Field || 
                (m.MemberType == MemberTypes.Method && (m as MethodInfo).GetParameters().Length == 0)
            ) && isToBeLogged(m); //CHANGED

        }

        private bool isToBeLogged(MemberInfo m){ //CHANGED
            try{
                //                                                            dotnet type system does not require annotation to be attribute 
                Object[] atrb = m.GetCustomAttributes(typeof(ToLog), true); //retorna object, porque o c# obriga a que um customattribute seja qualquer coisa derivada de attribute, mas plataforma permite que usem um tipo qualquer como anotaçao
                //                                             bool inherit, true->entao queremos procurar tambem na classe base. Se o membro é um metodo, que é um metodo virtual que esta a ser redifinido. ele pode nao ter a anotaçao na classe que a redifiniu, mas pode ter no tipo base
                //bool isDef = Attribute.IsDefined(m, typeof(ToLog)); //alternative
                //Object atrb = Attribute.GetCustomAttribute(m,typeof(ToLog)); //forma estatica
                if(atrb.Length!=0/*isDef*/) return true;
                /*System.Attribute[] attrs = System.Attribute.GetCustomAttributes(m); outra alternativa?
                foreach (System.Attribute attr in attrs)  {  
                    if (attr is ToLog) {  
                        return true;
                    }  
                }  */
            } catch (Exception e) {
                Console.WriteLine("An exception occurred: {0}", e.Message);
            }
            return false;
        }
        private object GetValue(object target, MemberInfo m) {
            switch(m.MemberType)
            {
                case MemberTypes.Field:
                    return (m as FieldInfo).GetValue(target);
                case MemberTypes.Method: 
                    return (m as MethodInfo).Invoke(target, null);
                default:
                    throw new InvalidOperationException("Non properly member for logging!");
            }
        }

        private class ConsolePrinter : IPrinter
        {
            public void Print(string output)
            {
                Console.WriteLine(output);
            }
        }
    }
}
