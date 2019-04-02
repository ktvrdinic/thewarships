using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginSql : MonoBehaviour {
    public InputField usernameField;
    public InputField passwordField;
    public Button loginButton;

    private bool connectToMaster = false;

    private void Start()
    {
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
	IEnumerator Login() {
        WWWForm form = new WWWForm();

        form.AddField("name", usernameField.text);
        form.AddField("password", passwordField.text);

        WWW www = new WWW("http://localhost/theWarships/login.php", form);

        yield return www;

        if (www.text[0] == '0')
        {
            string[] userInfo = www.text.Split('\t'); 
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
        loginButton.interactable = (usernameField.text.Length >= 8  && passwordField.text.Length >= 8);
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene("RegisterScene");
    }
}