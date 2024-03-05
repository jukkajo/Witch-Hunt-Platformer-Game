using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    private bool _playerEnter = false;

    private bool PlayerEnter
    {
        get { return _playerEnter; }

        set
        {
            _playerEnter = value;
            animator.SetBool(AnimationStrings.playerEnter, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !PlayerEnter)
        {
            PlayerEnter = true;
            
            // For continue option in main menu
            PlayerPrefs.SetFloat("PlayerX", transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", transform.position.y);
            PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
            
            StartCoroutine(WaitAndDestroy());
        }
    }

    private System.Collections.IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }
}

