using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class InGameControls : MonoBehaviour {

    private float cameraAngle;
    public Camera camMain;
    public Camera camRight;
    public Camera camLeft;
    public Camera camFront;
    public Camera camBack;
    public static short currentCamera = 0; // 0 - Main, 1 - Front, 2 - Right, 3 - Back, 4 - Left


    // Dugmad u igri
    public GameObject buttonFireMode;
    public GameObject buttonsInFireMode;
    public Button buttonFireInFireMode;
    public float reloadingTime;

    // Varijable za ispaljivanje topova
    public GameObject cannonBall;
    GameObject thisCannonBall;

    public Slider firePowerSlinder;

    public Transform[] cannonRight;
    public Transform[] cannonLeft;
    public Transform[] cannonFront;
    public Transform[] cannonBack;
    // --

    private float verticalInput;
    private float horizontalInput;

    // Provjera da li igrac moze pucati
    private float []cannonTimeBeg;
    private float []cannonTimeEnd;
    

    // Use this for initialization
    void Start () {
        reloadingTime = 15f;
        cannonTimeBeg = new float[4];
        cannonTimeEnd = new float[4];
    }

	// Update is called once per frame
	void Update () {
        
    }

    public void ExitFireModeButton()
    {
        
        buttonFireMode.SetActive(true);
        buttonsInFireMode.SetActive(false);

        camMain.enabled = true;
        camRight.enabled = false;
        camLeft.enabled = false;
        camFront.enabled = false;
        camBack.enabled = false;

        currentCamera = 0;
    }

    // Funkcija bira stranu pucanja topova pomoću kuta kamere
    public void FireButtonMode()
    {
        cameraAngle = GetComponent<CameraFollow360>().AngleBetweenVectors();

        buttonFireMode.SetActive(false);
        buttonsInFireMode.SetActive(true);
        
        if (cameraAngle >= -145 && cameraAngle <= -45)
        {
            camLeft.enabled = true;
            camMain.enabled = false;
            camRight.enabled = false;
            camFront.enabled = false;
            camBack.enabled = false;

            if ((Time.time - cannonTimeBeg[3]) < reloadingTime) StartCoroutine(CountingDownCannon(Mathf.Clamp(reloadingTime - (Time.time - cannonTimeBeg[3]), 0, reloadingTime)));
            else buttonFireInFireMode.interactable = true;

            currentCamera = 4;
            Debug.Log("Pucanje lijevo");
        }
        else if (cameraAngle <= 145 && cameraAngle >= 45)
        {
            camRight.enabled = true;
            camMain.enabled = false;    
            camLeft.enabled = false;
            camFront.enabled = false;
            camBack.enabled = false;

            if ((Time.time - cannonTimeBeg[1]) < reloadingTime) StartCoroutine(CountingDownCannon(Mathf.Clamp(reloadingTime - (Time.time - cannonTimeBeg[1]), 0, reloadingTime)));
            else buttonFireInFireMode.interactable = true;

            currentCamera = 2;
            Debug.Log("Pucanje desno");
        }
        else if ((cameraAngle >= 145 && cameraAngle <= 173) || (cameraAngle >= -173 && cameraAngle <= -145))
        {
            camFront.enabled = true;
            camMain.enabled = false;
            camRight.enabled = false;
            camLeft.enabled = false;            
            camBack.enabled = false;

            if ((Time.time - cannonTimeBeg[0]) < reloadingTime) StartCoroutine(CountingDownCannon(Mathf.Clamp(reloadingTime - (Time.time - cannonTimeBeg[0]), 0, reloadingTime)));
            else buttonFireInFireMode.interactable = true;

            currentCamera = 1;
            Debug.Log("Pucanje naprijed");
        }
        else if ((cameraAngle >= 0 && cameraAngle < 45) || (cameraAngle <= 0 && cameraAngle > -45))
        {
            camBack.enabled = true;
            camMain.enabled = false;
            camRight.enabled = false;
            camLeft.enabled = false;
            camFront.enabled = false;

            // Enable and disable button for fire for specific side
            if ((Time.time - cannonTimeBeg[2]) < reloadingTime) StartCoroutine(CountingDownCannon(Mathf.Clamp(reloadingTime - (Time.time - cannonTimeBeg[2]), 0, reloadingTime)));
            else buttonFireInFireMode.interactable = true;
            currentCamera = 3;
            Debug.Log("Pucanje natrag" );
        }   
    }

    
    public void ChooseCannonsShoot()
    {
        if (camRight.enabled)
        {
            Debug.Log("Cannon Shoot Right");
            for (int i = 0; i < cannonRight.Length; ++i)
            {
                //GameObject thisCannonBall = Instantiate(cannonBall, transform.position, transform.rotation);
                thisCannonBall = Instantiate(cannonBall, cannonRight[i].position, cannonRight[i].rotation);
                thisCannonBall.GetComponent<Rigidbody>().AddRelativeForce(0, 0, firePowerSlinder.value, ForceMode.Impulse);
            }
            cannonTimeBeg[1] = Time.time;
            StartCoroutine(CountingDownCannon(reloadingTime));
        }
        else if (camLeft.enabled)
        {
            Debug.Log("Cannon Shoot Left");
            for (int i = 0; i < cannonLeft.Length; ++i)
            {
                //GameObject thisCannonBall = Instantiate(cannonBall, transform.position, transform.rotation);
                thisCannonBall = Instantiate(cannonBall, cannonLeft[i].position, cannonLeft[i].rotation);
                thisCannonBall.GetComponent<Rigidbody>().AddRelativeForce(0, 0, firePowerSlinder.value, ForceMode.Impulse);
            }
            cannonTimeBeg[3] = Time.time;
            StartCoroutine(CountingDownCannon(reloadingTime));
        }
        else if (camBack.enabled)
        {
            Debug.Log("Cannon Shoot Back");
            for (int i = 0; i < cannonBack.Length; ++i)
            {
                //GameObject thisCannonBall = Instantiate(cannonBall, transform.position, transform.rotation);
                
                thisCannonBall = Instantiate(cannonBall, cannonBack[i].position, cannonBack[i].rotation);
                thisCannonBall.GetComponent<Rigidbody>().AddRelativeForce(0, 0, firePowerSlinder.value, ForceMode.Impulse);
            }
            cannonTimeBeg[2] = Time.time;
            StartCoroutine(CountingDownCannon(reloadingTime));
        }
        else if (camFront.enabled)
        {
            Debug.Log("Cannon Shoot Front");
            for (int i = 0; i < cannonFront.Length; ++i)
            {
                //GameObject thisCannonBall = Instantiate(cannonBall, transform.position, transform.rotation);
                thisCannonBall = Instantiate(cannonBall, cannonFront[i].position, cannonFront[i].rotation);
                thisCannonBall.GetComponent<Rigidbody>().AddRelativeForce(0, 0, firePowerSlinder.value, ForceMode.Impulse);
            }
            cannonTimeBeg[0] = Time.time;
            StartCoroutine(CountingDownCannon(reloadingTime));
        }
    }

    public IEnumerator CountingDownCannon(float timeReturn)
    {
        buttonFireInFireMode.interactable = false;
        yield return new WaitForSeconds(timeReturn);
        buttonFireInFireMode.interactable = true;
    }
    
}
