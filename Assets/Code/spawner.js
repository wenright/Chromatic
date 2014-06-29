var friend : GameObject;
var en : GameObject;
var over : boolean = false;
var wait_time : float = 2.0;
private var prev_color : int = -1;

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
	
	//Basic enemy spawner, spawns one every x seconds
	while (!over) {
		//for (var j : int = 0; j < 5; j++) {		//uncommment this for a burst kind of spawn, its interesting but is kind of frustrating.
			var n : int = 1;
			if (Random.value > 0.5)
				n = -1;
		
			//Test location coordinates to see if an object is near, and if so, spawn a different one
			var e : GameObject;
			if (Random.value > 0.5)
				e = Instantiate(en, Camera.main.ViewportToWorldPoint(Vector2(Random.value * n, 1.1)), transform.rotation);
			else
				e = Instantiate(en, Camera.main.ViewportToWorldPoint(Vector2(1.1, Random.value * n)), transform.rotation);
			var temp_color : int = -1;
			if (Random.value < 0.75 || prev_color == -1) {
				temp_color = Random.Range(1, 4);
				e.GetComponent(enemy).SetColor(temp_color);
			}
			else {
				temp_color = prev_color;
				e.GetComponent(enemy).SetColor(prev_color);
			}
			
			prev_color = temp_color;
		//}
		
		yield WaitForSeconds(wait_time);
		if (wait_time > 0.01)
			wait_time -= 0.01;
	}
}

function GameOver () {
	over = true;
}