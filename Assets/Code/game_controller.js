var over : boolean = false;
var can_restart: boolean = false;

function Update () {
	//Exits the application if the player pressed the back button
	if (Input.GetButtonDown("Exit"))
		Application.Quit();
		
	if (over && !can_restart)
		if (Input.touchCount == 0)
			can_restart = true;

	if (over && can_restart && (Input.touchCount > 0 || Input.GetButtonDown("Fire1")))
		Application.LoadLevel(2);
}

function GameOver () {
	if (Input.touchCount == 0)
		can_restart = true;
	over = true;
}
