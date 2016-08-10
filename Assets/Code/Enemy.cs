using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public new GameObject particleSystem;

    private int speed = 500;
    protected Vector3 target;
	protected Color type;
	protected Controller gc;
    protected GameObject player;

	// Use this for initialization
	void Awake () {
        type = ColorList.white;
		gc = GameObject.FindWithTag("GameController").GetComponent<Controller>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        updateTarget();
        move();
		if (!type.Equals(this.GetComponent<SpriteRenderer>().color)){
			this.GetComponent<SpriteRenderer>().color = type;
		}
    }
    public virtual void updateTarget()
    {
        if (player)
        {
            target = player.transform.position;
        }
    }
   void move()  {
        //TODO: FIX THIS HORRIBLE MESS
        Quaternion newRotation = Quaternion.LookRotation(transform.position - target, Vector3.forward);
        newRotation.x = 0.0f;
        newRotation.y = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 500 * Time.deltaTime);
        GetComponent<Rigidbody2D>().AddForce(transform.up * Time.deltaTime * speed);
    }

	public void SetColor(Color color){
        type = color;
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" ) {
            if (this.type != other.GetComponent<Player>().GetColor()) {
                other.GetComponent<Player>().Kill();
            } else {
                Kill();
            }
        }
    }

    public void Kill () {
        // Set particle system color to player color
        particleSystem.GetComponent<ParticleSystem>().startColor = type;
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<Spawner> ().enemycount--;
		gc.hp += 10;
        Instantiate(particleSystem, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
