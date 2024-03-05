using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenu;
    void Start()
    {
        deathMenu.SetActive(false);
    }
    
    public void NewGame()
    {
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("CurrentScene");
        PlayerPrefs.Save();
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelForest");
    }
    
    public void Continue()
    {
        string currentScene = PlayerPrefs.GetString("CurrentScene", "LevelForest");
        Debug.Log("Curr-Scene: " + currentScene);
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentScene);
        
    }
    
    public void ReturnToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void OnPlayerDeath() {
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
    }


}
