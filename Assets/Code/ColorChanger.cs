using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {
    bool hidden;
    Color type;
	// Use this for initialization
	void Awake () {
        type = this.GetComponent<SpriteRenderer>().color;
        hidden = false;
        if (this.name == "Red")
            type = ColorList.red;
       else if(this.name == "Blue")
            type = ColorList.blue;
        if (this.name == "Yellow")
            type = ColorList.yellow;
    }
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), 150f * Time.deltaTime);
    }

    void OnTriggerEnter2D (Collider2D other) {
        // TODO change player color to w/e this is (Or calculate new color based on players color)
        if (other.tag == "Player" && !hidden)
        {
            other.gameObject.GetComponent<Player>().type = this.type;
        }
    }
}
