var over : boolean = false;

function Update () {
	//Exits the application if the player pressed the back button
	if (Input.GetButtonDown("Exit"))
		Application.Quit();
		
	if (over && (Input.touchCount > 0 || Input.GetButtonDown("Fire1"))) 
		Application.LoadLevel(0);
}

function GameOver () {
	over = true;
}