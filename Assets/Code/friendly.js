//The color of the enemy, starts out either red, yellow, or blue
var color : Color = Color.white;var friend : GameObject;

//The speed at which the object rotates around its center
var ROTATE_SPEED : int = 50;

//The sprite renderer component of this object
var sprite : SpriteRenderer;

function Update () {
	transform.RotateAround(transform.position, transform.forward, ROTATE_SPEED * Time.deltaTime);
}

function SetColor (num : int) {
print(num);
	switch (num) {
		case 0: color = Color.red;
			break;
		case 1: color = Color.yellow;
			break;
		case 2: color = Color.blue;
			break;
		default: break;
	}
	
	sprite.color = color;
}