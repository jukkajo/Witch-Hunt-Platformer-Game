using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeObjectBehavior : StateMachineBehaviour
{
    public float fadingTime = 0.75f;
    private float elapsedTime = 0f;
    public float fadingDelay = 0f;
    private float elapsedFadingDelay = 0.0f;

    GameObject removeTarget;
    Color spriteColorStart;
    
    SpriteRenderer spriteRenderer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        elapsedTime = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        removeTarget = animator.gameObject;
        spriteColorStart = spriteRenderer.color;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Debug.Log("Entered");
       if (fadingDelay > elapsedFadingDelay) {
           elapsedFadingDelay += Time.deltaTime;
       } else {    
           elapsedTime += Time.deltaTime;
           float updateAlpha = spriteColorStart.a * (1 - elapsedTime / fadingTime);
       
           spriteRenderer.color = new Color(spriteColorStart.r, spriteColorStart.g, spriteColorStart.b, updateAlpha);
           Debug.Log(elapsedTime + "  and  " + fadingTime);
           if (elapsedTime > fadingTime) {
               Destroy(removeTarget);
               
               if (removeTarget.GetComponent<DamageElement>())
               {
                   string removableHealthBarName = removeTarget.GetComponent<DamageElement>().healthBarName;
                   GameObject healthBar = GameObject.FindWithTag(removableHealthBarName);
                   Destroy(healthBar);
               }

           }   
       }
    }

}
