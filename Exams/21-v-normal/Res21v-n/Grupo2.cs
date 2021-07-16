using System;
using System.Collections; //to acess IComparer and IComparable
using System.Collections.Generic; //so we can do List<IComparer>
using System.Reflection; //for ...Info and custom attributes

public class Grupo2 {
    public static void a(){
        //eles aqui enganaram-se, a prop em Stduent é 'name' com minuscula
        PropertyComparer p = new PropertyComparer(typeof(Student).GetProperty("name")); 
        MethodComparer m = new MethodComparer(typeof(Student).GetMethod("CalculateGradeAvg"));
        Cmp cmp = new Cmp(typeof(Student));
        cmp.Then<Student, int>(studen => studen.Birth.Year);
    }
}

/* public interface IComparable { int CompareTo(object obj); }
public interface IComparer { int Compare(object x, object y); } */

//1
public class PropertyComparer : IComparer {
    private PropertyInfo prop;
    public PropertyComparer(PropertyInfo p) {
        //IComparable ic = p as IComparable; or this, and see if ic is null in the if.
        if(p==null)  throw new NullReferenceException();
        if(p.PropertyType.IsAssignableTo(typeof(IComparable))){
            prop=p;
        } else throw new ArgumentException(p.Name+" must be of a comparable type!");
        
    }
    public int Compare(object x, object y) {
        if(x==null || y==null) throw new NullReferenceException();
        PropertyInfo px = x.GetType().GetProperty(prop.Name);
        PropertyInfo py = y.GetType().GetProperty(prop.Name);
        if(px!=null && py!=null){
            if(px.PropertyType!=prop.PropertyType && py.PropertyType!=prop.PropertyType){
                IComparable xc = px.GetValue(x) as IComparable;
                IComparable yc = py.GetValue(y) as IComparable;
                if(xc!=null && yc!=null){
                    return xc.CompareTo(yc);    
                }
            }
        }
        throw new ArgumentException();
    }
}

//2
public class MethodComparer : IComparer {
    private MethodInfo method;
    public MethodComparer(MethodInfo m) {
        if(m==null)  throw new NullReferenceException();
        if(!(m.ReturnType.IsAssignableTo(typeof(IComparable)))) 
            throw new ArgumentException("Return type incompatible with IComparable!");
        if(m.GetParameters().Length!=0) 
            throw new ArgumentException("Method receives parameters!");
        method=m;
    }
    public int Compare(object x, object y) {
        if(x==null || y==null) throw new NullReferenceException();
        MethodInfo px = x.GetType().GetMethod(method.Name);
        MethodInfo py = y.GetType().GetMethod(method.Name);
        if(px!=null && py!=null){
            if(px.ReturnType!=method.ReturnType && py.ReturnType!=method.ReturnType){
                IComparable xc = px.Invoke(x, null) as IComparable;
                IComparable yc = py.Invoke(y, null) as IComparable;
                if(xc!=null && yc!=null){
                    return xc.CompareTo(yc);    
                }
            }
        }
        throw new ArgumentException();
    }
}


//3

[AttributeUsage(AttributeTargets.Class)]
public class CompareWith : Attribute {
    private String[] members;
    public CompareWith(String[] s){
        members=s;
    }

    public String[] GetNames(){
        return members;
    }
}

//4
public class Cmp : IComparer {
    readonly List<IComparer> props = new List<IComparer>();
    public Cmp(Type t){
        String[] memberNames = ((CompareWith) Attribute.GetCustomAttribute(t,typeof(CompareWith))).GetNames();
        if(memberNames==null || memberNames.Length==0) throw new CustomAttributeFormatException();
        PropertyInfo[] properties = t.GetProperties();
        MethodInfo[] methods = t.GetMethods();
    
        foreach(String s in memberNames){
            foreach(MethodInfo m in methods){
                if(s.Equals(m.Name)) props.Add(new MethodComparer(m));          
            }
            foreach(PropertyInfo p in properties){
                if(s.Equals(p.Name)) props.Add(new PropertyComparer(p));            
            }
        }
        
    }
    public int Compare(object x, object y){
        foreach(IComparer c in props){
            int res = c.Compare(x,y);
            if(res !=0) return res;
        }
        return 0;
    }

    public IComparer Then<T,R>(Func<T, R> function){ //nao sei se isto ta certo
        if(function.Method.ReturnType.IsAssignableTo(typeof(IComparer))){
            //IComparable val = function.Method.Invoke(T.GetType(), null); ?
            IComparer prop = function.Method.ReturnType as IComparer;
            if(prop!=null) {
                props.Add(prop);  
                return prop;
            }
        }
        return null;
    }

    
}
