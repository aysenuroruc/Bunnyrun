using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.IO;

public class register : MonoBehaviour
{
    public GameObject username;
    public GameObject email;
    public GameObject password;
    private string Username;
    private string Email;
    private string Password;
    private string ConfPassword;
    private string form;
    private bool EmailValid = false;
    private string[] Characters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "y", "z", "x" , "w",
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P","Q","R", "S", "T", "U", "V", "Y", "Z", "X", "W",
    "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    //private object confpassword;
    public GameObject confpassword;

    // Use this for initialization
    /* void Start()
    {

    } */
    public void RegisterButtonOn()
    {
        bool UN = false;
        bool EM = false;
        bool PW = false;
        bool CPW = false;

        if (Username != "")
        {
            if (!File.Exists(@Constants.getUserPath(Username)))
            {
                UN = true;
            }
            else
            {
                Debug.LogWarning("Username Taken");
            }
            // print("Registtration Successful");
        }
        else
        {
            Debug.LogWarning("Username field empty");
        }
        if (Email != "")
        {
              EmailValidation();
            if (EmailValid)
            {
              
                if (Email.Contains("@")){
                    if (Email.Contains("."))
                    {
                        EM = true;
                    }else
                    {
                      Debug.LogWarning("Email is invalid");
                    }
                }
                else
                {
                   Debug.LogWarning("Email is incorrect");
                }
            }
            else
            {
                Debug.LogWarning("Email is BBincorrect");
            }
        }
        else
        {
            Debug.LogWarning("Email field empty");
        }
        if(Password != "")
        {
            if(Password.Length > 5)
            {
                PW = true;
            }
            else
            {
                Debug.LogWarning("Password must be atleast 6 Characters Long");
            }
        }
        else
        {
            Debug.LogWarning("Password Field is Empty");
        }
        if(ConfPassword != "")
        {
            if(ConfPassword == Password)
            {
                CPW = true;
            }
            else
            {
                Debug.LogWarning("Password Dont Match");
            }
        }
        else
        {
            Debug.LogWarning("Confirm Password Field");
        }
        if(UN == true &&EM == true && PW == true&& CPW == true)
        {
            int i = 1;
            string newPassword="";
            foreach(char c in Password)
            {
                i++;
                char Encrypted = (char)(c * i);
                newPassword += Encrypted.ToString();
            }
            form = (Username + Environment.NewLine + Email + Environment.NewLine + newPassword);
            File.WriteAllText(@Constants.getUserPath(Username), form);
            username.GetComponent<InputField>().text = "";
            email.GetComponent<InputField>().text = "" ;
            password.GetComponent<InputField>().text = "";
            confpassword.GetComponent<InputField>().text = "";
            print("Registration Completed");
            string scorePath = @Constants.getScorePath(Username);
            if (!File.Exists(scorePath))
            {
                File.WriteAllText(scorePath, "0.0");
            }
            string levelPath = @Constants.getLevelPath(Username);
            if (!File.Exists(levelPath))
            {
                File.WriteAllText(levelPath, "1");
            }
            PlayerPrefs.SetFloat("CurrentScore", 0);
            PlayerPrefs.SetFloat("CurrentLevel", 1);
            PlayerPrefs.SetString("Username", Username);
            SceneManager.LoadScene("Title");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                email.GetComponent<InputField>().Select();
            }
            if (email.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                confpassword.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Password != "" && Email != "" && Password !="" && ConfPassword != "")
            {
                RegisterButtonOn();
            }
        }
        Username = username.GetComponent<InputField>().text;
        Email = email.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        ConfPassword = confpassword.GetComponent<InputField>().text;
    }

    void EmailValidation()
    {
        bool SW = false;
        bool EW = false;
     for (int i =0; i<Characters.Length; i++)
        {
            if (Email.StartsWith(Characters[i]))
            {
                SW = true;
            }
        }
        for (int i = 0; i < Characters.Length; i++)
        {
            if (Email.EndsWith(Characters[i]))
            {
                EW = true;
            }
        }
        if(SW == true&&EW == true)
        {
            EmailValid = true;
        }
        else
        {
            EmailValid = false;
        }
    }
}
