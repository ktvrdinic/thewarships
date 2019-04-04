using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManger : Photon.MonoBehaviour {


    //PlayerInformation playerInformation;
    [Header("MyPlayer")]
    public Text PlayerNameText;
    public Text PlayerHealthText;
    public Image PlayerRankImage;
    public Slider PlayerHealthSlider;
    [Space]
    [Header("Enemy")]
    public Text EnemyNameText;
    public Text EnemyHealthText;
    public Image EnemyRankImage;
    public Slider EnemyHealthSlider;
    public string loser;


    public GameObject waitForPlayers;


    public List<Sprite> rankImages;


    //public List<string> playersInGame;
    public List<PlayerInformation> playersInGame;


    [Space]
    public GameObject winPanel, losePanel, PlayersUI;



    private static BattleManger _instance;

    public static BattleManger Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start ()
   {

        
        //playerInformation = PlayerInformation.Instance;

        //if(playerInformation != null)

        Init();

        UpdatePlayerUI();
        Debug.Log("Username: " + DBManager.username + ", enemy: " + BattleManger.Instance.EnemyNameText.text);

    }
    bool isWin = false;
    private void Update()
    {
        if(winner != "" && !isWin && PhotonNetwork.isMasterClient)
        {
            isWin = true;
            StartCoroutine(InsertWinnerMySQL());
            Debug.LogError("Winner is :" + winner);
        }
    }

    // NE BRISATI
    IEnumerator InsertWinnerMySQL()
    {
        WWWForm form = new WWWForm();

        

        foreach (PhotonPlayer p in PhotonNetwork.playerList)
        {
            //Debug.Log(s + ": PrintPlayersInRoom: " + " -> " + p.name + " : " + p.isMasterClient);
            if (winner != p.name)
            {
                loser = p.name;
            }
        }

        Debug.Log("Username: " + DBManager.username + ", enemy: " + BattleManger.Instance.EnemyNameText.text);

        form.AddField("username", winner);
        form.AddField("username_enemy", loser); // Dodati neprijatelja
        

        WWW www = new WWW("http://localhost/theWarships/saveData.php", form);

        yield return www;

        if (www.text[0] == '0')
        {
            Debug.Log("Battle successfully inserted in MySQL.");
        }
        else
        {
            Debug.Log("Battle creation failed in MySQL. Error #" + www.text);
        }
    }

    public void Init()
    {
        if (playersInGame.Count == 2)
        {

            //player
            PlayerHealthText.text = "100/100";
            PlayerNameText.text = PhotonNetwork.player.name; ///playersInGame[0].playerName;
            //Rankimage
            PlayerHealthSlider.value = 100;


            //enemy
            EnemyHealthText.text = "100/100";
            EnemyNameText.text = GetEnemyName();  //playersInGame[1].playerName;
            //Rankimage
            EnemyHealthSlider.value = 100;



            //image
        }
    }

    string GetEnemyName()
    {
        string s = "";

        foreach (PhotonPlayer p in PhotonNetwork.playerList)
        {
            //Debug.Log(s + ": PrintPlayersInRoom: " + " -> " + p.name + " : " + p.isMasterClient);
            if (!p.name.Equals(PhotonNetwork.player.name))
            {
                s = p.name;
            }
        }


        return s;
    }


    void UpdatePlayerUI()
    {
        
        //rankImages[playerInformation.level];   
    }

    public string winner = "";







}

