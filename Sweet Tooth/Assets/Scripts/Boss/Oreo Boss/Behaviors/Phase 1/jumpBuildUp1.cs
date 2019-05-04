﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpBuildUp1 : StateMachineBehaviour
{
    [SerializeField] private OreoBossScriptableObject values;

    private float counter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        counter = values.jumpBuildupTime1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (counter <= 0)
        {
            counter = values.jumpBuildupTime1;
            animator.SetTrigger("jumpAttack");
        }

        else
        {
            counter -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
