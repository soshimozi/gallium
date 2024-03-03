class foo {
	function bar(a : int): none {
		print a;
	}
}

class baz : foo {
	__()__ {
		// constructor
		//super();
	}

	function bar(a : int) : none {
		super.bar(a);
	}
}

var x : int = 0;

for (;;) {
	//print x;
}