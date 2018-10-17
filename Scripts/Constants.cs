using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Constants : MonoBehaviour {

    private static string basePath = "C:\\UnityRegisterTest\\";
    private static string scorePath = basePath + "scores\\";
    private static string levelPath = basePath + "levels\\";

    public static string getBasePath()
    {
        return basePath;
    }

    public static string getBaseScorePath()
    {
        return scorePath;
    }

    public static string getBaseLevelPath()
    {
        return levelPath;
    }

    public static string getUserPath(string user)
    {
        return basePath + user + ".txt";
    }

    public static string getScorePath(string user)
    {
        return scorePath + user + ".txt";
    }

    public static string getLevelPath(string user)
    {
        return levelPath + user + ".txt";
    }

    public static void createBaseDirs()
    {
        try
        {
            if (!Directory.Exists(Constants.getBasePath()))
            {
                Directory.CreateDirectory(Constants.getBasePath());
            }

        }
        catch (IOException ex)
        {
            print(ex.Message);
        }

        try
        {
            if (!Directory.Exists(Constants.getBaseScorePath()))
            {
                Directory.CreateDirectory(Constants.getBaseScorePath());
            }

        }
        catch (IOException ex)
        {
            print(ex.Message);
        }

        try
        {
            if (!Directory.Exists(Constants.getBaseLevelPath()))
            {
                Directory.CreateDirectory(Constants.getBaseLevelPath());
            }

        }
        catch (IOException ex)
        {
            print(ex.Message);
        }
    }

    // Use this for initialization
    void Start () {
        print("starting constants");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        print("starting constants");
    }
}
