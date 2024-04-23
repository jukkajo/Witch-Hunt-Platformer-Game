using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public int sceneNumber;

    private void OnTriggerEnter2D(Collider2D detected)
    {
        if (detected.CompareTag("Player"))
        {
            PlayerPrefs.DeleteKey("PlayerX");
            PlayerPrefs.DeleteKey("PlayerY");
            StartCoroutine(LoadSceneAndWait(sceneNumber));
        }
    }

    IEnumerator LoadSceneAndWait(int sceneNumber)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNumber, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        Debug.Log("Scene fully loaded, player position keys deleted.");
    }
}
