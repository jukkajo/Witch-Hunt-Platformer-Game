using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAttackOnceBehaviour : StateMachineBehaviour
{
    public AudioClip effect;
    public float volumeLevel = 1f;
    public bool playAfterDelay = false;
    public bool playAtStateEnter = true;
    public bool playWhenExit = false;
    //public bool playContinuously = false;
    //public bool applyDelayAtStart = false;
    
    // Delayed sound timer
    public float delay = 0.40f;
    private float timePassed = 0;
    private bool delayedSoundPlayed = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if(playAtStateEnter) {
            AudioSource.PlayClipAtPoint(effect, animator.gameObject.transform.position, volumeLevel);
        }
        timePassed = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playAfterDelay && !delayedSoundPlayed) {
            timePassed += Time.deltaTime;

            if(timePassed > delay)
            {
                AudioSource.PlayClipAtPoint(effect, animator.gameObject.transform.position, volumeLevel);
                delayedSoundPlayed = true;
            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playWhenExit) {
            AudioSource.PlayClipAtPoint(effect, animator.gameObject.transform.position, volumeLevel);
        }
    }
}
