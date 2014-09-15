#pragma strict

#pragma strict
var sound : AudioClip;
var fader : fade_in;
var playButton : GameObject;
var scoreButton : GameObject;
var muteButton : GameObject;
var tutorialButton : GameObject;
var splash : GameObject;
var restart: boolean = false;
var button_audio: button_sound;
var altMuteButton : Sprite;
var regularSoundButton : Sprite;
private var buttonRadius : float = 1.0;
private var canPress : boolean;
private var canPressMute : boolean;

function Start () {
	canPress = true;
	canPressMute = true;
	
	camera.main.GetComponent(AudioListener).volume = 1;
		muteButton.GetComponent (SpriteRenderer).sprite = regularSoundButton;
//	if (PlayerPrefs.GetInt("Volume") == 0) {
//		muteButton.GetComponent (SpriteRenderer).sprite = altMuteButton;
//		camera.main.GetComponent(AudioListener).volume = 0;
//	}
}

function Update () {
	#if (UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE) && !UNITY_EDITOR
		if (Input.touchCount > 0) {
			//Play button
			if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.touches[0].position), playButton.transform.position) <= buttonRadius) {
				canPress = false;
				loadGame();
				}
			//Score button
			else if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.touches[0].position), scoreButton.transform.position) <= buttonRadius) {
				print("Clicked on the score button");
				Application.LoadLevel ("highscores");
			}
			//Mute button
			else if (canPressMute && Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.touches[0].position), muteButton.transform.position) <= buttonRadius) {
				print ("Clicked on the volume button");
				audio.PlayOneShot (sound);
				buttonDelay ();
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
	#else
		if (Input.GetButtonDown("Fire1")) {
			if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.mousePosition), playButton.transform.position) <= buttonRadius) {
			canPress = false;
			loadGame();
			}
			//Score button
			else if (Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.mousePosition), scoreButton.transform.position) <= buttonRadius) {
				print("Clicked on the score button");
				audio.PlayOneShot (sound);
				Application.LoadLevel ("highscores");
			}
			//Mute button
			else if (canPressMute && Vector2.Distance (Camera.main.ScreenToWorldPoint(Input.mousePosition), muteButton.transform.position) <= buttonRadius) {
				print ("Clicked on the volume button");
				audio.PlayOneShot (sound);
				buttonDelay ();
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
				audio.PlayOneShot (sound);
				Application.LoadLevel("Tutorial");
			}
		}
	#endif
}
function loadGame(){
	button_audio.Play();
	Application.LoadLevel ("main");
}
function buttonDelay () {
	canPressMute = false;
	while (Input.touchCount > 0)
		yield WaitForSeconds (0.01);
	canPressMute = true;
}