var over : boolean = false;
var can_restart: boolean = false;
var fader: fade_in;
private var bg_color : Color = Color(2/255, 20/255, 26/255, 0.05);

function Start () {
	camera.backgroundColor = Color.white / 9;
	fader.Fade();
	Application.targetFrameRate = 60;
}

function Update () {
	//Exits the application if the player pressed the back button
	if (Input.GetButtonDown("Exit"))
		Application.Quit();
		
	if (over && !can_restart)
		if (Input.touchCount == 0)
			can_restart = true;

	if (over && can_restart && (Input.touchCount > 0 || Input.GetButtonDown("Fire1"))){
		Application.LoadLevel("main");
	}
		
	camera.backgroundColor = Color.Lerp(camera.backgroundColor, bg_color, .1);
}

function GameOver () {
	if (Input.touchCount == 0)
		can_restart = true;
	over = true;
}

function ChangeBackgroundColor (color : Color) {
	if(color == Color.white)
		bg_color = color / 8;
	else
		bg_color = color / 7;
}