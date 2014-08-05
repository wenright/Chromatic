#pragma strict

function Start () {

}

function Update () {
#if (UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE) && !UNITY_EDITOR
	if (Input.touchCount == 0)
		Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 0.01);
	else
		Time.timeScale = Mathf.Lerp(Time.timeScale, 1, 0.01);
#else		
	if (!Input.GetButton("Fire1"))
		Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 0.01);
	else
		Time.timeScale = Mathf.Lerp(Time.timeScale, 1, 0.01);
#endif
}