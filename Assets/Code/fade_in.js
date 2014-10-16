var spawnerScript : spawner;
var player_C : player_controller;
var pauser : Pause;

private var done : boolean = false;

function Start () { 
	done = false;
}

function Fade () {
	done = true;
	
	if (player_C) 
		player_C.playAnimation ();
		
	while (transform.GetComponent(GUITexture).color.a > 0) {
		transform.GetComponent(GUITexture).color.a -= 0.03;
		yield;
	}
	
	if (pauser)
		pauser.canPause = true;
	
	if (player_C)
		player_C.canMove = true;
	
	if (spawnerScript)
		spawnerScript.Spawn ();
}

function FadeOut () {
	while (transform.GetComponent(GUITexture).color.a < 1) {
		transform.GetComponent(GUITexture).color.a += 0.03;
		yield;
	}
}