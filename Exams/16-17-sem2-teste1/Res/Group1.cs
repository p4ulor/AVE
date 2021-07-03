using System;
using System.Collections;
using System.Collections.Generic;

namespace Res {
    public class Group1 { //GRUPO1
        public static void group1(){
            int a = 5;
            printn(a.ToString());

            object b = 10;
            
            //IEnumerable c; //-> System.Collections;

            printn((typeof(DateTime).GetType() == typeof(String).GetType()).ToString());
            
        } 

        private static void printn(String s){
            Console.WriteLine(s);
        }

        //Func<K,X> X(the last parameter in <...>) is the return type
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

    }
}
//IEnumerable<T> -> System.Collections.Generic;
//namespace N { class C<W> : IEnumerable<T> { void M(W w, T t) { } } } //uncomment to see error

