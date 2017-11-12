using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playbutton : MonoBehaviour {

    void OnMouseUp()  {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
