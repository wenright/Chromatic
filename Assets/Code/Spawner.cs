using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject enemy;
    public GameObject fixedenemy;
	public float timer;
	int i = 10;
	SpawnPattern current;
	public int enemycount;
	int scheme;
	// Use this for initialization
	void Awake () {
		scheme = Random.Range(1, 4);
		current = new SpawnPattern (i, scheme);
		timer = 0;
	}

	// Update is called once per frame
	void Update () {
        //TODO: Add fixedenemy code, fix this logic.

    
		//Do we have a valid spawn pattern?
		if (current.isComplete ()) {
			//are we supposed to wait?
			if (!current.isWaiting ()) {
				i++;
				//if not get a new one
				current = new SpawnPattern (i, scheme);
			} else if (enemycount == 0){
				//if yes, than wait for all enemies to be dead.
				i++;
				current = new SpawnPattern (i, scheme);
			}
		}
        GameObject.FindWithTag("GameController").GetComponent<Controller>().level = i;
        //out of time
        if (timer <= 0 && !current.isComplete()) {
			//spawn the enemy
			SpawnCommand enemyInfo = current.getNext();
			enemycount++;
            GameObject lastEnemy;
            if (enemyInfo.isFixed())
			    lastEnemy = Instantiate (fixedenemy, enemyInfo.GetLocation(), new Quaternion()) as GameObject;
            else
                lastEnemy = Instantiate(enemy, enemyInfo.GetLocation(), new Quaternion()) as GameObject;
            lastEnemy.GetComponent<Enemy>().SetColor(enemyInfo.GetColor());
            timer = enemyInfo.GetDelay();
		}


		//tick time down
		timer -= Time.deltaTime;
	}
}

