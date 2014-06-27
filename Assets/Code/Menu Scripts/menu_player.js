#pragma strict

var done : boolean = false;

function Update () {
	if (!done) {
		if (Input.touchCount > 0) {
			transform.position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		}
		if (Input.GetMouseButton(0)) {
			transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		
		transform.position.z = 0;
	}
}

function OnCollisionEnter2D() {
	done = true;
}