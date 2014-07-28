var otherThingToFade : GameObject;
var spawnerScript : spawner;

private var done : boolean = false;

function Start () { 
	done = false;
}

function Update () {
	if ((Input.GetButtonDown("Fire1") || Input.touchCount > 0) && !done)
		Fade ();
}

function Fade () {
	done = true;
	
	for (var i : int = 0; i < 75; i++) {
		transform.GetComponent(GUITexture).color.a -= 0.03;
		otherThingToFade.transform.GetComponent(TextMesh).color.a -= 0.1;
		yield WaitForSeconds(0.02);
	}
	
	spawnerScript.Spawn ();
}