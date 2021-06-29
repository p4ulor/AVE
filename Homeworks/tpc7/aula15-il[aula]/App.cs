public static int m(int a, int b, int c){
	bool V_0 = c > b;
	int V_1;
	bool V_2;
	if(V_O) {
		V_1 = 1;
		return V_1;
	}
	BOOL V_2 = c < a;
	if(V_2){
		V_1 = -1;
		return V_1;
	}
	V_1 = 0;
	return V_1;
} 
//                      min    max   value
private static int m(int a, int b, int c){
	if(c>b) return 1;
	else if(c < a) return -1;
	else return 0;
}

public static void Main(string[] args){
	Console.WriteLine(m(12,21,19));
	Console.WriteLine(m(12,21,31));
	Console.WriteLine(m(12,21,7));
}