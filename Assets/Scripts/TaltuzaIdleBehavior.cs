﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaltuzaIdleBehavior : StateMachineBehaviour
{
	//public int patrolNumber;
	private int patrolNumber;
	private Transform patrolLocation;
	private Transform playerPos;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		patrolNumber = animator.GetComponent<TaltuzaController>().patrolNumber;
		patrolLocation = GameObject.Find("TaltuzaPatrolSpots" + patrolNumber.ToString()).transform;
		playerPos = GameObject.FindGameObjectWithTag("Player").transform;

	}


	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if ((Vector2.Distance(playerPos.position, patrolLocation.position) < 8.0f) | animator.GetBool("isPoisoned"))
		{
			animator.SetBool("isFollowing", true);
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}


}
