using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaltuzaPatrollingBehavior : StateMachineBehaviour
{
    private TaltuzaPatrolSpots patrol;
    public float speed;
    private int randomSpot;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrol = GameObject.FindGameObjectWithTag("PatrolSpots").GetComponent<TaltuzaPatrolSpots>();
        randomSpot = Random.Range(0, patrol.patrolPoints.Length);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(animator.transform.position, patrol.patrolPoints[randomSpot].position) > 0.2f)
        {
            //animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrol.patrolPoints[randomSpot].position, speed * Time.deltaTime);
            Vector2 _vector = (patrol.patrolPoints[randomSpot].position - animator.transform.position).normalized;
            animator.GetComponent<Rigidbody2D>().velocity = _vector * speed;

        }
        else
        {
            randomSpot = Random.Range(0, patrol.patrolPoints.Length);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isPatrolling", false);
        }

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
