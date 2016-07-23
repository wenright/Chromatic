using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    
    public new GameObject particleSystem;

    public Color type = ColorList.white;
    public Color oldtype;

    Controller gc;
    private int moveSpeed = 15;

    private TrailRenderer trail;
    private SpriteRenderer sprite;

    void Awake () {
        trail = GetComponent<TrailRenderer>();
        sprite = GetComponent<SpriteRenderer>();
        type = ColorList.white;
        gc = GameObject.FindWithTag("GameController").GetComponent<Controller>();
    }

	void Update () {
        if (!type.Equals(oldtype))
        {
            if ((type.Equals(ColorList.blue) && oldtype.Equals(ColorList.yellow)) || (oldtype.Equals(ColorList.blue) && type.Equals(ColorList.yellow)))
                type = ColorList.green;
            else if ((type.Equals(ColorList.red) && oldtype.Equals(ColorList.yellow)) || (oldtype.Equals(ColorList.red) && type.Equals(ColorList.yellow)))
                type = ColorList.orange;
            else if ((type.Equals(ColorList.blue) && oldtype.Equals(ColorList.red)) || (oldtype.Equals(ColorList.blue) && type.Equals(ColorList.red)))
                type = ColorList.purple;
        }
        if(gc.hp == 0)
        {
            type = ColorList.white;
        }
        SetColor(type);
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
        this.oldtype = type;
        this.type = color;

        trail.material.SetColor("_Color", color);
        sprite.color = color;
    }

    public Color GetColor () {
        return type;
    }

    public void Kill () {
        Instantiate(particleSystem, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
