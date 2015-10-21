var wait_time : int = 1;
var shrink : AnimationClip;
var animate : boolean = false;

function Start () {
	yield WaitForSeconds(wait_time);
	if (animate && shrink) {
		GetComponent.<Animation>().clip = shrink;
		GetComponent.<Animation>().Play();
		yield WaitForSeconds(0.1);
	}
	Destroy (gameObject);
}