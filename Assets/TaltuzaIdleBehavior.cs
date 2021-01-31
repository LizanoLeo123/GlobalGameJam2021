using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaltuzaIdleBehavior : StateMachineBehaviour
{
	//public GameObject effect;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}


	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			animator.SetBool("isFollowing", true);
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//Instantiate(effect, animator.transform.position, Quaternion.identity);
	}
}
