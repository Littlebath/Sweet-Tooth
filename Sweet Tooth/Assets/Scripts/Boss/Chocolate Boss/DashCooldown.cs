using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCooldown : StateMachineBehaviour
{
    [SerializeField] private ChocolateBossScriptableObject values;

    private float cooldownCounter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cooldownCounter = values.coolDownTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (cooldownCounter <= 0)
        {
            animator.SetTrigger("followPlayer");
            cooldownCounter = values.coolDownTime;
        }

        else
        {
            cooldownCounter -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
