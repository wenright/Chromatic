using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Spawn(int i)    {
        SpawnPattern current = new SpawnPattern(i);
    }
}
