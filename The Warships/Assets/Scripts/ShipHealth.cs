using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour {

    public static int HP;
    public Transform HealthBar;
    public Image enemyHealthBar;

	// Use this for initialization
	void Start () {
        HP = 100;
	}
	
	// Update is called once per frame
	void Update () {
        HealthBar.LookAt(Camera.main.transform);
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "CannonBall") // this string is your newly created tag
        {
            HP--;
            Destroy(collider.gameObject);
            enemyHealthBar.fillAmount = HP / 100.0f;
        }
        if(HP < 0)
        {
            Debug.Log("Brod je uništen");
            Destroy(GameObject.Find("Enemy_Pirate_ship"));
        }
    }
}
