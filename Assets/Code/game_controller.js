var over : boolean = false;
var can_restart: boolean = false;
var fader: fade_in;
private var bg_color : Color = Color(2/255, 20/255, 26/255, 0.05);

function Start () {
	GetComponent.<Camera>().backgroundColor = Color.white / 9;
	fader.GetComponent(GUITexture).color = Color.black;
	fader.Fade();
	Application.targetFrameRate = 60;
}

function Update () {
	//Exits the application if the player pressed the back button
	if (Input.GetButtonDown("Exit"))
		Application.Quit();
		
	#if UNITY_EDITOR
		if (Input.GetKeyDown("f"))
			Cursor.visible = false;
	#endif
		
	if (over && !can_restart)
		if (Input.touchCount == 0)
			can_restart = true;

	if (over && can_restart && (Input.touchCount > 0 || Input.GetButtonDown("Fire1"))){
		Application.LoadLevel("main");
	}
		
	GetComponent.<Camera>().backgroundColor = Color.Lerp(GetComponent.<Camera>().backgroundColor, bg_color, .1);
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