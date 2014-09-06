#pragma strict

var buttonRadius : int = 1;
var lerpSpeed : float = 0.3;
var canPause : boolean = false;
var paused : boolean = false;
var pauseSprite : Sprite;
var playSprite : Sprite;

var pauseText : GUIText;
var backToMenuButton : GameObject;

var canPressPause : boolean;
var playerIsDead : boolean;


function Start () {
	pauseText.color.a = 0;
	backToMenuButton.GetComponent(SpriteRenderer).color.a = 0;
	
	transform.position = Camera.main.ScreenToWorldPoint (Vector2 (Screen.width - 150, Screen.height - 75));
	transform.position.z = 0;
	
	backToMenuButton.transform.position = Camera.main.ScreenToWorldPoint (Vector2 (150, Screen.height - 75));
	backToMenuButton.transform.position.z = 0;
	
	canPressPause = true;
	playerIsDead = false;
}

function Update () {
	if (canPause && !playerIsDead) {
		#if (UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE) && !UNITY_EDITOR
			//Load main level
			if (paused && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position), backToMenuButton.transform.position) < buttonRadius) {
				Time.timeScale = 1;
				Application.LoadLevel ("menu");
			}
			
			//If player is not touching the screen
			if (Input.touchCount == 0) {
				GetComponent (SpriteRenderer).color.a = Mathf.MoveTowards (GetComponent (SpriteRenderer).color.a, 1, lerpSpeed);
			}
			//Player is touching the screen
			else {
				//If this is the first frame when the player begins to tap the screen
				if (Input.GetTouch (0).phase != TouchPhase.Began) {
					GetComponent (SpriteRenderer).color.a = Mathf.MoveTowards (GetComponent (SpriteRenderer).color.a, 1, lerpSpeed);
				}
				//This is every frame after the player has first touched the screen
				else {
					if (!paused)
						GetComponent (SpriteRenderer).color.a = Mathf.MoveTowards (GetComponent (SpriteRenderer).color.a, 0, lerpSpeed);
					
					if (GetComponent (SpriteRenderer).color.a > 0.7 && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position), transform.position) < buttonRadius) {
						Pause ();
						pauseDelay ();
					}
				}
			}
		#else
			if (paused && Input.GetButton ("Fire1") && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), backToMenuButton.transform.position) < buttonRadius) {
				Time.timeScale = 1;
				Application.LoadLevel ("menu");
			}
			
			if (!Input.GetButton ("Fire1")) {
				GetComponent (SpriteRenderer).color.a = Mathf.MoveTowards (GetComponent (SpriteRenderer).color.a, 1, lerpSpeed);
				
			}
			else {
				if (!paused)
					GetComponent (SpriteRenderer).color.a = Mathf.MoveTowards (GetComponent (SpriteRenderer).color.a, 0, lerpSpeed);
					
				if (canPressPause && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), transform.position) < buttonRadius) {
					Pause ();
					pauseDelay ();
				}
			}
		#endif
		
		if (Input.GetKeyDown ("escape"))
			Pause ();
	}
}

function Pause () {
	paused = !paused;
	if(paused == true){
		GetComponent (SpriteRenderer).sprite = playSprite;
	}
	else{
		GetComponent (SpriteRenderer).sprite = pauseSprite;
	}
	backToMenuButton.GetComponent(SpriteRenderer).color.a = Mathf.Abs (backToMenuButton.GetComponent(SpriteRenderer).color.a - 1);
	pauseText.color.a = Mathf.Abs (pauseText.color.a - 1);
	Time.timeScale = Mathf.Abs (Time.timeScale - 1);
}

function pauseDelay () {
	canPressPause = false;
	var curTime : float = Time.realtimeSinceStartup;
	while (Time.realtimeSinceStartup - curTime  < 0.25)
		yield;
	canPressPause = true;
}