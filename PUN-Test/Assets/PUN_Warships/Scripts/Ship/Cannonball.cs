using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PhotonView))]
public class Cannonball : Photon.MonoBehaviour
{
	// How much damage is applied on hit
	public float damage = 5f;

	// Particle emitter that will be creating a smoke trail behind the cannonball
	public ParticleEmitter smokeEmitter;

	// Cannonballs will be destroyed after this much time passes
	public float maxLifetime = 4f;

	// Smoke will stop being produced by the smoke emitter after this amount of time
	public float smokeCutoffTime = 1f;

	// Object (ship, tower) that fired this cannon ball
	[HideInInspector] public GameObject owner;

	// Cache some values
	Rigidbody mRb;
	float mSpawnTime = 0f;

	void Start ()
	{
		mRb = GetComponent<Rigidbody>();
		mSpawnTime = Time.time;
	}

	/// <summary>
	/// Smoke should start at 100% and taper off to nothing over the course of 'smokeCutoffTime'.
	/// </summary>

	void Update ()
	{
		float lifetime = Time.time - mSpawnTime;

		if (smokeEmitter != null && smokeCutoffTime > 0f)
		{
			float factor = Mathf.Clamp01(lifetime / smokeCutoffTime);
			factor = 1.0f - factor;
			//smokeEmitter = smokeEmitter.maxParticles * (int)factor;
		}

		// Destroy the cannonballs once their lifetime expires
		if (lifetime > maxLifetime) Destroy(gameObject);
	}

    /// <summary>
    /// Going below water should increase drag significantly.
    /// </summary>

    void FixedUpdate()
    {
        Vector3 pos = mRb.position;
        if (pos.y < 0f)
        {
            mRb.drag = 100;
            mRb.angularDrag = 100f;
            //mRb.velocity = Vector3.zero;
        }
    }




}