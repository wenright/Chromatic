﻿function Start () {
	for (var i : int = 0; i < 50; i++) {
		transform.GetComponent(GUITexture).color.a -= 0.03;
		yield WaitForSeconds(0.01);
	}
}