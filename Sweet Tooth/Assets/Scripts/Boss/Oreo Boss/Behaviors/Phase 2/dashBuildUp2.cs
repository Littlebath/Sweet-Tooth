﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashBuildUp2 : StateMachineBehaviour
{
    [SerializeField] private OreoBossScriptableObject values;

    private float counter;

    [HideInInspector] public Boss_OreoChocolateBoss boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        counter = values.dashBuildUpTime2;
        FindObjectOfType<Boss_OreoChocolateBoss>().indicator.SetActive(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FindObjectOfType<Boss_OreoChocolateBoss>().indicator.SetActive(false);
    }

    public void Dash_Build_2 (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (counter <= 0)
        {
            animator.SetTrigger("dashAttack");
            FindObjectOfType<Boss_OreoChocolateBoss>().indicator.SetActive(false);
            counter = values.dashBuildUpTime2;
        }

        else
        {
            counter -= Time.fixedDeltaTime;
        }
    }

}
