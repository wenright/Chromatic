//returns the player to the menu when the button this script is attached to is pressed.

#pragma strict

function Update () {
	#if (UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE) && !UNITY_EDITOR
		if (Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.getTouch (0).position), transform.position) < 1) 
			Application.LoadLevel ("main");
	#else
		if (Input.GetButtonDown ("Fire1") && Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), transform.position) < 1)
			Application.LoadLevel ("main");
	#endif
}