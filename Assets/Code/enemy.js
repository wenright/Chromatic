//The speed at which the enemy moves towards the move location, should be between 0 and 1
var MOVE_SPEED : float = .03;		//A higher number means faster movement

//The speed at which the object rotates around its center
var ROTATE_SPEED : int = 50;

//The color of the enemy, starts out either orange, purple, or green
var color : Color = Color.white;
var purple : Color = Color(191/255.0F, 0, 1, 1);
var green : Color = Color(0, 1, 0, 1);
var orange : Color = Color(1, 127/255.0F, 0, 1);

//The target that the enemy chases, typically the player, but could be anything
private var target : Transform;

//The sprite renderer component of this object
var sprite : SpriteRenderer;

function Start () {
	//Finds the object tagged player
	target = GameObject.FindGameObjectWithTag("Player").transform;		//TODO: getting error messages from player being dead and enemies spawning.

	//For now, this just makes it a random color
	var num : int = Random.Range(1, 4);
	
	switch (num) {
		case 1: color = orange;
			break;
		case 2: color = purple;
			break;
		case 3: color = green;
			break;
		default: break;
	}
	
	sprite.color = color;
}

function Update () {
	transform.RotateAround(transform.position, transform.forward, ROTATE_SPEED * Time.deltaTime);
	
	MOVE_SPEED += .0001;
	//Prevents annoying 2s lag and error messages
	if (target)
		transform.position = Vector2.MoveTowards(transform.position, target.transform.position, MOVE_SPEED);
}