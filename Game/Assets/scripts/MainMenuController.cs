using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
   
    // I got help from ChatGPT for codes!!!!!!!!!

    public Text highScore;
    public Text currentLevelText;
    public Image muteImage;
    bool isMuted=false;
    bool menuopened = false;
    public Sprite mute;
    public Sprite unmute;
    public GameObject storyPanel;
    public GameObject infoPanel;
    public GameObject creditsPanel;
    int currentlevel;
    bool waitingForUserInput = false;



    void Start()
    {
        currentlevel = PlayerPrefs.GetInt("currentlevel", 1);
        currentLevelText.text = currentlevel.ToString();

        highScore.text=PlayerPrefs.GetString("highScore", "0");


    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("currentlevel",1);

        SpaceShipScript.isAlive = true;
        ShowHowToPlay();
    }
    public void LoadGame()
    {
        SpaceShipScript.isAlive = true;
        SceneManager.LoadScene("Level" + currentlevel);
        

    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void MuteGame()
    {
        SoundManager.Instance.ToggleMuteAll();
        if (!isMuted)
        {
           
           
            isMuted = true;
            muteImage.sprite = mute;
            
        }
        else
        {
            unMuteGame();
        }

    }
    public void unMuteGame()
    {
        isMuted =false;
        muteImage.sprite = unmute;
        

    }
    public void Credits()
    {
        
        if (!menuopened)
        {
            creditsPanel.SetActive(true);
            menuopened = true;
        }
        else {
            creditsPanel.SetActive(false);
            menuopened = false;

        }
    }
    public void ShowHowToPlay()
    {
        infoPanel.SetActive(true);

        waitingForUserInput = true;
    }

    private void Update()
    {
       
        if (waitingForUserInput && Input.anyKeyDown)
        {
            infoPanel.SetActive(false);          
            storyPanel.SetActive(true);
            waitingForUserInput = false;
        }
    }



}
