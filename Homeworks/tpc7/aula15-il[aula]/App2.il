.method private hidebysig static float64
		Mode(int 32x,
		     int32y) cil managed
	{
	//Code size 23 (0x17)
	.maxstack 2
	.locals init(int 32 V_0,
	 	      int32 V_1,
		      float64 V_2)
	IL_0001:  ldarg.0
	IL_0002:  ldarg.0
	IL_0003:  mul
	IL_0004:  stloc.0
	IL_0005:  ldarg.1
	IL_0006:  ldarg.1
	IL_0007:  mul
	IL_0008:  stloc.1
	IL_0009:  ldloc.0
	IL_000a:  ldloc.1
	IL_000b:  add
	IL_000c:  conv.r8
	IL_000d:  call float64 [System.Runtime]System.Math::Sqrt(float64)
	IL_0012:  stloc.2
	IL_0013:  br.s		IL_0015

	IL_0015:  ldloc.2
	IL_0016:  ret
} //end of method Program::Mod