﻿using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {

    private void Start()
    {
        Destroy(gameObject, 2);
    }

    void Update ()
	{

		//if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C))
		//   Destroy(transform.gameObject);
	
	}
}
