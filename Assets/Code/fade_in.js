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
		
	for (var i : int = 0; i < 15; i++)
		transform.GetComponent(GUITexture).color.a -= 0.03;
	
	if (pauser)
		pauser.canPause = true;
	
	if (player_C)
		player_C.canMove = true;
	
	if (spawnerScript)
		spawnerScript.Spawn ();
}