using UnityEngine;

public class EnemyTeleport : MonoBehaviour
{
    public GameObject[] enemies; // Reference to the mummy GameObjects
    public Vector3 teleportPosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the player has entered the trigger
        {
            TeleportEnemies();
        }
    }

    void TeleportEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.transform.position = teleportPosition;
        }
    }
}
