using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "CannonBall") // this string is your newly created tag
        {
            Destroy(collider.gameObject);
        }
    }
}
