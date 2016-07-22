using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

	//Singleton
	private static Controller gcsingleton;
	public static Controller Instance { get { return gcsingleton; } }

    // UI controls
    public Slider healthSlider; public float hp; public readonly float  MAX_HP = 100;
    public Image Fill;  // assign in the editor the "Fill"
    private Player player;

    void Awake () {
		if (gcsingleton == null)
			gcsingleton = this;
		else
			Destroy (gameObject);
		DontDestroyOnLoad (this.gameObject);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
	    healthSlider.value = hp/MAX_HP;
        Fill.color = player.GetColor();
        if(hp > 0)
         hp--;
	}
}
