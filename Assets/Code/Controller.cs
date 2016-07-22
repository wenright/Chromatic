using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	//Singleton
	private static Controller gcsingleton;
	public static Controller Instance { get { return gcsingleton; } }

	void Awake () {
		if (gcsingleton == null)
			gcsingleton = this;
		else
			Destroy (gameObject);
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
