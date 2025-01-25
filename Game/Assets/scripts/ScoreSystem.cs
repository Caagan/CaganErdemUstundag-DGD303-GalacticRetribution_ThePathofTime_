using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public Text scoreText;
    public GameObject missionComplate;
    public static int currentlevel;
    private SpaceShipScript _spaceShipScript;
    int nextlevel;
    bool missionpassed=false;
    string currentHighScore;

   

    public int[] minimumLevelScores = {300,400,500,600,700};//gorevleri tamamlamak icin kart harici minimim skor
    GameObject SpaceScriptObj;


    private void Start()
    {
        currentlevel = PlayerPrefs.GetInt("currentlevel", 1); //leveli al level yoksa birden baslat
        nextlevel = currentlevel + 1; 
        currentHighScore= PlayerPrefs.GetString("highScore","0"); //leveli al level yoksa birden baslat


        SpaceScriptObj = GameObject.FindGameObjectWithTag("SpaceShip");
        if (SpaceScriptObj != null)
        {
            _spaceShipScript = SpaceScriptObj.GetComponent<SpaceShipScript>();
            Debug.Log(minimumLevelScores[currentlevel - 1]);

        }

    }
    private void Update()
    {
        if(SpaceScriptObj != null)
        CheckLevel();
        if (missionpassed)
        {
            if (Input.anyKeyDown)
            {
                missionpassed = false;
                // Aktif olan sahneyi yeniden yükle
                if (currentlevel <= 4)//sonlevel5
                {
                    PlayerPrefs.SetInt("currentlevel", nextlevel);

                    SceneManager.LoadScene("Level" + nextlevel);
                    nextlevel++;
                }
                else {
                    //bitis
                    


                }
                

            }
           
        }
       
    }

    public void IncreaseScore(int amount)
    {
        scoreText.text = (Convert.ToInt32(scoreText.text) + amount).ToString();

        if (Convert.ToInt32(currentHighScore) < Convert.ToInt32(scoreText.text))
        {
            PlayerPrefs.SetString("highScore", scoreText.text); //rekor güncelle
        }
       
        
    }
    public void CheckLevel()
    {
        if (_spaceShipScript.partCollected && minimumLevelScores[currentlevel - 1] <= (Convert.ToInt32(scoreText.text)))
        {
            NextLevel();
        }
    }
    void NextLevel()
    {
       
        missionComplate.SetActive(true);
        missionpassed = true;
       



    }

}
