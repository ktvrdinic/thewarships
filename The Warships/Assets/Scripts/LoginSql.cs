using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginSql : MonoBehaviour {
    public InputField usernameField;
    public InputField passwordField;
    public Button loginButton;

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
            DBManager.score = int.Parse(userInfo[6]);
            DBManager.broj_brodova = int.Parse(userInfo[2]);
            DBManager.zlato = int.Parse(userInfo[3]);
            DBManager.rum = int.Parse(userInfo[4]);
            DBManager.drvo = int.Parse(userInfo[5]);
            DBManager.biseri = int.Parse(userInfo[6]);
            DBManager.score = int.Parse(userInfo[7]);
            DBManager.level = int.Parse(userInfo[8]);
            // username[1], broj_brodova[2], zlato[3], rum[4], drvo[5], biseri[6], score[7], level[8]

            Debug.Log("User Sign In successfully. Username: " + userInfo[1] + ", and he have " + userInfo[3] + " golds.");
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
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