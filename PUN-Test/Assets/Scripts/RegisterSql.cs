using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterSql : MonoBehaviour {

    public InputField nameField;
    public InputField emailField;
    public InputField passwordField;

    public Button register;


    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register() {
        WWWForm form = new WWWForm();

        form.AddField("name", nameField.text);
        form.AddField("email", emailField.text);
        form.AddField("password", passwordField.text);

        WWW www = new WWW("https://testwebsitecro.000webhostapp.com/register.php", form);

        yield return www;

        if(www.text == "0")
        {
            Debug.Log("User created successfully.");
            UnityEngine.SceneManagement.SceneManager.LoadScene("LoginScene");
        }
        else
        {
            Debug.Log("User creation failed. Error #" + www.text);
        }
    }

    public void VerifyInputs()
    {
        register.interactable = (nameField.text.Length >= 8 && emailField.text.Length >= 8 && passwordField.text.Length >= 8);
    }

    public void goBack()
    {
        SceneManager.LoadScene("LoginScene");
    }
}
