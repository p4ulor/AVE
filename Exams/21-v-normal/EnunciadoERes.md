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
___
## Grupo 2

As interfaces IComparable e IComparer do dotnet são equivalentes às suas homólogas no Java Comparable e Comparator. Estas interaces são definidas em dotnet por:
```csharp
public interface IComparable { int CompareTo(object obj); }
public interface IComparer { int Compare(object x, object y); }
```
1 - Implemente a classe `PropertyComparer` que implementa IComparer e compara dois objetos segundo a **propriedade** no construtor.

E.g. `new PropertyComparer(typeof(Student)).GetProperty("Name"));`

Para tal, o tipo da propriedade especificada deve ser compatível com IComparable. Caso não seja, o construtor lança a exceção `ArgumentException` com a mensagem, e.g. "Name must be of a comparable type!"

2 - Implemente a classe `MethodComparer` que implementa IComparer e compara dois objetos segundo o **método** especificado no construtor.

E.g. `new MethodComparer(typeof(Student)).GetMethod("CalculateGradeAvg"));`

Para tal, o método especificado não pode receber parâmetros e tem que ter um tipo de retorno compatível com IComparable. Caso não cumpra alguma destas condições, lança a exceção `ArgumentException` com a mensagem em conformidade com o erro.

3 - Pretende-se anotar classes de domínio com o *custom attribute* que indique os nomes dos membros(propriedades ou métodos) a usar como critério de comparação, conforme indicado no exemplo seguinte:

```csharp
[CompareWith(new String[] {"Number", "Address", "CalculateGradesAvg"})]
```

Implemente o *custom attribute* `CompareWith` de acordo com a utilização apresentada.

4 - Considere parte da definição da classe `Cmp` que representa um comparador(i.e. `IComparer`) que compara objetos de domínio, segundo os membros especificados na anotação `CompareWith`.

O representante da classe de domínio é passado como parâmetro ao construtor, e.g. `new Cmp(typeof(Student))`.

Implemente a construtor de `Cmp` que preenche a lista da `props` com instâncias das classes definidas no ponto 1 e 2, correspondentes aos membros indicados na anotação `CompareWith`, sobre a classe domínio

```csharp
public class Cmp : IComparer {
    readonly List<IComparer> props = new List<IComparer>();
    ...
    public int Compare(object x, object y){
        foreach(IComparer c in props){
            int res = c.Compare(x,y);
            if(res !=0) return res;
        }
        return 0;
    }
}
```

5 - Pretende-se adicionar um método `Then` a `Cmp` que adiciona um critério de comparação, baseado numa função que recebe um objeto de domínio por parâmetro e retorna um valor comparável (i.e. `IComparable` **(tenho quase a certeza que aqui devia ser IComparer visto que a lista props é `List<IComparer> props`)**).

Por exemplo, considere que se quer adicionar como critério o ano da data de nascimento, dado uma instância de `Cmp` referida por uma variável cmp, pretende-se fazer `cmp.Then<Student, int>(st => st.Birth.Year);`
- Defina a assinatura da função `Then` incluindo os seus parâmetros genéricos.
- Defina o tipo delegate que é o tipo do parãmetro da função `Then`.

6 - Implemente o método `Then` cumprindo a funcionalidade especificada sem alterar e nem adicionar nada na classe Cmp. Para além da implementação do método `Then()`, pode apenas criar novas classes auxiliares.
