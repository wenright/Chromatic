var friend : GameObject;
var enemy : GameObject;

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
	
	//Basic enemy spawner, spawns one every 2 seconds
	while (true) {
		yield WaitForSeconds(2);
		
		var px2 : float = Random.Range(-width, width);
		
		Instantiate(enemy, Camera.main.ViewportToWorldPoint(Vector2(Random.value, 1.0)), transform.rotation);
		
		yield WaitForSeconds(2);
		
		Instantiate(enemy, Camera.main.ViewportToWorldPoint(Vector2(1.0, Random.value)), transform.rotation);
	}
}