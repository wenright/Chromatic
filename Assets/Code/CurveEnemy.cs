using UnityEngine;
using System.Collections;

public class CurveEnemy : Enemy {
    private Vector3 pos;
    private Vector3 axis;
    bool left;

    void Start()
    {
        axis = transform.up;
        pos = transform.position;
    }
    public override void move()
    {

        if(left)
            pos -= (transform.right) * Time.deltaTime * 7;
        else
            pos += (transform.right) * Time.deltaTime * 7;
        transform.position = (pos) + axis * Mathf.Sin(Time.time * 8);
    }

    public void SetLeft()
    {
        left = true;
    }
    public void SetRight()
    {
        left = false;
    }
}
