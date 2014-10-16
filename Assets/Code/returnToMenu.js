//returns the player to the menu when the button this script is attached to is pressed.
#pragma strict

function Update () {
	#if (UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE) && !UNITY_EDITOR
		if (Input.touchCount > 0)
			if (Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position), transform.position) < 1) 
				loadMainMenu ();
	#else
		if (Input.GetButtonDown ("Fire1") && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), transform.position) < 1)
			loadMainMenu ();
	#endif
}

function loadMainMenu () {
	//TODO Have text fade in/out on begin and exit
	Application.LoadLevel ("menu");
}