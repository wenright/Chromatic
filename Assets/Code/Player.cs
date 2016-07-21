using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private int moveSpeed = 20;

	void Start () {
	
	}
	
	void Update () {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0)) {
            Vector2 moveLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector2.MoveTowards(transform.position, moveLocation, Time.deltaTime * moveSpeed);
        }
#else
    
#endif
    }
}
