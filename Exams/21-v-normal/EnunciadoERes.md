# 5 Julho 2021, 1:30, Grupo 1[6v], Grupo 2[14v]

## Grupo 1
## Considere:
```csharp
class Student{
    public string name {get; set;}
}
```

### 1 - Escreva em IL o resultado da compilação do metodo Main
```csharp
 public static void Main(){
        Student s = new Student();
        Console.WriteLine(s.name);
}
```
### Res:
```
newobj     instance void Student::.ctor()
stloc.0
ldloc.0
callvirt   instance string Student::get_name()
call       void [System.Console]System.Console::WriteLine(string)
ret
```

### 2 - Dada a resposta anterior, modifique as instruções IL que seriam diferentes caso Student fosse definido como struct em vez de class
``` c
ldloca.s   V_0
initobj    Student
ldloca.s   V_0
call     //instance string Student::get_name()
//call       void [System.Console]System.Console::WriteLine(string)
//ret
```

![](understanding%20IL_structVSclass.png)

### 3 - Explique numa frase porque é que uma struct não pode definir metodos virtuais?

```
Porque são tipos de tipo valor e porque as structs não suportam herança
```

### 4 - Qual o output da execução do método `Test()`? Justifique
[Game.cs](Res21v-n/Game.cs)
```
Output:
71
72
71
72
Em Foo(), o valor do campo Nr, da instancia de Attribute obtido da metadata do método Foo(), é incrementado tal como um objeto normal durante a execução do método estático Foo(). Na segunda chamada ao método, a informação guardada no Attribute vai ser lida novamente da metadata(que não muda entre chamadas) cujo valor Nr(igual a 71)
```