using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public new GameObject particleSystem;
    public Texture2D warningSprite;

    protected int speed = 500;
    protected Vector3 target;
	protected Color color;
	protected Controller gc;
    protected GameObject player;
    protected int warningSize;

	public bool canHit;


	public bool onScreenOnce;

	void Awake () {
        warningSize = 0;
        onScreenOnce = false;
        color = ColorList.white;
		gc = GameObject.FindWithTag("GameController").GetComponent<Controller>();
        player = GameObject.FindGameObjectWithTag("Player");
        warningSize = 24;
    }

    void OnGUI () {
        // Show a '!' when outside of the screen
		if (!this.gameObject.GetComponent<SpriteRenderer> ().isVisible && !onScreenOnce) {
			Vector3 pos = Camera.main.WorldToScreenPoint (transform.position);
			float x = Mathf.Clamp (pos.x, 0, Screen.width - 50);
			float y = Mathf.Clamp (Screen.height - pos.y, 0, Screen.height - 50);
            if (pos.y > Screen.height)
                y += 15;
            GUI.color = color;
			GUI.DrawTexture (new Rect (x, y, warningSize, warningSize), warningSprite);
		} else {
			onScreenOnce = true;
		}

    }
	
	void Update () {
		
		if (this.color == player.GetComponent<Player> ().GetColor ())
			this.GetComponent<Animator> ().SetBool ("canHit", true);
		else
			this.GetComponent<Animator> ().SetBool ("canHit", false);

        if(warningSize < 48)
         warningSize += 2;
        // TODO better offscreen kill detection (Preferably based on screen height in game units)
        if (!this.GetComponent<SpriteRenderer>().isVisible && onScreenOnce) {
            Kill();  
        }
        updateTarget();
        move();
		if (!color.Equals(this.GetComponent<SpriteRenderer>().color)) {
			this.GetComponent<SpriteRenderer>().color = color;
		}
    }
    public virtual void updateTarget() {
        if (player) {
            target = player.transform.position;
        }
    }
   public virtual void move()  {
        //TODO: FIX THIS HORRIBLE MESS
        Quaternion newRotation = Quaternion.LookRotation(transform.position - target, Vector3.forward);
        newRotation.x = 0.0f;
        newRotation.y = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 500 * Time.deltaTime);
        GetComponent<Rigidbody2D>().AddForce(transform.up * Time.deltaTime * speed);
    }

	public void SetColor(Color color) {
        this.color = color;
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (this.color != other.GetComponent<Player>().GetColor()) {
                other.GetComponent<Player>().Kill();
            } else {
                particleSystem.GetComponent<ParticleSystem>().startColor = color;

                Instantiate(particleSystem, transform.position, transform.rotation);
                gc.hp += 20;
                Kill();
            }
        }
    }

    public void Kill () {
        // Set particle system color to player color
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<Spawner> ().enemycount--;
        Destroy(gameObject);
    }
}
