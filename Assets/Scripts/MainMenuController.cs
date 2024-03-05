using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject creditsMenu;
    public GameObject mainMenu;
    public GameObject helpMenu;
    
    void Start()
    {
        creditsMenu.SetActive(false);
        helpMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    
    public void NewGame()
    {
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("CurrentScene");
        PlayerPrefs.Save();

        SceneManager.LoadScene("LevelForest");
    }
    
    public void Continue()
    {
        string currentScene = PlayerPrefs.GetString("CurrentScene", "LevelForest");
        Debug.Log("Curr-Scene: " + currentScene);
        creditsMenu.SetActive(false);
        SceneManager.LoadScene(currentScene);
        
    }
    
    public void ToMainMenu()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
        helpMenu.SetActive(false);
    }
    
    public void Help() 
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }
    
    public void ShowCredits()
    {
        //helpMenu.SetActive(false);
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }
    
}

