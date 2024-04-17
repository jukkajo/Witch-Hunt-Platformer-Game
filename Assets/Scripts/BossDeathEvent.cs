using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BossDeathEvent : MonoBehaviour
{
    public GameObject boss;
    public int nextSceneNumber;

    private bool bossDead = false;

    private void Update()
    {
        if (!bossDead && boss == null)
        {
            bossDead = true;
            StartCoroutine(DeathSequence());
        }
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(3.0f);

        // Load the next scene by scene number
        SceneManager.LoadScene(nextSceneNumber, LoadSceneMode.Single);
    }
}
