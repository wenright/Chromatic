//The speed at which the player moves towards the move location, should be between 0 and 1
var LERP_SPEED : float;		//A higher number means faster movement

//The last location that the player touches
private var move_location : Vector2 = Vector2.zero;

function Update () {
	//Takes in player touches and stores the X and Y coordinates in terms of world coordinates
	if (Input.touchCount > 0)
		move_location = Camera.main.ScreenToWorldPoint(Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
	//Takes in mouse input instead
	else if (Input.GetMouseButton(0))
		move_location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	
	//Interpolates the location of the player from its current location to the last location touched by the user
	transform.position = Vector2.Lerp(transform.position, move_location, LERP_SPEED);
	
	//Exits the application if the player pressed the back button
	if (Input.GetButtonDown("Exit"))
		Application.Quit();
}