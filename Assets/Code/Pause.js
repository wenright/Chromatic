#pragma strict

var buttonRadius : int = 1;
var lerpSpeed : float = 0.3;
var canPause : boolean = false;
var paused : boolean = false;
var pauseSprite : Sprite;
var playSprite : Sprite;

var pauseText : GUIText;
var backToMenuButton : GameObject;


function Start () {
	pauseText.color.a = 0;
	backToMenuButton.GetComponent(SpriteRenderer).color.a = 0;
}

function Update () {
	if (canPause) {
		#if (UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE) && !UNITY_EDITOR		
			if (Input.touchCount == 0 && !paused) {
				GetComponent (SpriteRenderer).color.a = Mathf.Lerp (GetComponent (SpriteRenderer).color.a, 1, lerpSpeed);
			}
			else {
				GetComponent (SpriteRenderer).color.a = Mathf.Lerp (GetComponent (SpriteRenderer).color.a, 0, lerpSpeed);
					
				if (GetComponent (SpriteRenderer).color.a > 0.7 && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position), transform.position) < buttonRadius)
					Pause ();
					
				if (paused && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position), backToMenuButton.transform.position) < buttonRadius) {
					Application.LoadLevel ("main");
				}
			}
		#else
			if (paused && Input.GetButton ("Fire1") && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), backToMenuButton.transform.position) < buttonRadius) {
				Application.LoadLevel ("main");
			}
		
			if (!Input.GetButton ("Fire1")) {
				GetComponent (SpriteRenderer).color.a = Mathf.Lerp (GetComponent (SpriteRenderer).color.a, 1, lerpSpeed);
			}
			else {
				GetComponent (SpriteRenderer).color.a = Mathf.Lerp (GetComponent (SpriteRenderer).color.a, 0, lerpSpeed);
					
				if (GetComponent (SpriteRenderer).color.a > 0.7 && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), transform.position) < buttonRadius)
					Pause ();
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