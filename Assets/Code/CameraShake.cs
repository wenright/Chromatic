using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    private int numRepetitions = 10;
    private float frequency = 0.025f;
    private float intensity = 0.075f;

    public void Shake () {
        StartCoroutine(ShakeIEnumerator());
    }

    private IEnumerator ShakeIEnumerator () {
        Vector3 startLocation = transform.position;

        for (int i = 0; i < numRepetitions; i++) {
            transform.position = startLocation + new Vector3(Random.value * intensity, Random.value * intensity);
            yield return new WaitForSeconds(frequency);
        }

        transform.position = startLocation;
    }
}
