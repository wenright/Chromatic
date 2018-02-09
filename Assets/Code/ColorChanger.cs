using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ColorChanger : MonoBehaviour {

    private Controller gc;
    private Color color;
    private bool collidable = true;

    void Awake () {
        gc = GameObject.FindWithTag("GameController").GetComponent<Controller>();

        if (this.name == "Red") {
            color = ColorList.red;
        }
        if(this.name == "Blue") {
            color = ColorList.blue;
        }
        if (this.name == "Yellow") {
            color = ColorList.yellow;
        }

		this.GetComponent<SpriteRenderer> ().color = color;
    }
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), 100f * Time.deltaTime);
    }

    void OnTriggerEnter2D (Collider2D other) {
        // Change player color to w/e this is (Or calculate new color based on players color)
        if (other.tag == "Player" && collidable) {
            AudioSource source = GetComponent<AudioSource>();
            source.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            collidable = false;

            gc.hp = gc.MAX_HP;
            other.gameObject.GetComponent<Player>().AddColor(this.color);
        }
    }

    public void Respawn () {
        collidable = true;

        GetComponent<SpriteRenderer>().enabled = true;

        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.25f)
            .SetEase(Ease.InQuad);
    }
}
