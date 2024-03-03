using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpMenu : MonoBehaviour
{

    public GameObject helpMenu;
    public GameObject pauseMenu;
    
    void Start()
    {
        helpMenu.SetActive(false);
    }
    
    public void ToMainMenu()
    {
        pauseMenu.SetActive(true);
        helpMenu.SetActive(false);
    }
    
    public void Help() 
    {
        pauseMenu.SetActive(false);
        helpMenu.SetActive(true);
    }
    
}

