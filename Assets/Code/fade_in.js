var spawnerScript : spawner;
var menuButtons : GameObject[];
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
	
	for (var i : int = 0; i < 15; i++) {
		transform.GetComponent(GUITexture).color.a -= 0.03;
		for (var j : int = 0; j < menuButtons.Length; j++){
			if(menuButtons[j].GetComponent(SpriteRenderer))
				menuButtons[j].GetComponent(SpriteRenderer).color.a -= 0.1;
			else
				menuButtons[j].GetComponent(TextMesh).color.a -= 0.1;
		}
		
		yield WaitForSeconds(0.01);
	}
	if (pauser)
		pauser.canPause = true;
	transform.GetComponent(GUITexture).color.a = 0;
	for (var k : int = 0; k < menuButtons.Length; k++){
		if(menuButtons[k].GetComponent(SpriteRenderer))
			menuButtons[k].GetComponent(SpriteRenderer).color.a = 0;
		else 
			menuButtons[k].GetComponent(TextMesh).color.a = 0;
		
		}
	
	if (player_C)
		player_C.canMove = true;
	
	if (spawnerScript)
		spawnerScript.Spawn ();
}