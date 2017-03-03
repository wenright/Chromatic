using UnityEngine;
using System.Collections;

public class LightParticle : MonoBehaviour {

	private ParticleSystem system;
	private ParticleSystem.Particle[] particles;

	public void Awake () {
		 system = GetComponent<ParticleSystem>();
		 particles = new ParticleSystem.Particle[system.main.maxParticles];
	}

	private void LateUpdate () {
		int numParticles = system.GetParticles(particles);

		for (int i = 0; i < numParticles; i++) {
			// TODO change particle size based on speed
			Vector3 vel = particles[i].velocity;

			particles[i].rotation = Mathf.Rad2Deg * Mathf.Atan2(vel.y, vel.x);
		}

		system.SetParticles(particles, numParticles);
	}
}
