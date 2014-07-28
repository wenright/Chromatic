var spawnerScript : spawner;
var menuButtons : GameObject;

private var done : boolean = false;

function Start () { 
	done = false;
}

function Fade () {
	done = true;
	
	for (var i : int = 0; i < 75; i++) {
		transform.GetComponent(GUITexture).color.a -= 0.03;
		yield WaitForSeconds(0.02);
	}
	
	menuButtons.SetActive (false);
	
	if (spawnerScript)
		spawnerScript.Spawn ();
}