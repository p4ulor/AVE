using System;
class Grupo2 { 
    delegate void Action(int i); 

    class A { //I have no idea if this is correct
    public delegate void M1(int i);
    M1 method = M;
    static Action Identify(Action a){
        if(a==null){
            return null;
        }   
        return new Grupo2.Action(new Grupo2.A().method);
    
    } 

    static void M(int i){
       
    }
    
    }
}