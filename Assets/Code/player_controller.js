//Please Add Variables to the proper area!!
//SetUp
var width : float;
var height : float;
//Movement
var MOVE_SPEED : float;
private var move_location : Vector2 = Vector2.zero;
var tolerance : float;//tolerance for spawning a friendly
//Explosion variables
var EXPLOSION_RADIUS : int = 7;
var EXPLOSION_FORCE : float = 2000;
//Colors
var color : Color = Color.white;
var purple :  Color = Color(191/255.0F, 0, 1, 1);
var green : Color = Color(0, 1, 0, 1);
var orange : Color = Color(1, 127/255.0F, 0, 1); 				
//Game Objects
var GameController : GameObject;
var SpawnController : GameObject;
var friend : GameObject;
var line : GameObject;
var explosion : GameObject;
var ScoreText : GameObject;
var sprite : SpriteRenderer; //The sprite renderer component of this object
var trail : TrailRenderer;
var game_over : GameObject;
//Time
private var timer : int = 0;
var ADDITIONAL_TIME : int = 75;
var MAX_TIME : int = 200;
//Score
private var multiplier : int = 1;
private var best_multiplier : int = 0;
private var score : int = 0;
private var highscore : int = 0;
var BaseScore : int = 100;
//Rage
private var rage_mode : boolean = false;
private var rage_timer : int = 0;
//Sounds
var enemy_killed : AudioClip;
var player_killed : AudioClip;
var player_hit : AudioClip;
//Misc (add random shit to be sorted here)
var dead : boolean = false;


function Start () {
	width = Camera.main.ViewportToWorldPoint(Vector3(1, 1, 10)).x; //Set width to viewport width
	height = Camera.main.ViewportToWorldPoint(Vector3(1, 1, 10)).y; //Set height to viewport height
	highscore = PlayerPrefs.GetInt("HighScore"); //Get the high score
	dead = false;
}

function OnGUI () {
	//Super basic score counter(WILL BE REPLACED!!!)
	GUI.Label (Rect(0, 0, 120, 60), "High Score: " + highscore);
	GUI.Label (Rect(0, 30, 120, 60), "Score: " + score);
	GUI.Label (Rect(0, 60, 120, 60), "Time: " + timer);
}

