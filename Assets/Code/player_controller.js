#pragma strict

//Please Add Variables to the proper area!!
//SetUp
var width : float;
var height : float;
//Movement
var MOVE_SPEED : float;
private var move_location : Vector2 = Vector2.zero;
var canMove : boolean = false;//Used to prevent player from moving to button position at beginning of game. Check this box for tutorial, but not main.
var tolerance : float;//tolerance for spawning a friendly
//Explosion variables
var EXPLOSION_RADIUS : int = 7;
var EXPLOSION_FORCE : float = 2000;
//Colors
var color : Color = Color.white;
static var purple :  Color = Color(160/255.0F, 0, 240/255.0F, 1);
static var green : Color = Color(20/255.0F, 220/255.0F, 0, 1);
static var orange : Color = Color(1, 127/255.0F, 0, 1); 
static var red : Color = Color.red;
static var yellow: Color = Color.yellow;
static var blue: Color = Color.blue;				
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
var ADDITIONAL_TIME : int = 100;
var MAX_TIME : int = 300;
//Score
private var multiplier : int = 1;
private var best_multiplier : int = 0;
private var score : int = 0;
private var highscore : int = 0;
private var kills : int = 0;
private var BaseScore : int = 100;
private var combo_score : int = 0;
//Rage
private var rage_mode : boolean = false;
private var rage_timer : int = 0;
//Sounds
var enemy_killed : AudioClip;
var player_hit : AudioClip;
var triangle_picked_up : AudioClip;
//Misc (add random shit to be sorted here)
var dead : boolean = false;
var canDie : boolean = true;

function Start () {
	width = Camera.main.ViewportToWorldPoint(Vector3(1, 1, 10)).x; //Set width to viewport width
	height = Camera.main.ViewportToWorldPoint(Vector3(1, 1, 10)).y; //Set height to viewport height
	highscore = PlayerPrefs.GetInt("HighScore"); //Get the high score
	dead = false;
	kills = 0;
	combo_score = 0;
	trail.time = .1;
}

/*		//Basic score, timer, and high score. Useful for debug.
function OnGUI () {
	//Super basic score counter(WILL BE REPLACED!!!)
	GUI.Label (Rect(0, 0, 120, 60), "High Score: " + highscore);
	GUI.Label (Rect(0, 30, 120, 60), "Score: " + score);
	GUI.Label (Rect(0, 60, 120, 60), "Time: " + timer);
}
*/

