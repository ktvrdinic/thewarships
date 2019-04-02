using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    private static MainMenuManager _instance;

    public static MainMenuManager Instance { get { return _instance; } }

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


    public GameObject holderListOfShips;

    public List<Ship> shipyardShips;

    public List<Sprite> rankImages;





    public PlayerInformation playerInformation;

    public void GenerateListOfShipsButtons()
    {
        //destroy if ship was sold
        Transform[] allChildren = holderListOfShips.GetComponentsInChildren<Transform>();
        for (int i = 0; i < allChildren.Length; i++)
        {
            //Debug.Log(allChildren[i].name);
            if (i > 0)
                GameObject.Destroy(allChildren[i]);

        }
        //

        for (int i = 0; i < shipyardShips.Count; i++)
        {
            int x = i;
            //generate button and place it in holder
            GameObject loadObject = Resources.Load("UI/BuyShipPanel") as GameObject;
            GameObject b = Instantiate(loadObject);
            b.transform.SetParent(holderListOfShips.transform);


            ShipyardBuyScript shipyardBuyScript = b.GetComponent<ShipyardBuyScript>();
            shipyardBuyScript.shipNameText.text = shipyardShips[x].shipName;
            shipyardBuyScript.strText.text = shipyardShips[x].strength.ToString();
            shipyardBuyScript.cannText.text = shipyardShips[x].attackValue.ToString();
            shipyardBuyScript.speedText.text = shipyardShips[x].speed.ToString();
            shipyardBuyScript.turntext.text = shipyardShips[x].turnSpeed.ToString();

            //TODO: price for ship ...
            shipyardBuyScript.priceWood.text = (12 * (shipyardShips[x].strength + shipyardShips[x].turnSpeed + shipyardShips[x].speed)).ToString();
            shipyardBuyScript.priceRum.text = (6 * (shipyardShips[x].strength + shipyardShips[x].attackValue + shipyardShips[x].turnSpeed)).ToString();
            shipyardBuyScript.priceGold.text = (4 * (shipyardShips[x].strength + shipyardShips[x].attackValue)).ToString();
            shipyardBuyScript.pricePearl.text = (4 * (shipyardShips[x].speed + shipyardShips[x].turnSpeed)).ToString();


            Button button = b.GetComponentInChildren<Button>();
            Debug.Log("ShipNumb: " + x);
            button.onClick.AddListener(() => { BuyShip(x); });
            ///button.GetComponentInChildren<Text>().text = playerInformation.listOfShips[x].shipName;

        }
    }

    [Header("Resources")]
    public GameObject ResourcesPanel;
    public Text woodPanel, rumPanel, goldPanel, diamondPanel;

    [Space()]

    [Header("MainMenu")]
    public GameObject MainMenu;
    public Text playerNamePanel, playerLevel;
    public Image rankImg;
    public Slider playerNextLevelSlider;
    public Text playerNextLevelPanel;
    public Text main_StrengthPanel, main_CannonsPanel, main_speedPanel, main_turnPanel, main_shipName;

    [Space()]

    [Header("Harbor")]
    public GameObject harbor;
    public Text harbor_StrengthPanel, harbor_CannonsPanel, harbor_speedPanel, harbor_turnPanel, harbor_shipName;
    public Text shipSkillName, shipSkillLvl;
    public Image[] shipSkillImages;

    [Space()]

    [Header("Shipyard")]
    public GameObject shipyard;

    [Space()]

    [Header("Bank")]
    public GameObject bank;

    [Space()]

    [Header("Settings")]
    public GameObject settingsPanel;

    public GameObject soundOn, soundOff, musicOn, musicOff;

    [Space()]

    [Header("Credits")]
    public GameObject creditsPanel;

    [Space()]

    [Header("ExitGame")]
    public GameObject exitPanel;


    [Space()]

    [Header("JoinBattle")]
    public GameObject JoinBattlePanel;



    public void SoundButtonChange(){

		isSoundOn =   !isSoundOn;

		soundOn.SetActive(!isSoundOn);
		soundOff.SetActive(isSoundOn);
	}
	public void MusicButtonChange(){

		isMusicOn =   !isMusicOn;

		musicOn.SetActive(!isMusicOn);
		musicOff.SetActive(isMusicOn);
	}

	bool isSoundOn = false, isMusicOn = false;
	



	void Init(){

		shipSkillsName = new string[]{"Strength", "Cannons Power", "Speed", "Turn Speed"};

		CloseAllPanels();
		UpdateResources();
		//OpenPanel(MainMenu);

		updateMainMenu();

		SelectShipSkill(0);



		
		CreateAllShips();
		SoundButtonChange();
		MusicButtonChange();

		GenerateListOfShipsButtons();
	}

	void CreateAllShips(){
		shipyardShips = new List<Ship>();
		shipyardShips.Add(new Ship("Brig", 1, 5, 10, 350, 5, 8));
		shipyardShips.Add(new Ship("Carrack", 2, 6, 11, 420, 10, 10));
		shipyardShips.Add(new Ship("Cutter", 3, 8, 10, 500, 10, 10));
		shipyardShips.Add(new Ship("Fluyt", 4, 10, 12, 700, 10, 10));
		//shipyardShips.Add(new Ship("Fluyt", 5, 11, 15, 745, 10, 10));

		//////////shipyardShips.Add(new Ship("", 6, 13, 10, 880, 10, 10));
		shipyardShips.Add(new Ship("Frigate", 7, 10, 14, 1000, 10, 10));
		//shipyardShips.Add(new Ship("Galleon", 8, 9, 15, 1300, 10, 10));
		shipyardShips.Add(new Ship("Galleon", 9, 15, 17, 1500, 10, 10));
		shipyardShips.Add(new Ship("Ship of the line", 10, 20, 20, 2000, 10, 10));

		
	}

	public void BuyShip(int i){
		playerInformation.Add_to_shipList(shipyardShips[i]);
        StartCoroutine(Login(i));
    }

    IEnumerator Login(int i)
    {
        WWWForm form = new WWWForm();

        form.AddField("nameOfShip", shipyardShips[i].shipName);
        form.AddField("username", DBManager.username);

        WWW www = new WWW("http://localhost/theWarships/saveData.php", form);

        yield return www;

        if (www.text[0] == '0')
        {
            Debug.Log("Ship is inserted. Username: " + DBManager.username);
        }
        else
        {
            Debug.Log("Ship isn't inserted.");
        }
    }

            public void OpenMenuPanelsOnMaster()
    {
        OpenPanel(MainMenu);
        ResourcesPanel.SetActive(true);
    }



	public void CloseAllPanels(){
		MainMenu.SetActive(false);
		harbor.SetActive(false);
		shipyard.SetActive(false);
		bank.SetActive(false);
		settingsPanel.SetActive(false);
		creditsPanel.SetActive(false);
		exitPanel.SetActive(false);
        JoinBattlePanel.SetActive(false);
	} 

	public void OpenPanel(GameObject panel){
		CloseAllPanels();

		updateMainMenu();
		UpdateHarborMenu();
		UpdateShipValues();
		UpdateHarborSkillLvl();
		
		//Update_ShipSkill_Values_From_Class();

		panel.SetActive(true);
	}

	public void toBattle(){
        //play online
        /////SceneManager.LoadScene(1);
        OpenPanel(JoinBattlePanel);
    }

	public void ExitGame(){
		//play online
		Application.Quit();
	}



	void SelectShip(int i){
		playerInformation.selectShip(i);
	}

	public void NextShip(int i){

		int count = playerInformation.listOfShips.Count;
		int getShip=0;

		if (i==1) //1 right
		{
			getShip = playerInformation.currentShipSelected;
			getShip++;

			if(getShip >= count)
				getShip = 0;
				
		}
		else //0 left
		{
			getShip = playerInformation.currentShipSelected;
			getShip--;

			if(getShip < 0)
				getShip = count - 1;
		}

		playerInformation.selectShip(getShip);

		updateMainMenu();
		
		//update canvas
		
	}
	
	void UpdateResources(){

		woodPanel.text = playerInformation.wood.ToString();
		rumPanel.text = playerInformation.rum.ToString();
		goldPanel.text = playerInformation.gold.ToString();
		diamondPanel.text = playerInformation.pearl.ToString();

	}

	int ship_str, ship_speed, ship_turn, ship_cannons;
	string ship_name;

	void UpdateShipValues(){
		
		UpdateResources();
		
		ship_name = playerInformation.listOfShips[playerInformation.currentShipSelected].shipName;
		ship_str = playerInformation.listOfShips[playerInformation.currentShipSelected].strength;
		ship_cannons = playerInformation.listOfShips[playerInformation.currentShipSelected].attackValue;

		ship_speed = playerInformation.listOfShips[playerInformation.currentShipSelected].speed;
		ship_turn = playerInformation.listOfShips[playerInformation.currentShipSelected].turnSpeed;
	}

	void UpdateHarborMenu(){
		UpdateShipValues();

		harbor_StrengthPanel.text = ship_str.ToString();
		harbor_CannonsPanel.text = ship_cannons.ToString();
		harbor_speedPanel.text = ship_speed.ToString();
		harbor_turnPanel.text = ship_turn.ToString();
		harbor_shipName.text = ship_name;

		//SelectShipSkill(0);

	}

	public void UpgradeCurrentSkill(){

		if(!PlayerHasEnoughResourcesToBuy(100, 2)) return; // price 100 gold for upgrade
		PayWithResources(100, 2);

		Ship ship = playerInformation.listOfShips[playerInformation.currentShipSelected];
		if(ship.shipSkills[currentSkillSelected] < 10)
			ship.shipSkills[currentSkillSelected]++;
		else
			return;

		SelectShipSkill(currentSkillSelected);

		
		
		updateMainMenu();

		UpdateHarborMenu();

		Update_ShipSkill_Values_From_Class();

	}

	string[] shipSkillsName;
	int currentSkillSelected;

	void UpdateHarborSkillLvl(){
		
		Ship ship = playerInformation.listOfShips[playerInformation.currentShipSelected];

		shipSkillName.text = shipSkillsName[currentSkillSelected];
		shipSkillLvl.text = ship.shipSkills[currentSkillSelected] + "/10";
		for (int j = 0; j < shipSkillImages.Length; j++)
		{	
			//Debug.LogWarning(j);
			shipSkillImages[j].gameObject.SetActive(false);
		}

		for (int j = 0; j < shipSkillImages.Length; j++)
		{
			if(j < ship.shipSkills[currentSkillSelected])
			shipSkillImages[j].gameObject.SetActive(true);
		}

	}
	public void SelectShipSkill(int i){
		currentSkillSelected = i;
		Ship ship = playerInformation.listOfShips[playerInformation.currentShipSelected];

		shipSkillName.text = shipSkillsName[i];
		shipSkillLvl.text = ship.shipSkills[i] + "/10"; 

		for (int j = 0; j < shipSkillImages.Length; j++)
		{	
			//Debug.LogWarning(j);
			shipSkillImages[j].gameObject.SetActive(false);
		}

		for (int j = 0; j < shipSkillImages.Length; j++)
		{
			if(j < ship.shipSkills[i])
			shipSkillImages[j].gameObject.SetActive(true);
		}

		

	} 


	void updateMainMenu(){

		UpdateShipValues();

		main_StrengthPanel.text = ship_str.ToString();
		main_CannonsPanel.text = ship_cannons.ToString();
		main_speedPanel.text = ship_speed.ToString();
		main_turnPanel.text = ship_turn.ToString();
		main_shipName.text = ship_name;

		playerNamePanel.text = playerInformation.playerName;
		playerLevel.text = "LEVEL " + playerInformation.level.ToString();
		playerNextLevelPanel.text =  playerInformation.experience.ToString() + "/" + playerInformation.levelExpMax.ToString(); //dodati next lvl points 
		playerNextLevelSlider.minValue = playerInformation.levelExpMin;
		playerNextLevelSlider.maxValue = playerInformation.levelExpMax;
		playerNextLevelSlider.value = playerInformation.experience;
		rankImg.sprite = rankImages[playerInformation.level];
	}

	void Update_ShipSkill_Values_From_Class(){

		Ship ship = playerInformation.listOfShips[playerInformation.currentShipSelected];
		//Debug.LogWarning(currentSkillSelected);

		int value = ship.shipSkills[currentSkillSelected];
		
		switch (currentSkillSelected)
		{
			case 0:
				ship.strength += value * 2; break;
			case 1:
				ship.attackValue += value;
				/*/ship.numberOfCannons += value;*/ break;
			case 2:
				ship.speed += value;  break;
			case 3:
				ship.turnSpeed += value; break;
				
		}
		UpdateHarborMenu();
			 
	}

	void PayWithResources(int value, int res){

		bool chechIfPlayerHasResources = PlayerHasEnoughResourcesToBuy(value, res);

		if(chechIfPlayerHasResources)
		{
			switch (res)
			{	
				case 0:  playerInformation.wood -=  value; break;

				case 1:  playerInformation.rum -=  value; break;
				
				case 2:  playerInformation.gold -=  value; break;
				
				case 3:  playerInformation.pearl -=  value; break;
				
			}	

		}


	}

	bool PlayerHasEnoughResourcesToBuy(int value, int res){ //0 - wood, 1 - rum, 2 - gold, 3 - pearl(diamonds)???
		bool check = false;

		switch (res)
		{	
			case 0:  if(playerInformation.wood - value >= 0) check = true; break;

			case 1:  if(playerInformation.rum - value >= 0) check = true; break;
			
			case 2:  if(playerInformation.gold - value >= 0) check = true; break;
			
			case 3:  if(playerInformation.pearl - value >= 0) check = true; break;
			
		}		

		return check;
	}

	void AddPlayerExp(int value){
		playerInformation.AddExp(value);
		updateMainMenu();
	}

	

	void Start () 
	{
        //GenerateListOfShipsButtons(playerInformation);
        playerInformation = PlayerInformation.Instance;    

		Init();


	}

	public void Update(){

		if (Input.GetKeyDown(KeyCode.A))
        {
            AddPlayerExp(200);
        }


	}
}
