using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    Transform target;
	// Use this for initialization
    public Enemy(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (target)
        {
            //TODO: FIX THIS HORRIBLE MESS
            Quaternion newRotation = Quaternion.LookRotation(transform.position - target.transform.position, Vector3.forward);
            newRotation.x = 0.0f;
            newRotation.y = 0.0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 500*Time.deltaTime);

            GetComponent< Rigidbody2D >().AddForce(transform.up * Time.deltaTime * 300);
        }
    }
}
