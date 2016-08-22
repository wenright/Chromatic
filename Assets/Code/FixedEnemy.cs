using UnityEngine;
using System.Collections;

public class FixedEnemy : Enemy {
    public void Start() {
        //add cases for differant starting moves?
        switch (gc.level) {
			default:
			if(Camera.main.WorldToScreenPoint (transform.position).y > Screen.height || Camera.main.WorldToScreenPoint (transform.position).y < 0)
                target = new Vector3(this.transform.position.x, -this.transform.position.y, this.transform.position.z);
			else
                target = new Vector3(-this.transform.position.x, this.transform.position.y, this.transform.position.z);
                break;
            case 9:
                target = new Vector3(-this.transform.position.x, -this.transform.position.y, this.transform.position.z);
                break;
        }
        
    }
    public override void updateTarget() {
       //do nothing by default unless we want more patterns.
       //Or do waypoints
    } 
 }
