﻿//The speed at which the player moves towards the move location, should be between 0 and 1
var MOVE_SPEED : float;		//A higher number means faster movement
var width : float;
var height : float;
//The last location that the player touches
private var move_location : Vector2 = Vector2.zero;
var friend : GameObject;
var line : GameObject;
var MAX_TIME : int = 200;
//The color of the player, starts out white
var color : Color = Color.white;
var purple :  Color = Color(191/255.0F, 0, 1, 1);
var green : Color = Color(0, 1, 0, 1);
var orange : Color = Color(1, 127/255.0F, 0, 1);
var GameController : GameObject;
var SpawnController : GameObject;

private var multiplier : int = 1;
var BaseScore : int = 1000;
var ScoreText : GameObject;

var tolerance : float; 				//tolerance for spawning a friendly

private var score : int = 0;
private var rage_mode : boolean = false;
private var rage_timer : int = 0;

var ADDITIONAL_TIME : int = 75;		//Make this zero if you don't like it

var explosion : GameObject;

//The sprite renderer component of this object
var sprite : SpriteRenderer;
var game_over : GameObject;

private var timer : int = 0;

private var highscore : int = 0;


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
	if(Vector2.Distance(move_location, transform.position) < 7)
		transform.position = Vector2.MoveTowards(transform.position, move_location, Time.deltaTime * MOVE_SPEED);		//TODO: make the speed constant
	
	//Checks if colored and counts down from 200, when 0 is reached, resets to white
	if(timer > 0 && color != Color.white){
	  timer -= Time.deltaTime;
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
	}
	else{
		transform.localScale = new Vector3(1.75, 1.75, 1);
		sprite.color = Color.white;
	  	color = Color.white;
	  	timer = MAX_TIME;
	  	multiplier = 1;
	}

	if (rage_timer > 0) {
		rage_timer -= Time.deltaTime;
		var ran : int = Random.Range(1, 3);
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
	}
	else
		rage_mode = false;
	
	if (color == Color.white) {
		line.transform.localScale.x = 0;
	}
	else {
		line.transform.localScale.x = timer/30.0;
		line.GetComponent(SpriteRenderer).color = color;
	}
}

function OnTriggerEnter2D (other : Collider2D) {
	var exp : GameObject;
	transform.localScale = new Vector3(1.75, 1.75, 1);
	if (other.tag == "Friendly" && !rage_mode) {
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
			}
			else if(color == Color.yellow){
				if(other.GetComponent(friendly).color == Color.red){
					color = orange;
				}
				else if(other.GetComponent(friendly).color == Color.blue){
					color = green;
				}
			}
			else if(color == Color.red){
				if(other.GetComponent(friendly).color == Color.yellow){
					color = orange;
				}
				else if(other.GetComponent(friendly).color == Color.blue){
					color = purple;
				}
			}
			
			timer = MAX_TIME;
		}
		else {
			timer = MAX_TIME; 
			color = other.GetComponent(friendly).color;
		}
		var counter: int = 0;
		multiplier = 1;
		//Spawn a friendly
		do {					//Worst case scenario, this loop could go on forever.  May want to add a counter and have this loop give up once it reaches a certain number
			var px : float = Random.Range(-width, width);
			var py : float = Random.Range(-height, height);
			counter ++;
			if(counter > 300)
				break;
		} while (!CheckPosition(px, py));
		
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
		sprite.color = color;
	}
	else if (other.tag == "Enemy") {
		if (color == other.GetComponent(enemy).color || rage_mode) {
			Destroy(other.gameObject);
			score += BaseScore * multiplier;		//Change this to w/e, doesn't really matter what it is
			var sc_txt : GameObject = Instantiate(ScoreText, transform.position, transform.rotation);
			sc_txt.GetComponent(TextMesh).text = "" + (BaseScore * multiplier);
			//sc_txt.GetComponent(TextMesh).color = other.GetComponent(enemy).color;		//<- if you want the color to be the same as the object destroyed
			multiplier++;
			if (multiplier == 5) {
				rage_mode = true;
				rage_timer = timer;
			}
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
			GameController.GetComponent(game_controller).GameOver();
			SpawnController.GetComponent(spawner).GameOver();
			Destroy (gameObject);
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