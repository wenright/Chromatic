using UnityEngine;
using System.Collections;

public class FixedEnemy : Enemy {
    public void Start()
    {
        //add cases for differant starting moves?
        target = new Vector3(this.transform.position.x, -this.transform.position.y, this.transform.position.z);
    }
    public override void updateTarget()
    {
       //do nothing by default unless we want more patterns.
       //Or do waypoints
       
    } 
 }
