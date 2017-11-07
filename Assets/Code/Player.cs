using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

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

        SpriteRenderer playerRenderer = GetComponent<SpriteRenderer>();
        SkinController skinController = GetComponent<SkinController>();

        int defaultSkin = 3;

        switch (PlayerPrefs.GetInt("skin", defaultSkin)) {
            case (int) SkinController.Skins.skull:
                playerRenderer.sprite = skinController.skull;
                break;
            case (int) SkinController.Skins.square:
                playerRenderer.sprite = skinController.square;
                break;
            case (int) SkinController.Skins.circle:
                playerRenderer.sprite = skinController.circle;
                break;
            case (int) SkinController.Skins.star:
                playerRenderer.sprite = skinController.star;
                break;
            default:
                Debug.LogError("Unknown skin selected!");
                break;
        }
    }

    void Update () {
        if (gc.hp == 0) {
            AddColor(ColorList.white);
        }

        Pulse ();
        transform.position = Vector2.MoveTowards(transform.position, GetMovement(), Time.deltaTime * moveSpeed);
    }

    private Vector2 GetMovement () {
        #if UNITY_EDITOR || UNITY_WEBGL
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
        gc.ResetMultiplier ();

        this.color = color;

        trail.material.DOColor(this.color, 0.25f).SetEase(Ease.OutQuad);
        sprite.DOColor(this.color, 0.25f).SetEase(Ease.OutQuad);
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

        // TODO We probably don't want to upload all scores. Maybe add a check to see if this score is higher than our previouis uploaded score.
        gc.UploadScore();

        // Initiate camera shake
        Camera.main.GetComponent<CameraShake>().Shake();
        gc.Invoke("ShowScoreScreen", 1.0f);
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
