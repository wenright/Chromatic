//The color of the enemy, starts out either red, yellow, or blue
var color : Color = Color.white;var friend : GameObject; 

var width : float;
var height : float;

var dirx : int = 1;
var diry : int = 1;

//The speed at which the object rotates around its center
var ROTATE_SPEED : int = 50;

//The sprite renderer component of this object
var sprite : SpriteRenderer;

function Start () {
	width = Camera.main.ViewportToWorldPoint(Vector3(1, 1, 10)).x;
	height = Camera.main.ViewportToWorldPoint(Vector3(1, 1, 10)).y;
	
	if (Mathf.Round(Random.value) == 1)
		dirx = -1;
	if (Mathf.Round(Random.value) == 1)
		diry = -1;
}

function Update () {
	transform.RotateAround(transform.position, transform.forward, ROTATE_SPEED * Time.deltaTime);
	
	if (transform.position.x > width) {
		dirx *= -1;
		transform.position.x = width;
	}
	else if (transform.position.x < -width) {
		dirx *= -1;
		transform.position.x = -width;
	}
	
	if (transform.position.y > height) {
		diry *= -1;
		transform.position.y = height;
	}
	else if (transform.position.y < -height) {
		diry *= -1;
		transform.position.y = -height;
	}
	
	transform.position.x += Time.deltaTime * dirx;
	transform.position.y += Time.deltaTime * diry;
}

function SetColor (num : int) {
	switch (num) {
		case 0: color = player_controller.red;
			break;
		case 1: color = player_controller.yellow;
			break;
		case 2: color = player_controller.blue;
			break;
		default: break;
	}
	
	sprite.color = color;
}