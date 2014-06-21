var over : boolean = false;
var press: boolean;

function Update () {
	//Exits the application if the player pressed the back button
	if (Input.GetButtonDown("Exit"))
		Application.Quit();
	if(press == null && (Input.touchCount > 0 || Input.GetButtonDown("Fire1"))){
		press = true;
	}
	if(Input.touchCount == 0 || !Input.anyKeyDown) {
		press = false;
	}
	print(press);
	if (over && !press && (Input.touchCount > 0 || Input.GetButtonDown("Fire1"))){
		Application.LoadLevel(0);
	}
}

function GameOver () {
	over = true;
	Time.timeScale = 0;
}
