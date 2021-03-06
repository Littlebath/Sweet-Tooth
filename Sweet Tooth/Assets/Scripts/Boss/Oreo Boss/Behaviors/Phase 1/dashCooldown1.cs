﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashCooldown1 : StateMachineBehaviour
{
    [SerializeField] private OreoBossScriptableObject values;

    private float counter;

    [HideInInspector] public Boss_OreoChocolateBoss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        counter = values.dashCoolDown1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public void Dash_Cooldown_1 (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (counter <= 0)
        {
            if (values.noOfDashes >= values.dashCount1 - 1)
            {
                //Go to jump
                animator.SetTrigger("jumpBuildUp");
                values.noOfDashes = 0;
            }

            else
            {
                //Go to follow
                animator.SetTrigger("chasePlayer");
                values.noOfDashes++;
            }

            counter = values.dashCoolDown1;
        }

        else
        {
            counter -= Time.fixedDeltaTime;
        }
    }
}
