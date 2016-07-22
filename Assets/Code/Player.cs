using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public GameObject particleSystem;

    private Color color = Colors.white;

    private int moveSpeed = 15;

    private TrailRenderer trail;
    private SpriteRenderer sprite;

    void Start () {
        trail = GetComponent<TrailRenderer>();
        sprite = GetComponent<SpriteRenderer>();
    }
	
	void Update () {
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
