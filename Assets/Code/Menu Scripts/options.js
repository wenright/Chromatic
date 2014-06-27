var explosion : GameObject;

function OnCollisionEnter2D () {
	var e : GameObject = Instantiate(explosion, transform.position, transform.rotation);
	e.GetComponent(ParticleSystem).startColor = Color.blue;
	yield WaitForSeconds(1);
	
	//Put option sutff here
}