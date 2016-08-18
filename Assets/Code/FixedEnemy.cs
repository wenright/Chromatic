using UnityEngine;
using System.Collections;

public class FixedEnemy : Enemy {
    public void Start() {
        //add cases for differant starting moves?
        switch (gc.level) {
            case 6:
            case 8:
                target = new Vector3(this.transform.position.x, -this.transform.position.y, this.transform.position.z);
                break;
            case 7:
            case 9:
                target = new Vector3(-this.transform.position.x, this.transform.position.y, this.transform.position.z);
                break;
            case 10:
                target = new Vector3(-this.transform.position.x, -this.transform.position.y, this.transform.position.z);
                break;
        }
        
    }
    public override void updateTarget() {
       //do nothing by default unless we want more patterns.
       //Or do waypoints
    } 
 }
