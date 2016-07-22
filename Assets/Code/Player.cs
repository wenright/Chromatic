using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

<<<<<<< HEAD
    public GameObject particleSystem;

    private Color color = ColorList.white;

    public Color type;
    public Color oldtype;

    private int moveSpeed = 15;

    private TrailRenderer trail;
    private SpriteRenderer sprite;

    void Start () {
        trail = GetComponent<TrailRenderer>();
        sprite = GetComponent<SpriteRenderer>();
    }

	void Update () {
        oldtype = this.GetComponent<SpriteRenderer>().color;
        if (!type.Equals(oldtype))
        {
            if ((type.Equals(ColorList.blue) && oldtype.Equals(ColorList.yellow)) || (oldtype.Equals(ColorList.blue) && type.Equals(ColorList.yellow)))
                type = ColorList.green;
            else if ((type.Equals(ColorList.red) && oldtype.Equals(ColorList.yellow)) || (oldtype.Equals(ColorList.red) && type.Equals(ColorList.yellow)))
                type = ColorList.orange;
            else if ((type.Equals(ColorList.blue) && oldtype.Equals(ColorList.red)) || (oldtype.Equals(ColorList.blue) && type.Equals(ColorList.red)))
                type = ColorList.purple;
            this.GetComponent<SpriteRenderer>().color = type;
        }
        transform.position = Vector2.MoveTowards(transform.position, GetMovement(), Time.deltaTime * moveSpeed);
    }

    private Vector2 GetMovement () {
        #if UNITY_EDITOR
            if (Input.GetMouseButton(0)) {
                return Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        #else
            if (Input.touchCount > 0) {
                return Camera.main.ScreenToWorldPoint(Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
            }
        #endif
        
        return transform.position;
    }

    public void SetColor (Color color) {
        this.color = color;

        trail.material.SetColor("_SetColor", color);
        sprite.color = color;
    }

    public void Kill () {
        Instantiate(particleSystem, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
