using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaltuzaPatrollingBehavior : StateMachineBehaviour
{
    private TaltuzaPatrolSpots patrol;
    public float speed;
    public int patrolNumber;
    private int randomSpot;
    private Transform patrolLocation;
    private Transform playerPos;

    public GameObject taltuzaPoisonPrefab;
    private Animator poisonAnimator;
    private Transform poisonPosition;
    private GameObject temp;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrol = GameObject.Find("TaltuzaPatrolSpots" + patrolNumber.ToString()).GetComponent<TaltuzaPatrolSpots>();
        randomSpot = Random.Range(0, patrol.patrolPoints.Length);
        patrolLocation = GameObject.Find("TaltuzaPatrolSpots" + patrolNumber.ToString()).transform;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        temp = (GameObject) Instantiate(taltuzaPoisonPrefab, animator.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        poisonAnimator = temp.GetComponent<Animator>();
        poisonPosition = temp.transform;
        temp.SetActive(false);

    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (animator.GetBool("isPoisoned"))
        {
            temp.SetActive(true);
            if (Vector2.Distance(animator.transform.position, patrol.patrolPoints[randomSpot].position) > 0.2f)
            {
                //animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrol.patrolPoints[randomSpot].position, speed * Time.deltaTime);
                Vector2 _vector = (patrol.patrolPoints[randomSpot].position - animator.transform.position).normalized;
                animator.GetComponent<Rigidbody2D>().velocity = _vector * speed;

                poisonPosition.transform.position = animator.transform.position + new Vector3(0, 1, 0);

            }
            else
            {
                randomSpot = Random.Range(0, patrol.patrolPoints.Length);
            }
        } else

        {
            temp.SetActive(false);
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

            if (Vector2.Distance(playerPos.position, patrolLocation.position) < 8.0f)
            {

                animator.SetBool("isPatrolling", false);
                animator.SetBool("isFollowing", true);
            }
        }

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(temp);
    }
}

