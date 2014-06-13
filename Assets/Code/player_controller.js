//The speed at which the player moves towards the move location, should be between 0 and 1
var LERP_SPEED : float;		//A higher number means faster movement
var width : float;
var height : float;
//The last location that the player touches
private var move_location : Vector2 = Vector2.zero;
var friend : GameObject;
//The color of the player, starts out white
var color : Color = Color.white;

private var score : int = 0;

var ADDITIONAL_TIME : int = 50;		//Make this zero if you don't like it

var explosion : GameObject;

//The sprite renderer component of this object
var sprite : SpriteRenderer;
var game_over : GameObject;
var purple :  Color = Color.magenta;
var green : Color = Color.green;
var orange : Color = Color(1, 0.65, 0, 1);
var timer : int = 0;

var highscore : int = 0;

function Start () {
	width = Camera.main.ViewportToWorldPoint(Vector3(1, 1, 10)).x;
	height = Camera.main.ViewportToWorldPoint(Vector3(1, 1, 10)).y;
	
	highscore = PlayerPrefs.GetInt("HighScore");
}

function OnGUI () {
	//Super basic score counter
	GUI.Label (Rect(0, 0, 120, 60), "High Score: " + highscore);	//Set this as a variable at the beginning to use fewer calculations
	GUI.Label (Rect(0, 30, 120, 60), "Score: " + score);
	GUI.Label (Rect(0, 60, 120, 60), "Time: " + timer);
}

function Update () {
	//Takes in player touches and stores the X and Y coordinates in terms of world coordinates
	if (Input.touchCount > 0)
		move_location = Camera.main.ScreenToWorldPoint(Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
	//Takes in mouse input instead
	else if (Input.GetMouseButton(0))
		move_location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
	//Interpolates the location of the player from its current location to the last location touched by the user
	transform.position = Vector2.Lerp(transform.position, move_location, LERP_SPEED);		//TODO: make the speed constant
	
	//Checks if colored and counts down from 30, when 0 is reached, resets to white
	if(timer <= 0){
	  timer = 300;
	  sprite.color = Color.white;
	  color = Color.white;
	 }
	if(timer > 0 && color != Color.white){
	  timer -= Time.deltaTime;
	  if(timer < 100){
	  	if(timer % 20 == 0)
	  		sprite.color = Color.white;
	  	else
	  		sprite.color = color;
	  	}		
	}
}

function OnTriggerEnter2D (other : Collider2D) {
	var exp : GameObject;

	if (other.tag == "Friendly") {
		if (color == Color.white)
			color = other.GetComponent(friendly).color;
		if(color != purple && color != orange && color != green){
			
			if(color == Color.blue){
				if(other.GetComponent(friendly).color == Color.red){
					color = purple;
					
				}
				else if(other.GetComponent(friendly).color == Color.yellow){
					color = green;
				}
				else if (other.GetComponent(friendly).color == Color.blue) {
					timer += ADDITIONAL_TIME;
				}
			}
			else if(color == Color.yellow){
				if(other.GetComponent(friendly).color == Color.red){
					color = orange;
				}
				else if(other.GetComponent(friendly).color == Color.blue){
					color = green;
				}
				else if (other.GetComponent(friendly).color == Color.yellow) {
					timer += ADDITIONAL_TIME;
				}
			}
			else if(color == Color.red){
				if(other.GetComponent(friendly).color == Color.yellow){
					color = orange;
				}
				else if(other.GetComponent(friendly).color == Color.blue){
					color = purple;
				}
				else if (other.GetComponent(friendly).color == Color.red) {
					timer += ADDITIONAL_TIME;
				}
			}	
			var px : float = Random.Range(-width, width);
			var py : float = Random.Range(-height, height);
			var f : GameObject = Instantiate(friend, Vector2(px, py), transform.rotation);
			if(other.GetComponent(friendly).color == Color.red){
				f.GetComponent(friendly).SetColor(0);
			}
			else if(other.GetComponent(friendly).color == Color.yellow){
				f.GetComponent(friendly).SetColor(1);
			}
			else if(other.GetComponent(friendly).color == Color.blue){
				f.GetComponent(friendly).SetColor(2);
			}
			exp = Instantiate(explosion, transform.position, transform.rotation);
			exp.GetComponent(ParticleSystem).startColor = color;
			Destroy(other.gameObject);
		}
			
		sprite.color = color;
	}
	else if (other.tag == "Enemy") {
		if (color == other.GetComponent(enemy).color) {
			Destroy(other.gameObject);
			score += 1000;		//Change this to w/e, doesn't really matter what it is
			timer += ADDITIONAL_TIME;
			exp = Instantiate(explosion, transform.position, transform.rotation);
			exp.GetComponent(ParticleSystem).startColor = color;
		}
		else {
			exp = Instantiate(explosion, transform.position, transform.rotation);
			exp.GetComponent(ParticleSystem).startColor = color;
			
			if (score > PlayerPrefs.GetInt("HighScore")) {
				PlayerPrefs.SetInt("HighScore", score);
				print("New High Score!");
			}
			else if (score == PlayerPrefs.GetInt("HighScore")) {
				print("Tied your old High Score.");
			}
				
			Instantiate (game_over, Vector2.zero, transform.rotation);
			//Time.timeScale = 0;	//This is where we would return you to the main menu or restart the level or something
			Destroy (gameObject);
		}
	}
}