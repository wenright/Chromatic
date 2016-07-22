using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    Transform target;
	public Color type;
	Controller gc;
	// Use this for initialization
	void Awake () {
		gc = GameObject.FindWithTag("GameController").GetComponent<Controller>();
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
		if(!type.Equals(this.GetComponent<SpriteRenderer>().color)){
			this.GetComponent<SpriteRenderer> ().color = type;
		}
    }

	public void setColor(string color){
		if (color.Equals ("orange"))
			type = gc.orange;
		else if (color.Equals ("green"))
			type = gc.green;
		else if (color.Equals ("purple"))
			type = gc.purple;
	}

	public void setColor(float color){
		if (color == 1)
			type = gc.orange;
		else if (color == 0)
			type = gc.green;
		else if (color == 2)
			type = gc.purple;
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            // TODO: Spawn some particle effects, display 'game over'
            Destroy(other.gameObject);
        }
    }
}
