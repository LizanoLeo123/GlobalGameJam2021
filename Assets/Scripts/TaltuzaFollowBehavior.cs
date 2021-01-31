using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaltuzaFollowBehavior : StateMachineBehaviour
{
    private Transform patrolLocation;
    private Transform playerPos;
    public float speed;
    public int patrolNumber;

    // Start
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        patrolLocation = GameObject.Find("TaltuzaPatrolSpots" + patrolNumber.ToString()).transform;
    }

    // Update
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (animator.GetBool("isPoisoned"))
        {
            animator.SetBool("isFollowing", false);
            animator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            animator.SetBool("isPatrolling", true);

        } else

        {
            //animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);
            //Debug.Log((playerPos.position - animator.transform.position).normalized);
            Vector2 _vector = (playerPos.position - animator.transform.position).normalized;
            animator.GetComponent<Rigidbody2D>().velocity = _vector * speed;
            //Debug.Log(Vector2.Distance(playerPos.position,patrolLocation.position));


            if (Vector2.Distance(playerPos.position, patrolLocation.position) > 8.0f)
            {
                animator.SetBool("isFollowing", false);
                animator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                animator.SetBool("isPatrolling", true);
            }

        }

    }

    //Stops
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
