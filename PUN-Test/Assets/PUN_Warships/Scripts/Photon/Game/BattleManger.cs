using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInBattle
{
    public string playerName;
    public int level;
    public int exp;

    public string shipName;
    public int strength;
    public int singleCannonDmg;
    public int speed;
    public int turnSpeed;

}


public class BattleManger : Photon.MonoBehaviour {


    public List<PlayerInBattle> players;



    //PlayerInformation playerInformation;
    [Header("MyPlayer")]
    public Text PlayerNameText; //playername
    public Text PlayerHealthText;
    public Text PlayerLevelText; //shipnameplayer
    public Image PlayerRankImage;
    public Slider PlayerHealthSlider;

    //public float PlayerShipHealth;

    

    [Space]
    [Header("Enemy")]
    public Text EnemyNameText; //enemyname
    public Text EnemyHealthText;
    public Text EnemyLevelText; //shipnnameEnemy
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
        BattleManger.Instance.players = new List<PlayerInBattle>(2);



        //playerInformation = PlayerInformation.Instance;

        //if(playerInformation != null)

        //Init();

        //UpdatePlayerUI();
       // Debug.Log("Username: " + DBManager.username + ", enemy: " + BattleManger.Instance.EnemyNameText.text);



        
    }
    bool isWin = false;
    bool firstCall = true;
    private void Update()
    {
        if(winner != "" && !isWin && PhotonNetwork.isMasterClient)
        {
            isWin = true;
            StartCoroutine(InsertWinnerMySQL());
            Debug.LogError("Winner is :" + winner);
        }


/*
        if(PhotonNetwork.room.playerCount == 2 && PlayerNameText.text == EnemyNameText.text && waitForPlayers.activeInHierarchy)
        {
            if(players.Count == 2 && players[0].playerName != "" && players[1].playerName != "")
                UpdatePlayerUI();
        }
*/
        /*if (PhotonNetwork.room.playerCount == 2)
        {
            if (firstCall)
            {
                foreach (PlayerInBattle p in players)
                {
                    Debug.LogWarning(p.playerName + " : " + p.level);
                }
                firstCall = false;
            }
        }*/

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.LogError(players);
            foreach (PlayerInBattle p in players)
            {
                Debug.LogError(p.playerName + " : " + p.level + " : " + p.shipName + " : " + p.singleCannonDmg + " : " + p.strength);
            }
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

        Debug.Log("Insert winner mYSQL Username: " + winner + ", loser Username: " + loser);

        form.AddField("usernameWin", winner);
        form.AddField("usernameLose", loser); // Dodati neprijatelja
        

        WWW www = new WWW("https://jugoslavian-holes.000webhostapp.com/saveData.php", form);

        yield return www;

        if (www.text[0] == '0') //////tu nesto - array index out of range
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
            
            PlayerNameText.text = PhotonNetwork.player.name; ///playersInGame[0].playerName;
            //Rankimage
            PlayerHealthSlider.value = 100;


            //enemy
            
            EnemyNameText.text = GetEnemyName();  //playersInGame[1].playerName;
            //Rankimage
        


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

    public void StartBattleWhenPlayersConnect()
    {
        if (players == null && players[0] == null && players[1] == null) return;

        if (PhotonNetwork.room.playerCount == 2)
        {
            if (firstCall)
            {
                foreach (PlayerInBattle p in players)
                {
                    Debug.LogError("StartBattleWhenpalyerrsConnect : " + p.playerName + " : " + p.level + " : " + p.shipName + " : " + p.strength);
                }
                firstCall = false;
            }
        }

        StartCoroutine(SetUIValuesOnStart(2));
        
    }

    public int maxHealthPlayer, maxHealthEnemy;

    public void UpdatePlayerUI()
    {
        /*foreach (PlayerInBattle p in players)
        {
            Debug.LogError("UpdatePlyersUI : " + p.playerName + " : " + p.level + " : " + p.shipName + " : " + p.strength);
        }*/

        if (players[0].playerName == null && players[1].playerName == null && PhotonNetwork.room.playerCount != 2) return;


        

        //player
        PlayerNameText.text = players[0].playerName;
        maxHealthPlayer = players[0].strength;
        PlayerHealthText.text = players[0].strength.ToString() + "/" + maxHealthPlayer.ToString();
        PlayerLevelText.text =  "LEVEL " + players[0].level + "\t" + players[0].shipName;
        PlayerHealthSlider.maxValue = maxHealthPlayer;
        PlayerHealthSlider.value = players[0].strength;
        PlayerRankImage.sprite = rankImages[players[0].level];

        //enemy
        EnemyNameText.text = players[1].playerName;
        maxHealthEnemy = players[1].strength;
        EnemyHealthText.text = players[1].strength.ToString() + "/" + maxHealthEnemy.ToString();
        EnemyLevelText.text = "LEVEL " + players[1].level + "\t" + players[1].shipName;
        EnemyHealthSlider.maxValue = maxHealthEnemy;
        EnemyHealthSlider.value = players[1].strength;
        EnemyRankImage.sprite = rankImages[players[1].level];
        
        
    }

    public string winner = "";



    IEnumerator SetUIValuesOnStart(float time)
    {
        yield return new WaitForSeconds(time);
        UpdatePlayerUI();

        yield return StartCoroutine(CloseWaitForPlayersPanel());
    }

     IEnumerator CloseWaitForPlayersPanel()
    {
        UpdatePlayerUI();

        yield return new WaitForSeconds(2f);
        UpdatePlayerUI();
        waitForPlayers.SetActive(false);
        UpdatePlayerUI();
    }


}

