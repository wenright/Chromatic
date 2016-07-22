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
		if (timer <= 0 && !current.isComplete()) {
			//spawn the enemy
			Vector4 enemyInfo = current.getNext ();
			Vector3 enemyLoc = new Vector3 (enemyInfo.x, enemyInfo.y, 0f);
			float waitTime = enemyInfo.z;
			print (enemyLoc.x.ToString () + " " + enemyLoc.y.ToString ());
			GameObject lastEnemy = Instantiate (enemy, enemyLoc, new Quaternion ()) as GameObject;
			lastEnemy.GetComponent<Enemy>().setColor(enemyInfo.w);
			timer = waitTime;
		}
		//tick time down
		timer -= Time.deltaTime;
	}
}

