using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), 150f * Time.deltaTime);
    }

    void OnTriggerEnter2D () {
        // TODO change player color to w/e this is (Or calculate new color based on players color)
    }
}
