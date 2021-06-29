public static void Foo(int nr){
	Console.Write(nr);
	Console.Write(", ");
	if(nr == 1) {
		Console.WriteLine();
		//return;
	} 
	else if(nr % 2 ==0){
		Foo(nr/2);
		//return;
	}
	else {
		Foo(nr*3 + 1);
	}
	//return;
}



public static void Main(string[] args){
	Foo(12);
	Foo(19);
}