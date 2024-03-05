using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public float flySpeed = 3f;
    int waypointNumber = 0;
    public List<Transform> travelWaypoints;
    public float waypointDetectionAccuracy = 0.1f;
    Animator animator;
    private GameObject player;
    private GameObject target=null;
    private Vector3 offset;

    void Start(){
        StartCoroutine(Flight());
	target = null;
        player = GameObject.FindWithTag("Player");
        animator = player.GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D col){
	target = col.gameObject;
	offset = target.transform.position - transform.position;
	//animator.SetBool(AnimationStrings.surfaced, true);
    }
    void OnTriggerExit2D(Collider2D col){
	target = null;
    }

    void LateUpdate(){
	if (target != null) {
		target.transform.position = transform.position+offset;
	}
    }

    private IEnumerator Flight()
    {
        while (true)
        {
            Vector2 targetPosition = travelWaypoints[waypointNumber].position;

            while (Vector2.Distance(transform.position, targetPosition) > waypointDetectionAccuracy)
            {
                MoveTowards(targetPosition);
                yield return null;
            }

            // If in acceptable distance, move to the next waypoint
            waypointNumber = (waypointNumber + 1) % travelWaypoints.Count;
            yield return null;
        }
    }

    private void MoveTowards(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        transform.Translate(direction * flySpeed * Time.deltaTime);

    }
}

