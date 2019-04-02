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


    }

    public void Init()
    {
        if (playersInGame.Count == 2)
        {

            //player
            PlayerHealthText.text = "100";
            PlayerNameText.text = playersInGame[0].playerName;
            //Rankimage
            PlayerHealthSlider.value = 100;


            //enemy
            EnemyHealthText.text = "100";
            EnemyNameText.text = playersInGame[1].playerName;
            //Rankimage
            EnemyHealthSlider.value = 100;



            //image
        }
    }


    void UpdatePlayerUI()
    {
        
        //rankImages[playerInformation.level];   
    }



}

