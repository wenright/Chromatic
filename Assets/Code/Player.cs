using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    
    public new GameObject particleSystem;

    public Color color = ColorList.white;
    private Color oldcolor;

    private Controller gc;

    private int moveSpeed = 15;

    private TrailRenderer trail;
    private SpriteRenderer sprite;
	private bool grow;
	private bool runningOut;
	private float pulsetimer;

    void Awake () {
        trail = GetComponent<TrailRenderer>();
        sprite = GetComponent<SpriteRenderer>();
        color = ColorList.white;
        gc = GameObject.FindWithTag("GameController").GetComponent<Controller>();
		pulsetimer = 1;
    }

	void Update () {
        if (!color.Equals(oldcolor)) {
            if ((color.Equals(ColorList.blue) && oldcolor.Equals(ColorList.yellow)) || (oldcolor.Equals(ColorList.blue) && color.Equals(ColorList.yellow)))
                color = ColorList.green;
            else if ((color.Equals(ColorList.red) && oldcolor.Equals(ColorList.yellow)) || (oldcolor.Equals(ColorList.red) && color.Equals(ColorList.yellow)))
                color = ColorList.orange;
            else if ((color.Equals(ColorList.blue) && oldcolor.Equals(ColorList.red)) || (oldcolor.Equals(ColorList.blue) && color.Equals(ColorList.red)))
                color = ColorList.purple;
        }
        
        if(gc.hp == 0) {
            color = ColorList.white;
        }

        SetColor(color);
		Pulse ();
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
        this.oldcolor = this.color;
        this.color = color;

        trail.material.SetColor("_Color", this.color);
        sprite.color = this.color;
    }

    public Color GetColor () {
        return color;
    }

    public void Kill () {
        // Set particle system color to player color
        particleSystem.GetComponent<ParticleSystem>().startColor = color;

        // Spawn particle system
        Instantiate(particleSystem, transform.position, transform.rotation);

        // Initiate camera shake
        Camera.main.GetComponent<CameraShake>().Shake();

        Destroy(gameObject);
    }

	public void Pulse () {
		if (gc.hp < 50 || this.gameObject.transform.localScale.x != 1) {
			this.gameObject.transform.localScale = Vector3.Lerp (new Vector3 (1, 1, 1), new Vector3 (1.5f, 1.5f, 1.5f), (pulsetimer));
			pulsetimer -= Time.deltaTime * 3;
		}

		if (pulsetimer <= 0 && color != ColorList.white && gc.hp < 50) {
			pulsetimer = 1;
		}
	}
}
