using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaltuzaFollowBehavior : StateMachineBehaviour
{
    private Transform playerPos;
    public float speed;

    // Start
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);
        Debug.Log((playerPos.position - animator.transform.position).normalized);
        Vector2 _vector = (playerPos.position - animator.transform.position).normalized;
        animator.GetComponent<Rigidbody2D>().velocity = _vector * speed;

        //if (Input.GetKeyDown(KeyCode.Space))
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isFollowing", false);
            animator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetBool("isPatrolling", true);
        }
    }

    //Stops
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
