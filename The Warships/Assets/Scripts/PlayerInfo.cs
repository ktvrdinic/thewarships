using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {

    // Dodati skill za skupljanje vise resursa
    // Dodati posadu
    public int playerHealth = 110;

    public int playerID;
    public int rWood;
    public int rRum;
    public int rGold;

    public Text wood;
    public Text rum;
    public Text gold;
    public Text username;
    public Text healthOfPlayer;

    private void Awake()
    {
        if (DBManager.username == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }else
        {
            wood.text = DBManager.drvo.ToString();
            rum.text = DBManager.rum.ToString();
            gold.text = DBManager.zlato.ToString();
            username.text = "Username " + DBManager.username;
        }
    }

    // Use this for initialization
    void Start () {
        healthOfPlayer.text = playerHealth.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateWood(int value) {
        rWood += value;
        wood.text = rWood.ToString();
    }

    public void updateRum(int value)
    {
        rRum += value;
        rum.text = rRum.ToString();
    }

    public void updateGold(int value)
    {
        rGold += value;
        gold.text = rGold.ToString();
    }

    public void damageShip(int value)
    {
        playerHealth -= value;
        healthOfPlayer.text = playerHealth.ToString();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ResursInfo>())
        {
            Debug.Log("Resurs collider");
            ResursInfo temp = other.GetComponent<ResursInfo>();
            if (true) // Provjera ID-a resID i playerID
            {
                switch (temp.resType)
                {
                    case 0:
                        Debug.Log("Pokupili ste drvo");
                        updateWood(temp.resValue);
                        break;
                    case 1:
                        Debug.Log("Pokupili ste rum");
                        updateRum(temp.resValue);
                        break;
                    case 2:
                        Debug.Log("Pokupili ste zlato");
                        updateGold(temp.resValue);
                        break;
                }
                Destroy(other.gameObject);
            }
        }else if (other.GetComponent<BombScript>())
        {
            Debug.Log("Boom!!");
            damageShip(other.GetComponent<BombScript>().damageOfBomb);
            Destroy(other.gameObject);
        }
    }
}