function Update () {
	if (!dead) {
		//Takes in player touches and stores the X and Y coordinates in terms of world coordinates
		if (Input.touchCount > 0)
			move_location = Camera.main.ScreenToWorldPoint(Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
		//Takes in mouse input instead
		else if (Input.GetMouseButton(0))
			move_location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
		if(Vector2.Distance(move_location, transform.position) < 7) //checks to see if finger/mouse is within range
			transform.position = Vector2.MoveTowards(transform.position, move_location, Time.deltaTime * MOVE_SPEED); //moves towards that location
		
		//Checks if colored and counts down from 200, when 0 is reached, resets to white
		if(timer > 0 && color != Color.white){
		  timer -= Time.deltaTime;
		   //Pulsing Animation Code-->
		   if(timer <= 100){
		  	if(timer % 20 == 0){
		  		transform.localScale = new Vector3(1.8, 1.8, 1);
		  	}
		  	else if(timer % 20 == 19){
		  		transform.localScale = new Vector3(1.9, 1.9, 1);;
		  	}
		  	else if(timer % 20 == 18){
		  		transform.localScale = new Vector3(2, 2, 1);
		  	}
		  	else if(timer % 20 == 17){
		  		transform.localScale = new Vector3(2.15, 2.15, 1);
		  	}
		  	else if(timer % 20 == 16){
		  		transform.localScale = new Vector3(2.15, 2.15, 1);
		  	}
		  	else if(timer % 20 == 15){
		  		transform.localScale = new Vector3(2, 2, 1);
		  	}
		  	else if(timer % 20 == 14){
		  		transform.localScale = new Vector3(1.9, 1.9, 1);
		  	}
		  	else if(timer % 20 == 13){
		  		transform.localScale = new Vector3(1.8, 1.8, 1);
		  	}
		  	else{
		  		transform.localScale = new Vector3(1.75, 1.75, 1);
		  	}
		  }	
		  //<--
		}
		else {
			//Time has run out
			transform.localScale = new Vector3(1.75, 1.75, 1); //Set size back to normal
			sprite.color = Color.white; //Set color to white
		  	color = Color.white; //change the color variable to white
		  	trail.material.SetColor("_Color", color);
		  	timer = MAX_TIME; //Reset timer
		  	multiplier = 1; //Multiplier to 1
		}
		//Rage Code-->
		if (rage_timer > 0) {
			rage_timer -= Time.deltaTime;
			var ran : int = Random.Range(1, 4);
			switch (ran) {
				case 1: color = orange;
					break;
				case 2: color = purple;
					break;
				case 3: color = green;
					break;
				default: break;
			}
			sprite.color = color;
			trail.material.SetColor("_Color", color);
		}
		else
			rage_mode = false;
		//<--
		if (color == Color.white) {
			line.transform.localScale.x = 0;//Reset timer to 0 size if white
		}
		else {
			line.transform.localScale.x = timer/30.0; //Make it the size of time_left/30
			line.GetComponent(SpriteRenderer).color = color;//set the color to the current color
		}
	}
}

function OnTriggerEnter2D (other : Collider2D) {
	if (!dead) {
		var exp : GameObject; // creates the explosion gameobject
		if (other.tag == "Friendly" && !rage_mode) {
			transform.localScale = new Vector3(1.75, 1.75, 1); //makes sure size is set to full
			////////////////////////////////////////////////
			//TODO: MAKE BETTER!!
			if (color == Color.white)
				color = other.GetComponent(friendly).color; //Sets base color
			if(color != purple && color != orange && color != green){
				if(color == Color.blue){
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
				timer = MAX_TIME; //Reset time when you pick up a color
			}
			else {
				timer = MAX_TIME; //Reset time 
				color = other.GetComponent(friendly).color;
			}
			////////////////////////////////////////////////
			var counter: int = 0;
			multiplier = 1;
			var temp_color : Color = other.GetComponent(friendly).color;
			Destroy(other.gameObject);
			exp = Instantiate(explosion, transform.position, transform.rotation);
			exp.GetComponent(ParticleSystem).startColor = color;
			sprite.color = color;
			trail.material.SetColor("_Color", color);
			
			yield WaitForSeconds(1);	//Wait a bit
			
			do {					//Worst case scenario, this loop could go on forever.  May want to add a counter and have this loop give up once it reaches a certain number
				var px : float = Random.Range(-width, width);
				var py : float = Random.Range(-height, height);
				counter ++;
				if(counter > 100)
					break;
			} while (!CheckPosition(px, py));
			
			var f : GameObject = Instantiate(friend, Vector2(px, py), transform.rotation);
			if(temp_color == Color.red)
				f.GetComponent(friendly).SetColor(0);
			else if(temp_color == Color.yellow)
				f.GetComponent(friendly).SetColor(1);
			else if(temp_color == Color.blue)
				f.GetComponent(friendly).SetColor(2);
		}
		else if (other.tag == "Enemy") {
			if (color == other.GetComponent(enemy).color || rage_mode) { //if the ball is the correct color
				Destroy(other.gameObject);
				score += BaseScore * multiplier;		//Change this to w/e, doesn't really matter what it is
				var sc_txt : GameObject = Instantiate(ScoreText, transform.position, transform.rotation);
				sc_txt.GetComponent(TextMesh).text = "X" + multiplier;
				//sc_txt.GetComponent(TextMesh).color = other.GetComponent(enemy).color;		//<- if you want the color to be the same as the object destroyed
				multiplier++;
				if(multiplier > best_multiplier)
					best_multiplier = multiplier; //sets high multiplier
				if (multiplier == 5) {
					rage_mode = true; //turns on rage mode
					if (timer < MAX_TIME)	timer = MAX_TIME;
					rage_timer = timer; //sets timer to remainging time
				}
				if(!rage_mode)
					timer += ADDITIONAL_TIME; //add aditional time per kill not not on ragemode
				exp = Instantiate(explosion, transform.position, transform.rotation); //explode
				exp.GetComponent(ParticleSystem).startColor = color; //particles for explosion
				audio.PlayOneShot(enemy_killed);
			}
			else if(color == Color.white) { //if the ball is white
				
				Instantiate (game_over, Vector3(0, 0, -1), transform.rotation); //Game over screen
				
				var hs : GameObject = Instantiate(game_over, Vector3(0, -1, -1), transform.rotation);
				
				if (score > PlayerPrefs.GetInt("HighScore")) {
					PlayerPrefs.SetInt("HighScore", score); //Saves highscore
					hs.GetComponent(TextMesh).text = "New High Score! " + score;//prints highscore
				}
				else {
					hs.GetComponent(TextMesh).text = "Score: " + score; //prints score
				}
				
				GameController.GetComponent(game_controller).GameOver();
				SpawnController.GetComponent(spawner).GameOver();
				audio.PlayOneShot(player_killed);
				dead = true;
				Destroy(gameObject);
			}
			else {//if the ball has the wrong color
				multiplier = 1; //reset multiplier
				timer = 0; //reset timer
				color = Color.white; //reset color
				Destroy(other.gameObject); //destory other object
				score -= BaseScore; //subtract penalty points
				Camera.main.GetComponent(shake_script).Shake();//shake camera
				audio.PlayOneShot(player_hit);
				explode ();
				
				exp = Instantiate(explosion, transform.position, transform.rotation);//explode
				exp.GetComponent(ParticleSystem).startColor = color;//particles
				
				var minus_text : GameObject = Instantiate(ScoreText, transform.position, transform.rotation);//minus text
				minus_text.GetComponent(TextMesh).text = "-" + BaseScore;//print loss
				minus_text.GetComponent(TextMesh).color = Color.red;//set loss to red
			}
		}
	}
}

//Checks the x, y coordinates to see if any objects are too close.  Returns true if good location, false otherwise.
function CheckPosition (x : float, y : float) {
	if ((transform.position.x - x) < tolerance && (transform.position.y - y) < tolerance)
		return false;

	var f : GameObject[] = GameObject.FindGameObjectsWithTag("Friendly");
	var e : GameObject[] = GameObject.FindGameObjectsWithTag("Enemy");
	
	for (var i : int = 0; i < f.Length; i++)
		if ((f[i].transform.position.x - x) < tolerance && (f[i].transform.position.y - y) < tolerance)
			return false;
	for (var j : int = 0; j < e.Length; j++)
		if ((e[j].transform.position.x - x) < tolerance && (e[j].transform.position.y - y) < tolerance)
			return false;
			
	return true;
}

//Takes all the rigidbodies in the scene and adds force pushing them away from the player
function explode () {
	var all_rigidbodies = FindObjectsOfType(Rigidbody2D);
	
	for (var r : Rigidbody2D in all_rigidbodies){
		if (Vector2.Distance(r.transform.position, transform.position) < EXPLOSION_RADIUS && r.tag != "Player")
			r.AddForce(Vector2(r.transform.position.x - transform.position.x, r.transform.position.y - transform.position.y).normalized * EXPLOSION_FORCE / Vector2.Distance(r.transform.position, transform.position));
	}
}