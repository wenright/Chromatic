using UnityEngine;
using System.Collections;

public class ColorSpawner : MonoBehaviour {
    private int moveSpeed = 10;
    public GameObject player;
    // Use this for initialization
    void Awake()
    {
         player = GameObject.FindWithTag("Player");
    }
    void Start () {
 
       
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
    }
}
