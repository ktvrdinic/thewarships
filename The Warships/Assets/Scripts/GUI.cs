using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {

    private float timeLeft;
    public Button windText;

	// Use this for initialization
	void Start () {
        timeLeft = 30.0f;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    

    public void StartWindCount()
    {
        StartCoroutine(CountingDownWind());
    }

    public IEnumerator CountingDownWind()
    {
        windText.interactable = false;
        yield return new WaitForSeconds(timeLeft);
        windText.interactable = true;
    }
}
