var explosion : GameObject;
var fade : GameObject;
var menu_object : GameObject;

function OnCollisionEnter2D () {
	var e : GameObject = Instantiate(explosion, transform.position, transform.rotation);
	e.GetComponent(ParticleSystem).startColor = Color.yellow;
	for (var i : int = 0; i < 50; i++) {
		fade.GetComponent(GUITexture).color.a += 0.03;
		yield WaitForSeconds(0.01);
	}
	
	menu_object.GetComponent(menu).ask_for_name = true;
}