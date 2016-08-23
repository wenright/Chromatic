﻿using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {
    bool collidable;
    Color color;
    Controller gc;

    void Awake () {
        gc = GameObject.FindWithTag("GameController").GetComponent<Controller>();
        collidable = false;

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
        if (other.tag == "Player" && !collidable) {
            GetComponent<SpriteRenderer>().enabled = false;
            collidable = true;

            gc.hp = gc.MAX_HP;
            other.gameObject.GetComponent<Player>().AddColor(this.color);
        }
    }

    public void CollideParticle () {
        if (!GetComponent<Animation>().isPlaying) {
            transform.localScale = Vector3.zero;
            GetComponent<Animation>().Play();
            GetComponent<SpriteRenderer>().enabled = true;
            collidable = false;
        }
    }
}
