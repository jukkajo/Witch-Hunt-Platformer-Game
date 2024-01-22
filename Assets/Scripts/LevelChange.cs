using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public int sceneNumber;

    private void OnTriggerEnter2D(Collider2D detected) {
        if(detected.tag == "Player") {
            SceneManager.LoadScene(sceneNumber, LoadSceneMode.Single);
        }
    } 

}
