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
        while (!current.isComplete())
        {
            Vector2 enemyLoc = current.getNext();
            Instantiate(new Enemy(enemyLoc.x, enemyLoc.y));
        }
    }
}
