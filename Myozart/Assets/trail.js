#pragma strict

var rb: Rigidbody;
//var ps: ParticleSystem;
var prevPos : Vector3;
var beenthere: boolean;

function Start() {
	rb = GetComponent.<Rigidbody>();
//	ps = GetComponent.<ParticleSystem>();
	prevPos = GetComponent.<Transform>().position;
	beenthere = false;
}


function Update () {
//	if(rb.velocity.magnitude < 10) {
//		ps.Emit(1);
//	} else {
//		ps.Emit(0);
//	}
//	var n = rb.velocity;
//	if(!prevPos) {
//		prevPos = transform.position;
//	}
	n = new Vector3(0, 0, 0) - n;
	var n = prevPos - transform.position;
	if(n.magnitude > 0.1)
	{
		ps.transform.rotation = Quaternion.LookRotation(n, Vector3.up);
		
//		Debug.Log("it's not zero");
		beenthere = false;
	}
	else {
//		Debug.Log("it's basicallly zero");
		ps.Emit(50);
		beenthere = true;
	}

	
	prevPos = transform.position;
}

function velocity(position) {
	
}