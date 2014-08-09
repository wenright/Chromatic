#pragma strict

var buttonRadius : int = 1;
var lerpSpeed : float = 0.3;
var canPause : boolean = false;

var pauseText : GUIText;

function Start () {
	pauseText.color.a = 0;
}

function Update () {
	if (canPause) {
		#if (UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE) && !UNITY_EDITOR
			if (Input.touchCount == 0 && Time.timeScale != 0) {
				GetComponent (SpriteRenderer).color.a = Mathf.Lerp (GetComponent (SpriteRenderer).color.a, 1, lerpSpeed);
			}
			else {
				GetComponent (SpriteRenderer).color.a = Mathf.Lerp (GetComponent (SpriteRenderer).color.a, 0, lerpSpeed);
					
				if (GetComponent (SpriteRenderer).color.a > 0.7 && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.getTouch(0).position), transform.position) < buttonRadius)
					Pause ();
			}
		#else		
			if (!Input.GetButton ("Fire1")) {
				GetComponent (SpriteRenderer).color.a = Mathf.Lerp (GetComponent (SpriteRenderer).color.a, 1, lerpSpeed);
			}
			else {
				GetComponent (SpriteRenderer).color.a = Mathf.Lerp (GetComponent (SpriteRenderer).color.a, 0, lerpSpeed);
					
				if (GetComponent (SpriteRenderer).color.a > 0.7 && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), transform.position) < buttonRadius)
					Pause ();
			}
		#endif
	}
}

function Pause () {
	print ("Pause button pressed!");
	pauseText.color.a = Mathf.Abs (pauseText.color.a - 1);
	Time.timeScale = Mathf.Abs (Time.timeScale - 1);
}