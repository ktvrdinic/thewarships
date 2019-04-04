using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginSql : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Button loginButton;

    private List<Ship> AllShips;

    private bool connectToMaster = false;

    private void Start()
    {
        DBManager.shipyardShips = new List<Ship>();


        AllShips = new List<Ship>();
        AllShips.Add(new Ship("Brig", 1, 5, 10, 350, 5, 8));
        AllShips.Add(new Ship("Carrack", 2, 6, 11, 420, 10, 10));
        AllShips.Add(new Ship("Cutter", 3, 8, 10, 500, 10, 10));
        AllShips.Add(new Ship("Fluyt", 4, 10, 12, 700, 10, 10));
        AllShips.Add(new Ship("Frigate", 5, 10, 14, 1000, 10, 10));
        AllShips.Add(new Ship("Galleon", 6, 15, 17, 1500, 10, 10));
        AllShips.Add(new Ship("Ship of the line", 7, 20, 20, 2000, 10, 10));


        //ImageFade.Instance.startfade = true;
    }

    private void Update()
    {
        if (ImageFade.Instance.startfade == false && connectToMaster)
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu01");
    }

    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    // Use this for initialization
    IEnumerator Login()
    {
        WWWForm form = new WWWForm();

        form.AddField("name", usernameField.text);
        form.AddField("password", passwordField.text);

        WWW www = new WWW("http://localhost/theWarships/login.php", form);

        yield return www;

        if (www.text[0] == '0')
        {
            string[] loginRows = www.text.Split('|');
            string[] userInfo = new string[11];
            string[] loginColumns = new string[7];

            userInfo = loginRows[0].Split('_');


            // Punjenje baze i informacija igraca
            DBManager.username = userInfo[1];
            DBManager.no_ships = int.Parse(userInfo[2]);
            DBManager.gold = int.Parse(userInfo[3]);
            DBManager.rum = int.Parse(userInfo[4]);
            DBManager.wood = int.Parse(userInfo[5]);
            DBManager.pearl = int.Parse(userInfo[6]);
            DBManager.experience = int.Parse(userInfo[7]);
            DBManager.level = int.Parse(userInfo[8]);
            DBManager.no_victory = int.Parse(userInfo[9]);
            DBManager.no_lose = int.Parse(userInfo[10]);
            // username[1], no_ships[2], gold[3], rum[4], wood[5], pearl[6], experience[7], level[8], no_victory[9], no_lose[10]


            for (int i = 1; i < loginRows.Length - 1; ++i)
            {
                loginColumns = loginRows[i].Split('_');

                // Debug.Log(loginColumns.Length + ": - " + loginColumns[0] + " " + int.Parse(loginColumns[1]) + " " + int.Parse(loginColumns[3]) + " " + int.Parse(loginColumns[5]) + " " + int.Parse(loginColumns[4]) + " " + int.Parse(loginColumns[6]) + " " + int.Parse(loginColumns[2]));
                Ship ship = new Ship(loginColumns[0], 
                                    int.Parse(loginColumns[1]), 
                                    int.Parse(loginColumns[3]) + AllShips[int.Parse(loginColumns[1]) - 1].speed, 
                                    int.Parse(loginColumns[5]) + AllShips[int.Parse(loginColumns[1]) - 1].turnSpeed, 
                                    int.Parse(loginColumns[4]) * 2 + AllShips[int.Parse(loginColumns[1]) - 1].strength, 
                                    int.Parse(loginColumns[6]) + AllShips[int.Parse(loginColumns[1]) - 1].singleCannonDmg, 
                                    int.Parse(loginColumns[2]) + AllShips[int.Parse(loginColumns[1]) - 1].numberOfCannons);
                ship.shipSkills = new int[] { int.Parse(loginColumns[4]), int.Parse(loginColumns[6]), int.Parse(loginColumns[3]), int.Parse(loginColumns[5])};
                DBManager.shipyardShips.Add(ship);
                // shipName[0], tier[1], no_cannons[2], speed[3], strength[4], turn_speed[5], singleCannonDmg[6]
            }





            Debug.Log("User Sign In successfully. Username: " + userInfo[1] + ", and he have " + userInfo[3] + " golds.");

            ImageFade.Instance.fadeaway = false;
            ImageFade.Instance.startfade = true;
            connectToMaster = true;
        }
        else
        {
            Debug.Log("User creation failed. Error #" + www.text);
        }
    }

    public void VerifyInputs()
    {
        loginButton.interactable = (usernameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene("RegisterScene");
    }
}