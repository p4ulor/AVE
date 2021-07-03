## Group 1 (V e F / True and false)
## 1
**a. F**, All types extend from object. So all times can override ToString(), GetType(), GetHashCode() and Equals() per example

**b. V**, Since value types extend from object, a boxing conversion occurs.

In IL:
```
ldc.i4.s 10
box [System.Runtime]System.Int32
stloc.0
```

**c. F**, If IEnumerable is generic in T, class C should also add the generic type T, but it's just W. Class A should start with:
- class C<W,T>

Plus, class C doesn't implement the interface method GetEnumarator()

**d. V**, typeof() returns an object of type Type. Thus, getting the type of the type

## 2
Soon(materia nao dada)

## 3

*Se viste a aula de res de exercicios vais ver q eles meteram tudo isto a falso, mas acho que se enganaram ou entao nao perceberam a propria pergunta tenho quase a certeza. De maneira nenhuma que acrescetar mais instruçoes e dados nao aumenta o espaço de um objeto...*

**a. V** More data = more bytes = more space. Properties, internally, are also fields, which occupy memory. And for each of these properties, there's also going to be created (implicitly, or explicitly: if the programmer so desires do make his own setters and getters) methods(at compile time by the compiler) in order to produce the IL code(which is also data) that will be common to all instances

**b. V** More data = more bytes

**c. F** This question doesn't even make sense. And the number of instances there are of an class doesn't effect the size of other instances

**d. V** More data = more bytes. If we change the definition of the class by adding more functionality(which are instructions), ALL instances of that class will occupy more memory. Methods make up IL code with takes up memory

## 4
**a. F** Custom attributes are metadata, it doesn't affect the instance of a class

**b. V** They can be instantiated just like any class

**c. F** You can't create constructors with delegates

**d. F** Depends on the definition of the custom attribute. Specificaly, on in own custom attribute
```csharp
[AttributeUsage(AttributeTargets.Class |  AttributeTargets.Method)]
```
## 5
IDK if this is correct...
```csharp
public static Dictionary<K, List<E>> CollectBy<K,E>(List<E> src, Func<E,K> toKey, Func<E, E> toElem){
        Dictionary<K, List<E>> res = new Dictionary<K, List<E>>();
        foreach(E item in src) {
            K key = toKey(item); //toKey receives "item", and its part of src(item in src), thus they have the same type
            List<E> elems;
            if(!res.TryGetValue(key, out elems)) { 
                elems = new List<E>(); res[key] = elems; 
            }
            elems.Add(toElem(item));
        }
        return res;
}
```

## [6](https://github.com/p4ulor/AVE/tree/main/Exams/16-17-sem2-teste1/EX6) In folder EX6 
