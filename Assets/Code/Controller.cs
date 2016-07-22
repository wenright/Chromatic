using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	//Colors
	public Color purple = new Color(160/255.0F, 0, 240/255.0F, 1);
	public Color green = new Color(20/255.0F, 220/255.0F, 0, 1);
	public Color orange = new Color(1, 127/255.0F, 0, 1); 
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
