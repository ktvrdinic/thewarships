using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour {

    private int ID;
    public static int HP;
    public Transform HealthBar;
    public Image enemyHealthBar;

    // Neprijateljevi resursi
    public GameObject resurs;
    public int wood;
    public int rum;
    public int gold;

    // Use this for initialization
    void Start () {
        HP = 100;
	}
	
	// Update is called once per frame
	void Update () {
        HealthBar.LookAt(Camera.main.transform);
	}

    public void instantiateResurse() // Kao argument dodati faktor drvo, rum, zlato
    {
        GameObject bacva = Instantiate(resurs, new Vector3(this.transform.position.x, 0, this.transform.position.z), transform.rotation);
        bacva.GetComponent<ResursInfo>().resID = ID;
        bacva.GetComponent<ResursInfo>().resType = 0;
        bacva.GetComponent<ResursInfo>().resValue = 10; // Mnoziti ce se s faktorom igraca za drvo

        bacva = Instantiate(resurs, new Vector3(this.transform.position.x, 0, this.transform.position.z), transform.rotation);
        bacva.GetComponent<ResursInfo>().resID = ID;
        bacva.GetComponent<ResursInfo>().resType = 1;
        bacva.GetComponent<ResursInfo>().resValue = 10; // Mnoziti ce se s faktorom igraca za rum

        bacva = Instantiate(resurs, new Vector3(this.transform.position.x, 0, this.transform.position.z), transform.rotation);
        bacva.GetComponent<ResursInfo>().resID = ID;
        bacva.GetComponent<ResursInfo>().resType = 2;
        bacva.GetComponent<ResursInfo>().resValue = 10; // Mnoziti ce se s faktorom igraca za zlato
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "CannonBall") // this string is your newly created tag
        {
            HP--;
            Destroy(collider.gameObject);
            enemyHealthBar.fillAmount = HP / 100.0f;
        }

        switch (HP)
        {
            case 90:
                // Dodati animaciju vatre
                instantiateResurse();
                break;
            case 80:
                instantiateResurse();
                break;
            case 70:
                instantiateResurse();
                break;
            case 60:
                instantiateResurse();
                break;
            case 50:
                instantiateResurse();
                break;
            case 40:
                instantiateResurse();
                break;
            case 30:
                instantiateResurse();
                break;
            case 20:
                instantiateResurse();
                break;
            case 10:
                instantiateResurse();
                break;
            case 0:
                Debug.Log("Brod je uništen");
                Destroy(GameObject.Find("Enemy_Pirate_ship"));
                break;
        }
    }
}
