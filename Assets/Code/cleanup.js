var wait_time : int = 1;
var shrink : AnimationClip;
var animate : boolean = false;

function Start () {
	yield WaitForSeconds(wait_time);
	if (animate && shrink) {
		animation.clip = shrink;
		animation.Play();
		yield WaitForSeconds(0.1);
	}
	Destroy (gameObject);
}