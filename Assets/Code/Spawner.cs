using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject enemy;
	public float timer;
	int i = 1;
	SpawnPattern current;
	// Use this for initialization
	void Awake () {
		current = new SpawnPattern (i);
		timer = 0;
	}

	// Update is called once per frame
	void Update () {
		//Do we have a valid spawn pattern?
		if (current.isComplete ()) {
			i++;
			//if not get a new one
			current = new SpawnPattern (i);
		}

		//out of time
		if (timer <= 0) {
			//spawn the enemy
			SpawnCommand enemyInfo = current.getNext();

			GameObject lastEnemy = Instantiate (enemy, enemyInfo.GetLocation(), new Quaternion()) as GameObject;
			lastEnemy.GetComponent<Enemy>().SetColor(enemyInfo.GetColor());
            timer = enemyInfo.GetDelay();
		}

		//tick time down
		timer -= Time.deltaTime;
	}
}

