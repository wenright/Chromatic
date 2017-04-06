using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MultiToolTip : MonoBehaviour {
    float maxSize = 0.1f;
    float growthSpeed = 0.3f;
    float shrinkDelay = 0.3f;
    protected Controller gc;

    void Start () {
        gc = GameObject.FindWithTag("GameController").GetComponent<Controller>();
        this.GetComponent<TextMesh>().text = "X"+gc.multiplier.ToString();

        transform.localScale = Vector3.zero;
        Grow();
    }

    private void Grow () {
        transform.DOScale(maxSize, growthSpeed)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => Invoke("Shrink", shrinkDelay));
    }

    private void Shrink () {
        transform.DOScale(0, growthSpeed)
            .SetEase(Ease.InQuad)
            .OnComplete(() => Destroy(gameObject));
    }
}
