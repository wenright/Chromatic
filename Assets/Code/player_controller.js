//The speed at which the player moves towards the move location, should be between 0 and 1
var LERP_SPEED : float;		//A higher number means faster movement

//The last location that the player touches
private var move_location : Vector2 = Vector2.zero;

//The color of the player, starts out white
var color : Color = Color.white;

//The sprite renderer component of this object
<<<<<<< HEAD
var sprite : SpriteRenderer;
var purple :  Color = Color.magenta;
var green : Color = Color.green;
var orange : Color = Color(1, 0.65, 0, 1);
var timer : int = 0;
=======
private var sprite : SpriteRenderer;

>>>>>>> FETCH_HEAD
function Start () {
	sprite = GetComponent(SpriteRenderer);
}

function Update () {
	//Takes in player touches and stores the X and Y coordinates in terms of world coordinates
	if (Input.touchCount > 0)
		move_location = Camera.main.ScreenToWorldPoint(Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
	//Takes in mouse input instead
	else if (Input.GetMouseButton(0))
		move_location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	//Checks if colored and counts down from 30, when 0 is reached, resets to white
	if(timer <= 0){
	  timer = 100;
	  sprite.color = Color.white;
	  color = Color.white;
	 }
	if(timer > 0 && color != Color.white){
	  timer -= Time.deltaTime;
	}
	//Interpolates the location of the player from its current location to the last location touched by the user
	transform.position = Vector2.Lerp(transform.position, move_location, LERP_SPEED);
	
	//Exits the application if the player pressed the back button
	if (Input.GetButtonDown("Exit"))
		Application.Quit();
	
		
		
}

function OnTriggerEnter2D (other : Collider2D) {
	if (other.tag == "Friendly") {
		if (color == Color.white)
			color = other.GetComponent(friendly).color;
		else if(color == Color.blue){
			if(other.GetComponent(friendly).color == Color.red)
				color = purple;
			else if(other.GetComponent(friendly).color == Color.yellow)
				color = green;
		}
		else if(color == Color.yellow){
			if(other.GetComponent(friendly).color == Color.red)
				color = orange;
			else if(other.GetComponent(friendly).color == Color.blue)
				color = green;
		}
		else if(color == Color.red){
			if(other.GetComponent(friendly).color == Color.yellow)
				color = orange;
			else if(other.GetComponent(friendly).color == Color.blue)
				color = purple;
		}
			
		
		sprite.color = color;
		
		Destroy(other.gameObject);
	}
	else if (other.tag == "Enemy") {
		print("You died! probably.  Unless you were the right color and you shouldnt have died because that hasnt been implemented yet, sorry :(");
		Destroy (gameObject);
	}
}