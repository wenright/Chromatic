var explosion : GameObject;
var fade : GameObject;

function OnCollisionEnter2D () {
	var e : GameObject = Instantiate(explosion, transform.position, transform.rotation);
	e.GetComponent(ParticleSystem).startColor = Color.red;
	for (var i : int = 0; i < 50; i++) {
		fade.GetComponent(GUITexture).color.a += 0.03;
		yield WaitForSeconds(0.01);
	}
	Application.Quit();
}