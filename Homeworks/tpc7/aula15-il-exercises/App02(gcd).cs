public static int gcd(int m, int n){
	if(m==n){
		return m;
	}
	if(m > n){
		gcd(m-n, n);
	}
	gcd(n-m, m);
}