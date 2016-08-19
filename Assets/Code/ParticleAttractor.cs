using UnityEngine;
using System.Collections;

public class ParticleAttractor : MonoBehaviour {

	private float speed = 500f;
	private Transform target;
	private ParticleSystem system;
	private ParticleSystem.Particle[] particles;

	public void Awake () {
		 system = GetComponent<ParticleSystem>();
		 particles = new ParticleSystem.Particle[system.maxParticles];
	}

	public void LateUpdate () {
		int numParticles = system.GetParticles(particles);

		for (int i = 0; i < numParticles; i++) {
			// Destroy a particle once it gets close enough to its final position
			if (Vector3.Distance(particles[i].position, target.position) < 0.25f) {
				particles[i].lifetime = 0;
			}

			Vector3 v = target.position - particles[i].position;

			particles[i].velocity += v * (speed / v.sqrMagnitude * Time.deltaTime);
		}

		system.SetParticles(particles, numParticles);
	}

	public void SetTarget (Transform target) {
		this.target = target;
	}
}
