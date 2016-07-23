using UnityEngine;
using System.Collections;

public class Cleanup : MonoBehaviour {

    public float delay;

	void Awake () {
        Destroy(gameObject, delay);
    }
}