function Update () {
	if (!dead) {	
		//Takes in player touches and stores the X and Y coordinates in terms of world coordinates
		if (Input.touchCount > 0 && canMove)
			move_location = Camera.main.ScreenToWorldPoint(Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
		//Takes in mouse input instead
		else if (Input.GetMouseButton(0) && canMove)
			move_location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
		if(Vector2.Distance(move_location, transform.position) < 7) //checks to see if finger/mouse is within range
		transform.position = Vector2.MoveTowards(transform.position, move_location, Time.deltaTime * MOVE_SPEED); //moves towards that location
		
		//Checks if colored and counts down from 200, when 0 is reached, resets to white
		if(timer > 0 && color != Color.white){
		  timer -= Time.deltaTime;
		  transform.localScale = new Vector3(1.75, 1.75, 1);
		   //Pulsing Animation Code-->
		   if(timer <= 100){
		   	if(timer % 50 == 0)
		   		Camera.main.GetComponent(shake_script).LightShake();
		  	if(timer % 20 == 0){
		  		transform.localScale = new Vector3(2, 2, 1);
		  	}
		  	else if(timer % 20 == 19){
		  		transform.localScale = new Vector3(2.3, 2.3, 1);;
		  	}
		  	else if(timer % 20 == 18){
		  		transform.localScale = new Vector3(2.7, 2.7, 1);
		  	}
		  	else if(timer % 20 == 17){
		  		transform.localScale = new Vector3(3.2, 3.2, 1);
		  	}
		  	else if(timer % 20 == 16){
		  		transform.localScale = new Vector3(3.2, 3.2, 1);
		  	}
		  	else if(timer % 20 == 15){
		  		transform.localScale = new Vector3(2.7, 2.7, 1);
		  	}
		  	else if(timer % 20 == 14){
		  		transform.localScale = new Vector3(2.3, 2.3, 1);
		  	}
		  	else if(timer % 20 == 13){
		  		transform.localScale = new Vector3(2, 2, 1);
		  	}
		  	else{
		  		transform.localScale = new Vector3(1.75, 1.75, 1);
		  	}
		  }	
		  //<--
		}
		else {
			//Time has run out.  This is called every frame FYI. Should change that, it could get costly especially with GetComponents
			transform.localScale = new Vector3(1.75, 1.75, 1); //Set size back to normal
			color = Color.white; //change the color variable to white
			sprite.color = Color.Lerp (sprite.color, color, 0.1);
			Camera.main.GetComponent(game_controller).ChangeBackgroundColor(Color.white);
			trail.material.SetColor("_Color", color);
			timer = MAX_TIME; //Reset timer
			canDie = true;
			multiplier = 1; //Multiplier to 1
			if (combo_score > 0) {
				//Instantiate a score text showing how many points were scored during that combo
				var scr_txt : GameObject = Instantiate(ScoreText, transform.position + Vector3(0,1,0), Quaternion.Euler(0, 0, -25));
				scr_txt.GetComponent(TextMesh).text = "+" + combo_score;	//We could have a switch statement here giving dif msgs like "Nice Combo!"
				combo_score = 0;
			}
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
			sprite.color = Color.Lerp (sprite.color, color, 0.9);
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
			line.GetComponent(SpriteRenderer).color = Color.Lerp (line.GetComponent(SpriteRenderer).color, color, 0.1);//set the color to the current color
		}
	}
}

function OnTriggerEnter2D (other : Collider2D) {
	if (!dead) {
		var exp : GameObject; // creates the explosion gameobject
		if (other.tag == "Friendly" && !rage_mode) {
			//audio.PlayOneShot (triangle_picked_up);
			transform.localScale = new Vector3(1.75, 1.75, 1); //makes sure size is set to full
			
			if (combo_score > 0) {
				//Instantiate a score text showing how many points were scored during that combo
				var scr_txt : GameObject = Instantiate(ScoreText, transform.position + Vector3(0,1,0), Quaternion.Euler(0, 0, -25));
				scr_txt.GetComponent(TextMesh).text = "+" + combo_score;	//We could have a switch statement here giving dif msgs like "Nice Combo!"
				combo_score = 0;
			}
			
			////////////////////////////////////////////////
			//TODO: MAKE BETTER!!
			if (color == Color.white)
				color = other.GetComponent(friendly).color; //Sets base color	
			if(color != purple && color != orange && color != green){
				if(color == blue){
					if(other.GetComponent(friendly).color == red)
						color = purple;
					else if(other.GetComponent(friendly).color == yellow)
						color = green;
				}
				else if(color == yellow){
					if(other.GetComponent(friendly).color == red)
						color = orange;
					else if(other.GetComponent(friendly).color == blue)
						color = green;
				}
				else if(color == red){
					if(other.GetComponent(friendly).color == yellow)
						color = orange;
					else if(other.GetComponent(friendly).color == blue)
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
			Camera.main.GetComponent(game_controller).ChangeBackgroundColor(color);
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
			if(temp_color == red)
				f.GetComponent(friendly).SetColor(0);
			else if(temp_color == yellow)
				f.GetComponent(friendly).SetColor(1);
			else if(temp_color == blue)
				f.GetComponent(friendly).SetColor(2);
		}
		else if (other.tag == "Enemy") {
			if (color == other.GetComponent(enemy).color || rage_mode) { //if the ball is the correct color
				Destroy(other.gameObject);
				score += BaseScore * multiplier;		//Change this to w/e, doesn't really matter what it is
				combo_score += BaseScore * multiplier;
				var mul_txt : GameObject = Instantiate(ScoreText, transform.position, transform.rotation);
				mul_txt.GetComponent(TextMesh).text = "X" + multiplier;
				//sc_txt.GetComponent(TextMesh).color = other.GetComponent(enemy).color;		//<- if you want the color to be the same as the object destroyed
				multiplier++;
				if(multiplier > best_multiplier)
					best_multiplier = multiplier; //sets high multiplier
				if (multiplier == 6) {
					rage_mode = true; //turns on rage mode
					if (timer < MAX_TIME)	timer = MAX_TIME;
					rage_timer = timer; //sets timer to remainging time
				}
				if(!rage_mode)
					timer += ADDITIONAL_TIME+ADDITIONAL_TIME/multiplier; //add aditional time per kill not not on ragemode
				exp = Instantiate(explosion, transform.position, transform.rotation); //explode
				exp.GetComponent(ParticleSystem).startColor = color; //particles for explosion
				kills++;
				audio.PlayOneShot(enemy_killed);
				Camera.main.GetComponent(shake_script).LightShake();//shake camera
			}
			else if(color == Color.white && canDie) { //if the ball is white
				
				var go : GameObject = Instantiate (game_over, Vector3(0, -1, -1), transform.rotation); //Game over screen
				go.gameObject.transform.localScale = Vector3.one * 0.7;
				
				var hs : GameObject = Instantiate(game_over, Vector3(0, 0, -1), transform.rotation);
				
				if (score > PlayerPrefs.GetInt("highscore5")) {
					hs.GetComponent(TextMesh).text = "New High Score! " + score;//prints highscore
					
					if (score > PlayerPrefs.GetInt ("highscore1")) {
						PlayerPrefs.SetInt ("highscore5", PlayerPrefs.GetInt ("highscore4"));
						PlayerPrefs.SetInt ("highscore4", PlayerPrefs.GetInt ("highscore3"));
						PlayerPrefs.SetInt ("highscore3", PlayerPrefs.GetInt ("highscore2"));
						PlayerPrefs.SetInt ("highscore2", PlayerPrefs.GetInt ("highscore1"));
						PlayerPrefs.SetInt ("highscore1", score);
					}
					else if (score > PlayerPrefs.GetInt ("highscore2")) {
						PlayerPrefs.SetInt ("highscore5", PlayerPrefs.GetInt ("highscore4"));
						PlayerPrefs.SetInt ("highscore4", PlayerPrefs.GetInt ("highscore3"));
						PlayerPrefs.SetInt ("highscore3", PlayerPrefs.GetInt ("highscore2"));
						PlayerPrefs.SetInt ("highscore2", score);
					}
					else if (score > PlayerPrefs.GetInt ("highscore3")) {
						PlayerPrefs.SetInt ("highscore5", PlayerPrefs.GetInt ("highscore4"));
						PlayerPrefs.SetInt ("highscore4", PlayerPrefs.GetInt ("highscore3"));
						PlayerPrefs.SetInt ("highscore3", score);
					}
					else if (score > PlayerPrefs.GetInt ("highscore4")){
						PlayerPrefs.SetInt ("highscore5", PlayerPrefs.GetInt ("highscore4"));
						PlayerPrefs.SetInt ("highscore4", score);
					}
					else {
						PlayerPrefs.SetInt ("highscore5", PlayerPrefs.GetInt ("highscore4"));
					}
				}
				else {
					hs.GetComponent(TextMesh).text = "Score: " + score; //prints score
				}
				
				GameController.GetComponent(game_controller).GameOver();
				SpawnController.GetComponent(spawner).GameOver();
				dead = true;
				//Destroy(gameObject);//Moving this to the upload function, so we can finish uploading then destroy the object
				UploadScore();
			}
			else {//if the ball has the wrong color
				multiplier = 1; //reset multiplier
				timer = 0; //reset timer
				color = Color.white; //reset color
				Destroy(other.gameObject); //destory other object
				score -= BaseScore; //subtract penalty points
				Camera.main.GetComponent(shake_script).Shake();//shake camera
				//Handheld.Vibrate();	//Kind of annoying...
				audio.PlayOneShot(player_hit);
				explode ();
				canDie = false;
				exp = Instantiate(explosion, transform.position, transform.rotation);//explode
				exp.GetComponent(ParticleSystem).startColor = Color.white;//particles
				
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
	if(transform.position.x - x < tolerance && transform.position.y - y < tolerance)
		return false;		
	return true;
}

//Takes all the rigidbodies in the scene and adds force pushing them away from the player
function explode () {
	var all_rigidbodies = FindObjectsOfType(Rigidbody2D);
	
	for (var r : Rigidbody2D in all_rigidbodies)
		if (Vector2.Distance(r.transform.position, transform.position) < EXPLOSION_RADIUS && r.tag != "Player" && r.tag != "Text")
			r.AddForce(Vector2(r.transform.position.x - transform.position.x, r.transform.position.y - transform.position.y).normalized * EXPLOSION_FORCE / Vector2.Distance(r.transform.position, transform.position));
}

function getMultiplier(){
	return multiplier; //to access this variable but it needs to stay private
}

function playAnimation () {
	animation.Play ();
}

function UploadScore () {
	//Upload score, kills, time, and player name to some server somewhere somehow
	//Use PlayerPrefs.GetString("Name"); to return the name of the player
	//Use yield www to wait for the upload to finish
	
	//Done uploading
	Destroy(gameObject);
}