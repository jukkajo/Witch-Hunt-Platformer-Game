using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{

    public GameObject inGameMenu;
    public bool isPaused; 
    
    void Start() {
       // inGameMenu = GetComponent<PauseMenu>();
        inGameMenu.SetActive(false);
    } 
    
    void Update() {
        if(Input.GetKeyDown(KeyCode.P)) {
            if (isPaused) {
                Resume();
            } else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        inGameMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume() {
        inGameMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToMainMenu() {
       //SceneManager.LoadScene(SceneManager.GetActiveScene().build);
    }
    
    public void Options() {
    //
    }
    
}
