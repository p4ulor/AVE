using System; //so we can use string
class C {
    private int val;
    private int val2;
    private String s;
    public C(int v) { 
        val = v; 
    }

    public C(int v1, int v2, int v3) { //to analyise stuff in IL
        val = v1;
        val2 = v2;
        val2 = v3;
        int v4=0;
        int v5=0;
        int v6=0;
        val2 = v4 + v5 + v6;
        int v7;
        int v8;
        int v9;
        v7=-1;
        v8=0;
        v9=v8;
        Oper("");
        Oper("ok");
        v9 = Oper("aaa");
        s="";
    }
    public int V { 
        get { return val; } 
    }
    public int Oper(String s) { 
        return val = V + s.IndexOf('a') + 1; 
    }












    public int ok(Type a, C cl){
        if(a.GetType()!=null) return 0;
        return cl.val;
    }
}