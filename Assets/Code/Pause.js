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


function Start () {
	pauseText.color.a = 0;
	backToMenuButton.GetComponent(SpriteRenderer).color.a = 0;
	
	canPressPause = true;
}

function Update () {
	if (canPause) {
		#if (UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE) && !UNITY_EDITOR
			if (paused && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position), backToMenuButton.transform.position) < buttonRadius) {
				Time.timeScale = 1;
				Application.LoadLevel ("main");
			}
			if (Input.touchCount == 0) {
				GetComponent (SpriteRenderer).color.a = Mathf.MoveTowards (GetComponent (SpriteRenderer).color.a, 1, lerpSpeed);
			}
			else {
				if (Input.GetTouch (0).phase != TouchPhase.began) {
					GetComponent (SpriteRenderer).color.a = Mathf.MoveTowards (GetComponent (SpriteRenderer).color.a, 1, lerpSpeed);
				}
				else {
					GetComponent (SpriteRenderer).color.a = Mathf.MoveTowards (GetComponent (SpriteRenderer).color.a, 0, lerpSpeed);
					
					if (GetComponent (SpriteRenderer).color.a > 0.7 && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position), transform.position) < buttonRadius)
						Pause ();
				}
			}
		#else
			if (paused && Input.GetButton ("Fire1") && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), backToMenuButton.transform.position) < buttonRadius) {
				Time.timeScale = 1;
				Application.LoadLevel ("main");
			}
			
			if (!Input.GetButtonDown ("Fire1")) {
				GetComponent (SpriteRenderer).color.a = Mathf.MoveTowards (GetComponent (SpriteRenderer).color.a, 1, lerpSpeed);
			}
			else {
				GetComponent (SpriteRenderer).color.a = Mathf.MoveTowards (GetComponent (SpriteRenderer).color.a, 0, lerpSpeed);
					
				if (canPressPause && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), transform.position) < buttonRadius) {
					Pause ();
				}
			}
		#endif
		
		if (Input.GetKeyDown ("escape"))
			Pause ();
	}
}

function Pause () {
	print ("Pause button pressed!");
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