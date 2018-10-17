using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.IO;

public class login : MonoBehaviour
{
    public GameObject username;
    public GameObject password;
    private string Username;
    private string Password;
    public string DecyptedPassword;
    private string form;
    public String[] Lines;
    /* private string[] Characters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "y", "z", "x" , "w",
         "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P","Q","R", "S", "T", "U", "V", "Y", "Z", "X", "W",
     "1", "2", "3", "4", "5", "6", "7", "8", "9" }; */
    void Awake()
    {
        Constants.createBaseDirs();
    }
    public void LoginButton()
    {
        bool UN = false;
        bool PW = false;
        if (Username != "")
        {
            if (File.Exists(@Constants.getUserPath(Username)))
            {
                UN = true;
                Lines =File.ReadAllLines(@Constants.getUserPath(Username));
            }
            else
            {
                Debug.LogWarning("Username invalid");
            }
        }
        else
        {
            Debug.LogWarning("Username Field Empty");
        }
        if (Password != "") {
            if (File.Exists(@Constants.getUserPath(Username)))
            {
                int i = 1;
                string filePass = Lines[2];
                string newPassword = "";
                foreach (char c in Password)
                {
                    i++;
                    char Encrypted = (char)(c * i);
                    newPassword += Encrypted.ToString();
                }
                if (filePass == newPassword) {
                    PW = true;
                }
                else
                {
                    Debug.LogWarning("Password is invalid-a");
                }
            }
            else
            {
                Debug.LogWarning("Password is invalid-b");
            }
        }
        else {
            Debug.LogWarning("Password Field Empty");
        }
        if(UN == true&& PW == true)
        {
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text= "";
            print ("login success");
            SceneManager.LoadScene("Continue");
            PlayerPrefs.SetString("Username", Username);
            string scorePath = @Constants.getScorePath(PlayerPrefs.GetString("Username"));
            if (File.Exists(scorePath))
            {
                PlayerPrefs.SetFloat("CurrentScore", float.Parse(File.ReadAllLines(scorePath)[0]));
            }
            string levelPath = @Constants.getLevelPath(PlayerPrefs.GetString("Username"));
            if (File.Exists(levelPath))
            {
                PlayerPrefs.SetInt("CurrentLevel", int.Parse(File.ReadAllLines(levelPath)[0]));
            }
            // Application.LoadLevel("Start Menü"); Anymore doesnt using this
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Username != "" && Password != "")
            {
                LoginButton();
            }
        }
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
    }
}
 

 
