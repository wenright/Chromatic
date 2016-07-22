using UnityEngine;
using System.Collections;

public class Cleanup : MonoBehaviour {

    public float delay;

	void Start () {
        Destroy();
	}

    IEnumerator Destroy () {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
