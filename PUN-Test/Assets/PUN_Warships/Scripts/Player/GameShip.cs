﻿//using Photon.Pun;
using UnityEngine;


public class GameShip : MonoBehaviour
{
    public Animation destroyAnimation;

    public bool isDead = false;
    

	// Units per second
	public float maxMovementSpeed = 7f;

	// Degrees per second
	public float maxTurningSpeed = 60f;

	// Same as hull damage, but for sails
	public float sailDamageReduction = 0f;

	// Current and maximum sail health
	public Vector2 sailHealth = new Vector2(100f, 100f);

    public float ShipHealth;
    public int Ship_singleCannonDmg = 1;

	Vector3 mLastPos;
	Vector3 mVelocity;

	/// <summary>
	/// Calculated movement speed depends on the current condition of the sails.
	/// </summary>

	public float movementSpeed { get { return maxMovementSpeed * sailHealth.x / sailHealth.y; } }

	/// <summary>
	/// Calculated turning speed depends on the current condition of the hull.
	/// </summary>

	public float turningSpeed { get { return maxTurningSpeed * 1f;  /*health.x / health.y;*/ } }

	/// <summary>
	/// Current velocity in units per second.
	/// </summary>

	public Vector3 velocity { get { return mVelocity; } }

	/// <summary>
	/// Helper function that finds the ship stats script that contains the specified child in its transform hierarchy.
	/// </summary>

	new static public GameShip Find (Transform trans)
	{
		while (trans != null)
		{
			GameShip stats = trans.GetComponent<GameShip>();
			if (stats != null) return stats;
			trans = trans.parent;
		}
		return null;
	}

    /// <summary>
    /// Cache some values.
    /// </summary>
    PhotonView photonView;
    void Start ()
	{
        photonView = GetComponent<PhotonView>();
    }

	/// <summary>
	/// Apply the specified amount of damage to the ship's sails.
	/// </summary>
   
	public float ApplyDamageToSails (float val)
	{

		if (val < 0f) val = 0f;
		//val *= (1.0f - sailDamageReduction);
		//val = Mathf.Min(sailHealth.x, val);
		//sailHealth.x -= val;

        ShipHealth -= val;
		

        if (ShipHealth == 0 && !isDead)
        {
            //dead
            isDead = true;

            if (destroyAnimation != null)
            {
                //destroyAnimation.Play();
                Debug.Log("Dead " + this.gameObject.name);

            }

        }

        Debug.Log("Damage: " + val);

        return val;
    }


    

	/// <summary>
	/// Calculate the ship's velocity.
	/// </summary>

	void LateUpdate ()
	{
		Vector3 pos = transform.position;
		mVelocity = (pos - mLastPos) * (1.0f / Time.deltaTime);
		mLastPos = pos;
	}

}