using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {
    bool hidden;
    Color type;
    Controller gc;

    void Awake () {
        gc = GameObject.FindWithTag("GameController").GetComponent<Controller>();
        hidden = false;
        if (this.name == "Red")
            type = ColorList.red;
       else if(this.name == "Blue")
            type = ColorList.blue;
        if (this.name == "Yellow")
            type = ColorList.yellow;
		this.GetComponent<SpriteRenderer> ().color = type;
    }
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), 100f * Time.deltaTime);
    }

    void OnTriggerEnter2D (Collider2D other) {
        // TODO change player color to w/e this is (Or calculate new color based on players color)
        if (other.tag == "Player" && !hidden)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            hidden = true;
            gc.hp = gc.MAX_HP;
            other.gameObject.GetComponent<Player>().SetColor(this.type);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        this.GetComponent<SpriteRenderer>().enabled = true;
        hidden = false;
    }
}
