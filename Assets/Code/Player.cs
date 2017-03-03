using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {
    
    public new GameObject particleSystem;
    public GameObject particleSystemAttractor;

    public ColorChanger RedColorChanger;
    public ColorChanger BlueColorChanger;
    public ColorChanger YellowColorChanger;

    private Color color = ColorList.white;

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
        if (gc.hp == 0) {
            AddColor(ColorList.white);
        }

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
                return Camera.main.ScreenToWorldPoint(new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
            }
        #endif
        
        return transform.position;
    }

    public void AddColor (Color color) {
        if ((color.Equals(ColorList.blue) && this.color.Equals(ColorList.yellow)) || (this.color.Equals(ColorList.blue) && color.Equals(ColorList.yellow))){
            SetColor(ColorList.green);
        } else if ((color.Equals(ColorList.red) && this.color.Equals(ColorList.yellow)) || (this.color.Equals(ColorList.red) && color.Equals(ColorList.yellow))) {
            SetColor(ColorList.orange);
        } else if ((color.Equals(ColorList.blue) && this.color.Equals(ColorList.red)) || (this.color.Equals(ColorList.blue) && color.Equals(ColorList.red))) {
            SetColor(ColorList.purple);
        } else {
            // Release the other one or two colors if not already white
            if (!this.color.Equals(ColorList.white)) {
                if (this.color.Equals(ColorList.green)) {
                    SpawnAttractorSystem(ColorList.blue, BlueColorChanger);
                    SpawnAttractorSystem(ColorList.yellow, YellowColorChanger);
                } else if (this.color.Equals(ColorList.orange)) {
                    SpawnAttractorSystem(ColorList.yellow, YellowColorChanger);
                    SpawnAttractorSystem(ColorList.red, RedColorChanger);
                } else if (this.color.Equals(ColorList.purple)) {
                    SpawnAttractorSystem(ColorList.red, RedColorChanger);
                    SpawnAttractorSystem(ColorList.blue, BlueColorChanger);
                } else if (this.color.Equals(ColorList.red)) {
                    SpawnAttractorSystem(ColorList.red, RedColorChanger);
                } else if (this.color.Equals(ColorList.blue)) {
                    SpawnAttractorSystem(ColorList.blue, BlueColorChanger);
                } else if (this.color.Equals(ColorList.yellow)) {
                    SpawnAttractorSystem(ColorList.yellow, YellowColorChanger);
                }
            }

            SetColor(color);
        }
    }

    private void SpawnAttractorSystem (Color color, ColorChanger cc) {
        GameObject system = Instantiate(particleSystemAttractor, transform.position, transform.rotation) as GameObject;

        var particleSystemMain = system.GetComponent<ParticleSystem>().main;
        particleSystemMain.startColor = color;

        system.GetComponent<ParticleAttractor>().SetTarget(cc.transform);
    }

    public void SetColor (Color color) {
		gc.RestMultiplier ();
        this.color = color;
        trail.material.SetColor("_Color", this.color);
        sprite.color = this.color;
    }

    public Color GetColor () {
        return color;
    }

    public void Kill () {
        // Set particle system color to player color
        var particleSystemMain = particleSystem.GetComponent<ParticleSystem>().main;
        particleSystemMain.startColor = color;

        // Spawn particle system
        Instantiate(particleSystem, transform.position, transform.rotation);

        // Initiate camera shake
        Camera.main.GetComponent<CameraShake>().Shake();
		SceneManager.LoadScene("Score");
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
