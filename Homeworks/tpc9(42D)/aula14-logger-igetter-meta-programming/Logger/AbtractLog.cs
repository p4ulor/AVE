﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Logger
{
    public abstract class AbstractLog  {

        private readonly IPrinter printer; 
        private readonly Dictionary<Type, List<IGetter>> members = new Dictionary<Type, List<IGetter>>();

        public AbstractLog(IPrinter p)
        {
            printer = p;
        }

        public AbstractLog() : this(new ConsolePrinter())
        {
            
        }

        public void Info(object o)  {
            //
            // Check if o is an IEnumerable
            //
            // Option 1: string output = typeof(IEnumerable).IsAssignableFrom(o.GetType())
            // Option 2: string output = o is IEnumerable
            // Option 3: 
            IEnumerable seq = o as IEnumerable; // as is translated to isinst
            string output = seq != null
                ? Inspect(seq)
                : Inspect(o);
            printer.Print(output);
        }

        private string Inspect(IEnumerable seq) {
            StringBuilder str = new StringBuilder();
            str.Append("Array of:\n");
            foreach(object item in seq) {
                str.Append("\t");
                str.Append(Inspect(item));
                str.Append("\n");
            }
            return str.ToString();
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
            foreach (IGetter member in GetMembers(t)) // Compilador de C# => cast explicito de IGetter para MemberInfo
            {
                str.Append(member.GetName());
                str.Append(": ");
                str.Append(member.GetValue(o));
                str.Append(", ");
            }
            if(str.Length > 0) str.Length -= 2;
            return str.ToString();

        }

        private IEnumerable<IGetter> GetMembers(Type t)
        {
            // First checj if exist in members dictionary
            List<IGetter> ms;
            if(!members.TryGetValue(t, out ms)) {
                ms = new List<IGetter>();
                foreach(MemberInfo m in t.GetMembers()) {
                    IGetter getter;
                    if(ShouldLog(m, out getter))
                    {
                        ms.Add(getter);
                    }
                }
                members.Add(t, ms);
            }
            return ms;
        }

        /// <summary>
        /// Validate if that member m should be logged, i.e. it must have a ToLog annotation
        /// and be a field or a parameterless method.
        /// Also it may return an instance of GetterField or GetterMethod.
        /// </summary>
        private bool ShouldLog(MemberInfo m, out IGetter getter)
        {
            getter = null;
            /**
             * Check if it is annotated with ToLog
             */
            if(!Attribute.IsDefined(m,typeof(ToLogAttribute))) return false;
            /**
             * Check if it is a Field
             */
            if(m.MemberType == MemberTypes.Field)
            {
                getter = CreateGetterField((FieldInfo) m);
                return true;
            }
            /**
             * Check if it is a parameterless method
             */
            if(m.MemberType == MemberTypes.Method  && (m as MethodInfo).GetParameters().Length == 0)
            {
                getter = CreateGetterMethod((MethodInfo) m);
                return true;
            }
            return false;
        }

        protected abstract IGetter CreateGetterField(FieldInfo field);
        protected abstract IGetter CreateGetterMethod(MethodInfo method);
        
        private class ConsolePrinter : IPrinter
        {
            public void Print(string output)
            {
                Console.WriteLine(output);
            }
        }
    }
}
