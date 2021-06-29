using System;
using System.Reflection;
using System.Reflection.Emit;

namespace aula16_meta_programming
{
    class Program
    {
        static void Main(string[] args)
        {
            Type dynType = BuildDynamicAssemblyAndType();
            // 
            // <=> obj = new MyDynamicType(7);
            // 
            object obj = Activator.CreateInstance(dynType, new object[]{7}); 
            // 
            // <=> obj.MyMethod(5);
            // 
            object res = dynType.GetMethod("MyMethod", new Type[]{typeof(int)}).Invoke(obj, new object[]{5});
            Console.WriteLine("new MyDynamicType(7).MyMethod(5) = " + res);
            //
            // <=> obj.MyMethod(new MyDynamicType(9)) ----> 63
            // 
            object other = Activator.CreateInstance(dynType, new object[]{9}); 
            object a = dynType.GetMethod("MyMethod3", new Type[]{}).Invoke(other, null);
            Console.WriteLine("MyDynamicType(9) = " + a);


            object res2 = dynType
                    .GetMethod("MyMethod2", new Type[]{dynType}).Invoke(obj, new object[]{other});
            Console.WriteLine("new MyDynamicType(7).MyMethod2(new MyDynamicType(9)) = " + res2);
        }
        private static Type BuildDynamicAssemblyAndType()
        {
            AssemblyName aName = new AssemblyName("DynamicAssemblyExample");
            AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(
                aName,
                AssemblyBuilderAccess.Run);

            // For a single-module assembly, the module name is usually
            // the assembly name plus an extension.
            ModuleBuilder mb = ab.DefineDynamicModule(aName.Name);

            return BuildType(mb);
        }

        private static Type BuildType(ModuleBuilder mb)
        {
            // Create a new type MyDynamicType
            TypeBuilder tb = mb.DefineType("MyDynamicType",TypeAttributes.Public);

            // Add a private field of type int (Int32).
            FieldBuilder fbNumber = tb.DefineField("m_number", typeof(int), FieldAttributes.Public);
            
            AddConstructor(tb, fbNumber);
            MethodBuilder method1 = AddMyMethod(tb, fbNumber);
            MethodBuilder method3 = AddMyMethod3(tb, fbNumber);
            AddMyMethod2(tb, fbNumber, method1); //alternatvely, make it receive method3(get number method)
            
            // Finish the type.
            Type t = tb.CreateType();
            return t;
        }

         private static MethodBuilder AddMyMethod3(TypeBuilder tb, FieldBuilder fbNumber)
        {
            MethodBuilder meth = tb.DefineMethod(
                "MyMethod3",
                MethodAttributes.Public,
                typeof(int),
                null);
            

            ILGenerator methIL = meth.GetILGenerator();
            methIL.Emit(OpCodes.Ldarg_0);         // push this
            methIL.Emit(OpCodes.Ldfld, fbNumber); // push field from .m_number
            methIL.Emit(OpCodes.Ret);
            return meth;
        }

        private static void AddMyMethod2(TypeBuilder tb, FieldBuilder fbNumber, MethodBuilder method)
        {
            MethodBuilder meth = tb.DefineMethod(
                "MyMethod2",
                MethodAttributes.Public,
                typeof(int),
                new Type[] { tb });
            

            ILGenerator methIL = meth.GetILGenerator();
            methIL.Emit(OpCodes.Ldarg_0);         // push this
            
            methIL.Emit(OpCodes.Ldarg_1);       //push 1st parameter
            methIL.Emit(OpCodes.Ldfld, fbNumber); // push field from this.m_number
            methIL.Emit(OpCodes.Call,method);
            //methIL.EmitCall(OpCodes.Call,method, new Type[]{typeof(int)});
            
            //alternative: receive getMethod(method3). parameter method would have to be method3 for the following to work
            //methIL.Emit(OpCodes.Ldfld, fbNumber); // push field from this.m_number
            //methIL.Emit(OpCodes.Ldarg_1);       //push 1st parameter
            //methIL.Emit(OpCodes.Call,method);
            //methIL.Emit(OpCodes.Mul);             // this.m_number * retorned value from method;

            methIL.Emit(OpCodes.Ret);
        }

        private static MethodBuilder AddMyMethod(TypeBuilder tb, FieldBuilder fbNumber)
        {
            // Define a method that accepts an integer argument and returns
            // the product of that integer and the private field m_number. This
            // time, the array of parameter types is created on the fly.
            MethodBuilder meth = tb.DefineMethod(
                "MyMethod",
                MethodAttributes.Public,
                typeof(int),
                new Type[] { typeof(int) });

            ILGenerator methIL = meth.GetILGenerator();
            // To retrieve the private instance field, load the instance it
            // belongs to (argument zero). After loading the field, load the
            // argument one and then multiply. Return from the method with
            // the return value (the product of the two numbers) on the
            // execution stack.
            methIL.Emit(OpCodes.Ldarg_0);         // push this
            methIL.Emit(OpCodes.Ldfld, fbNumber); // push this.m_number
            methIL.Emit(OpCodes.Ldarg_1);         // push arg
            methIL.Emit(OpCodes.Mul);             // this.m_number * arg;
            methIL.Emit(OpCodes.Ret);
            return meth;
        }

        private static void AddConstructor(TypeBuilder tb, FieldInfo fbNumber)
        {
            // Define a constructor that takes an integer argument and
            // stores it in the private field.
            Type[] parameterTypes = { typeof(int) };
            ConstructorBuilder ctor1 = tb.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                parameterTypes);
            ConstructorInfo baseCtor = typeof(object).GetConstructor(Type.EmptyTypes);
            ILGenerator ctor1IL = ctor1.GetILGenerator();
            ctor1IL.Emit(OpCodes.Ldarg_0);         // push this
            ctor1IL.Emit(OpCodes.Call, baseCtor);  // Call base construtor => Object constructor.
            ctor1IL.Emit(OpCodes.Ldarg_0);         // push this
            ctor1IL.Emit(OpCodes.Ldarg_1);         // push the int argument
            ctor1IL.Emit(OpCodes.Stfld, fbNumber); // store field
            ctor1IL.Emit(OpCodes.Ret);
        }
    }
}
