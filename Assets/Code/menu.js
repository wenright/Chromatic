#pragma strict

var fader : fade_in;

private var buttonRadius : float = 1.0;

function Update () {
	if (Input.touchCount > 0) {
		//Play button
		if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector2(-5, 0)) <= buttonRadius) {
			fader.Fade ();
		}
		//Score button
		else if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector2(-1.5, 0)) <= buttonRadius)
			print("Clicked on the score button");
		//Mute button
		else if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector2(1.5, 0)) <= buttonRadius)
			print ("Clicked on the volume button");
		//Tutorial button
		else if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector2(5, 0)) <= buttonRadius)
			print ("Clicked on the tutorial button");
	}
	else if (Input.GetButtonDown("Fire1")) {
		if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2(-5, 0)) <= buttonRadius) {
			fader.Fade ();
		}
		//Score button
		else if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2(-1.5, 0)) <= buttonRadius)
			print("Clicked on the score button");
		//Mute button
		else if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2(1.5, 0)) <= buttonRadius)
			print ("Clicked on the volume button");
		//Tutorial button
		else if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2(5, 0)) <= buttonRadius)
			print ("Clicked on the tutorial button");
	}
}