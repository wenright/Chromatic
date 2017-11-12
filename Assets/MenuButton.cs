using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

    void OnMouseUp()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
