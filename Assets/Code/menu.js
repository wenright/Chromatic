#pragma strict

var fader : fade_in;

var playButton : GameObject;
var scoreButton : GameObject;
var muteButton : GameObject;
var tutorialButton : GameObject;

var altMuteButton : Sprite;
var regularSoundButton : Sprite;

private var buttonRadius : float = 1.0;
private var canPress : boolean;

function Start () {
	canPress = true;
	
	if (PlayerPrefs.GetInt("Volume") == 0) {
		muteButton.GetComponent (SpriteRenderer).sprite = altMuteButton;
		camera.main.GetComponent(AudioListener).volume = 0;
	}
	else {
		camera.main.GetComponent(AudioListener).volume = 1;
		muteButton.GetComponent (SpriteRenderer).sprite = regularSoundButton;
	}
}

function Update () {
	if (canPress) {
		if (Input.touchCount > 0) {
			//Play button
			if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.touches[0].position), playButton.transform.position) <= buttonRadius) {
				canPress = false;
				fader.Fade ();
			}
			//Score button
			else if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.touches[0].position), scoreButton.transform.position) <= buttonRadius) {
				print("Clicked on the score button");
				Application.LoadLevel ("highscores");
			}
			//Mute button
			else if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.touches[0].position), muteButton.transform.position) <= buttonRadius) {
				print ("Clicked on the volume button");
				if (camera.main.GetComponent(AudioListener).volume == 1) {
					camera.main.GetComponent(AudioListener).volume = 0;
					PlayerPrefs.SetInt("Volume", 0);	//We could make a volume slider, but I dont think its necessary. Maybe if we have sound and music going at the same time.
					muteButton.GetComponent (SpriteRenderer).sprite = altMuteButton;
				}
				else {
					camera.main.GetComponent(AudioListener).volume = 1;
					PlayerPrefs.SetInt("Volume", 1);
					muteButton.GetComponent (SpriteRenderer).sprite = regularSoundButton;
				}
				
			}
			//Tutorial button
			else if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.touches[0].position), tutorialButton.transform.position) <= buttonRadius) {
				print ("Clicked on the tutorial button");
				Application.LoadLevel("tutorial");
			}
		}
		else if (Input.GetButtonDown("Fire1")) {
			if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.mousePosition), playButton.transform.position) <= buttonRadius) {
				canPress = false;
				fader.Fade ();
			}
			//Score button
			else if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.mousePosition), scoreButton.transform.position) <= buttonRadius) {
				print("Clicked on the score button");
				Application.LoadLevel ("highscores");
			}
			//Mute button
			else if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.mousePosition), muteButton.transform.position) <= buttonRadius) {
				print ("Clicked on the volume button");
				if (camera.main.GetComponent(AudioListener).volume == 1) {
					camera.main.GetComponent(AudioListener).volume = 0;
					PlayerPrefs.SetInt("Volume", 0);	//We could make a volume slider, but I dont think its necessary. Maybe if we have sound and music going at the same time.
					muteButton.GetComponent (SpriteRenderer).sprite = altMuteButton;
				}
				else {
					camera.main.GetComponent(AudioListener).volume = 1;
					PlayerPrefs.SetInt("Volume", 1);
					muteButton.GetComponent (SpriteRenderer).sprite = regularSoundButton;
				}
			}
			//Tutorial button
			else if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.mousePosition), tutorialButton.transform.position) <= buttonRadius) {
				print ("Clicked on the tutorial button");
				Application.LoadLevel("Tutorial");
			}
		}
	}
}