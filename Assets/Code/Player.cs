using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private int moveSpeed = 25;
    public Color type;
    public Color oldtype;

    void Awake()
    {
        type = Color.white;
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

    private Vector2 GetMovement() {
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
}
