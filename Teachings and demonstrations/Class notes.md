## 1
- CLR(Common language runtime), Virtual Execution enviorment -> runtime
- GC(garbage collector, manages memory)
- The creation of a VM(virtual machines) -> make one distribution for many computer architectures
- When we talk about java, it's more notable or apparant the reference to the JVM, not the language
- kotlinc Point.kt -> compiles to kotlin
- javap -c Point.class -> contains bytecodes + metadata, -c performs disassembly
- metadata describes the types and members

## 2
- VM produces pieces of software = components, which canno be split -> this is a great advantage of VM because it promotes reuse, thanks to the common IL and metadata which can be distributed without problems between systems and programs.
- Metadata is used in compilation, and used in the communication between components and in reflection
- The VM(a managed enviorment, in regards to its components and execution of these), allow the dynamic link/connection in execution time. Per example, the use of libraries in execution time can be late/delayed only in the moment that it needs to be used. And if a component or piece of code is not used, the VM is also aware of that and will for the most of it ignore it.
- Per example: in order to compile, we need all the components referenced, but in execution time, we can actually delete the unused compiled file of the unused component, and the program will run, because the VM is aware that it will not be used
- And if a method will never be called, the IL code will never be converted to native CPU code
- In C++, its used static link, which ocurrs in compilation time, which makes bigger compiled files, which requires to load all the used libraries into the result file.

## 3
- dotnet build(.net) -> javac(java) = compilation
- For each class in java, a .class file will be created
- In dotnet, many classes/types can be grouped into 1 .dll file
- dotnet publish -> produces a more optimized release
- dotnet build -> produces more useful information for debugging
- call(c#) -> invoke special(java) for the main method
- ctor(c#) -> init(java) for the constructor
- using System -> import java.lang*;
- namespace -> package in ava
- ildasm.exe locks writing to the open .dll file. You will need to close ildasm.exe before compiling
- Dependencies:
- In java: add -cp -> java -cp . App.java
- In C#: edit .csproj file
- Use of metadata: IDE intelissense and omnisharp, compiler, ILDASM, reflection, metadata programming
- A type can be defined by a class or interface. The reflection API follows the system types of C#
- A group of types in dotnet is what? A .dll, AKA a component
- A type belongs to an Assembly (file)

## 4
Getting private method:
```csharp
MethodInfo dynMethod = this.GetType().GetMethod("Draw_" + itemType, 
    BindingFlags.NonPublic | BindingFlags.Instance);
dynMethod.Invoke(this, new object[] { methodParams });
```
`Assembly asm = Assembly.LoadFrom("RestSharp.dll");`
- loadFrom and Assembly.Load

## 5
- 2 instances of the same class have the same class, obviously
- dotnet reference `..\FolderName\FolderName.csproj`

## 6
- public record className -> creates getters and setters for the properties
- Compiled code can generator symbols with < and > to avoid conflicts between variables and methods that the compiler can make and what the programmer can write
- Compilation errors and always easier to detect and solve than runtime
- The resulting name of the .dll is the name of the project-folder, that .dll will contain all the types used for that project, including imported ones
- Normally, "record" classes are used for object that are mostly immutable and/or readonly or for things that represent some domain.
- The use of a custom attribute like `[property: customAttributeName]` in a member of type property, will signal the compiler to attribute this custom attribute only to the property(or field, because a property, deep down, is also a field) and not its getters and setters. The methods to get and set will not have this customAttribute

## 7
- keyword `is` is processed and used for compilation time situations. In runtime, we use `.GetType()`
- `object".GetType().isSubclassOf(typeof(IEnumerable))` only works for classes, not interfaces
- `typeof(IEnumerable).IsAssignableFrom("object".getType())` works for all types
- Since methods can only returns 1 value(or reference), the keyword `out` was created, to allow us to alter the value of a variable that was declared before a method call that changes that variable
- `is` is converted to `isinst` in IL
- When using `is`, the `isinst` returns null(when fails) or the reference(on sucess), but then this value is converted from object to boolean, thus allowing it to return true or false. 
- In IL: `ld`(load) -> push, `st`(store) -> pop, `br` -> branch (AKA jump)
- `castclass` in IL is used when making something like `Method1((IEnumerable) obj)`
- advantage of `as` over `is`: it doesn't waste the as `isinst` operation

## 8
- Metaprogramming -> we program our own software to produce other software. We program code to produce another level of code(similar to a compiler). Resume from wikipedia: means that a program can be designed to read, generate, analyze or transform other programs, and even modify itself while running.[1][2] In some cases, this allows programmers to minimize the number of lines of code to express a solution, in turn reducing development time.[3] It also allows programs greater flexibility to efficiently handle new situations without recompilation.
- We perform meta programming using System.Reflection.Emit. The Assembly class represents a component(a logical functioning unit)
- Module builder allows the division of a component in many files(it has a questionable use)

## 9
- Each instance has a htype value, also called type handle, which points to its type
- htype points to RRTI (run time type information) != System.Reflection.Type
- RRTI is a native structure which the programer doesn't have acess to
- objects of the same type, have htype pointing to the same reference
- Are structs more heavy on memory that in references?
- Disavantage 1: parameter passing, an entire copy of the values is done when passing it to a method
- Advantage 1: more efficient field acess, theres less redirecting(of pointers) to obtain data
- Advantage 2: value types(structs) aren't processed by the GC(garbage collector), because they only live during the execution of a method(between the opening and closing braces { }) or use of an instance
- What is the life cycle of an instance? It's up to the GC
- A struct can only implement an interface, and it can't extend from another struct

## 10
- Instances at runtime RT(reference types) vs VT(value types)
- `new` of a struct -> inicialization -> stored in place(stack, unless its its in place that was instantiated) -> initobj(IL)
- `new` of a class -> instantiation -> stored in memory heap -> newobj(IL)
- Thus we can conclude that the use of `new` can have 2 different implications and meaning when compiled to IL
- `ldloca.s` -> load local adress stack -> push of local variable
- newobj -> alocation of memoery in heap, initialized space with zeros + contains Header(htype pointing to RTTI), calls the constructor and returns the instance
- initobj -> inicialization of memory in place, receives as parameters the addres of the allocated space.
- { and } opens and closes a stack
- if we explicetely call the constructor of a struct(which has been defined in our code) that takes 2 values, in IL, it's performed a ldc(load constant) of the 2 values and then its performed a explicit call to the constructor(example at the end of this page)
- `callvirt` can be used to call both virtual and instance methods that were overridden in `this` subclass
- ldsfld -> load static field
- throughput -> number of operations in a time slot

___

### Person is a class
`Person p = new Person();`

in IL:
```csharp
new obj instance void project.Person::.ctor()
stloc.0 //as you can see, the previous call returns a pointer to the address of the instance allocated in the heap, and we store it
```

### Point is a struct
`Point pt = new Point()`

in IL:
```csharp
ldloca.s V_1
initobj project.Point
```

`Point pt2 = new Point(5,7)`

in IL:
```csharp
ldloca.s V_2
ldc.i4.5
ldc.i4.7
call void Point::.ctor(int32, int32)
```
