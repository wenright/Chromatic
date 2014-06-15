var friend : GameObject;
var enemy : GameObject;
var over : boolean = false;

function Start () {
	var width : float = Camera.main.ViewportToWorldPoint(Vector3(1, 1, 10)).x;
	var height : float = Camera.main.ViewportToWorldPoint(Vector3(1, 1, 10)).y;

	for (var i = 0; i < 3; i++) {
		do {
			var px : float = Random.Range(-width, width);
			var py : float = Random.Range(-height, height);
		} while (px < ((width / 2) + 2) && px > ((width / 2) - 2) && py < ((height / 2) + 2) && py > ((height / 2) - 2));
		
		var f : GameObject = Instantiate(friend, Vector2(px, py), transform.rotation);
		f.GetComponent(friendly).SetColor(i);
	}
	yield WaitForSeconds(2);
	
	//Basic enemy spawner, spawns one every 2 seconds
	while (!over) {
		var n : int = 1;
		if (Random.value > 0.5)
			n = -1;
	
		//Test location coordinates to see if an object is near, and if so, spawn a different one
	
		if (Random.value > 0.5)
			Instantiate(enemy, Camera.main.ViewportToWorldPoint(Vector2(Random.value * n, 1.1)), transform.rotation);
		else
			Instantiate(enemy, Camera.main.ViewportToWorldPoint(Vector2(1.1, Random.value * n)), transform.rotation);
		yield WaitForSeconds(2);
		
	}
}

function GameOver () {
	over = true;
}