using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //static variable means the value is the same for all the objects of this class type and the class itself
    public static GameManager instance; //this static var will hold the Singleton. basically you're using the same thing as you did with the PlayerControl just now

    private int score = 0; //remember: private means that it'll stay in the script, so this can only be seen in the game manager 

    const string DIR_LOGS = "/Logs"; //you're making a directory
    const string FILE_SCORES = DIR_LOGS + "/highScore.txt"; //making a new constant so you have a txt file of all your saved scores in a directory
    const string FILE_ALL_SCORES = DIR_LOGS + "/allScores.csv"; //new constant so you have a txt file of ALL THE SCORES
    string FILE_PATH_HIGH_SCORES; //string for high scores
    string FILE_PATH_ALL_SCORES; //string for all scores

    public int Score
    {
        get { return score;  } //what you've done is that you can't see score anywhere OUTSIDE of this script, but you can see Score 
        set
        {
            score = value;

            //Debug.Log("Someone set the Score!"); //now you have this message to tell you everytime it changes!
            if (score > HighScore)//now you can just check it instead of having to write a buncha code on Update 
            {
                HighScore = score; //USING THE PROPERTY NOT THE VARIABLE
            }

            string fileContents = "";
            if (File.Exists(FILE_PATH_ALL_SCORES))
            {
                fileContents = File.ReadAllText(FILE_PATH_ALL_SCORES);
            }

            fileContents += score + ","; //CSV = comma separated values
            File.WriteAllText(FILE_PATH_ALL_SCORES, fileContents);

        }  
    }
    
    const string PREF_KEY_HIGH_SCORE = "HighScoreKey"; //creating a variable so you can track your strings w/ ease
    int highScore = -1; //IF THE HIGH SCORE IS -1

    public int HighScore //^we are creating a new property for the variable highScore
    {
        get
        {
            if (highScore < 0) //checking if the score is less than -1. only happens 1st time you're pulling out
            {
                //highScore = PlayerPrefs.GetInt(PREF_KEY_HIGH_SCORE, defaultValue: 3); //grab it out of PlayerPrefs. either 3 or default
                if (File.Exists(FILE_PATH_HIGH_SCORES))
                {
                    string fileContents = File.ReadAllText(FILE_PATH_HIGH_SCORES);
                    highScore = Int32.Parse(fileContents); 
                }
                else
                {
                    highScore = 3;
                }
            }
            
            return highScore; 
        }
        set
        {
            highScore = value;
            Debug.Log("new high score!!!");
            Debug.Log("File Path: " + FILE_PATH_HIGH_SCORES);
            //PlayerPrefs.SetInt(PREF_KEY_HIGH_SCORE, highScore); 

            if (!File.Exists(FILE_PATH_HIGH_SCORES)) //putting an ! means it does not exist
            {
                Directory.CreateDirectory(Application.dataPath + DIR_LOGS);
                //File.Create(FILE_PATH_HIGH_SCORES);
            }
            
            File.WriteAllText(FILE_PATH_HIGH_SCORES, highScore + ""); //make an empty string 
        }
    }

    int targetScore = 3;

    int currentLevel = 0;

    public TextMesh text;  //TextMesh Component to tell you the time and the score

    void Awake()
    {
        if (instance == null) //instance hasn't been set yet
        {
            DontDestroyOnLoad(gameObject);  //Dont Destroy this object when you load a new scene
            instance = this;  //set instance to this object
        }
        else  //if the instance is alsready set to an object
        {
            Destroy(gameObject); //destroy this new object, so there is only ever one
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FILE_PATH_HIGH_SCORES = Application.dataPath + FILE_SCORES;
        FILE_PATH_ALL_SCORES =  Application.dataPath + FILE_ALL_SCORES;
    }

    // Update is called once per frame
    void Update()
    {
        //update the text with the score and level
        text.text = "level:" + currentLevel + 
                    "\nboops: " + score + 
                    "\nbooped: " + targetScore +
                    "\nhigh score: " + HighScore; //change the variable highScore to the Property HighScore
        
        if (score == targetScore)  //if the current score == the targetScore
        {
            currentLevel++; //increase the level number
            SceneManager.LoadScene(currentLevel); //go to the next level when u hit the prize
            targetScore += targetScore + targetScore/2; //update target score
        }
    }
}
