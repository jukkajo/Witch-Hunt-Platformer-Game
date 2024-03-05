using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{

    public GameObject inGameMenu;
    public bool isPaused; 
    
    void Start()
    {
        inGameMenu.SetActive(false);
    }
    
private bool isPKeyPressed = false;

void Update() {
    if (Input.GetKeyDown(KeyCode.P) && !isPKeyPressed) {
        isPKeyPressed = true;

        if (isPaused) {
            Resume();
        } else {
            PauseGame();
        }
    }

    if (Input.GetKeyUp(KeyCode.P)) {
        isPKeyPressed = false;
    }
}

    public void PauseGame() {
        inGameMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume() {
        inGameMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Options() {
    //
    }
    
    // Shows controls
    public void Help() {
        //
    }
}
