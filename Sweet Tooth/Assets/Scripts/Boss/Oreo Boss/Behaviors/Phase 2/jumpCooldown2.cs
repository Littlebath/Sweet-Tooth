using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpCooldown2 : StateMachineBehaviour
{

    [SerializeField] private OreoBossScriptableObject values;

    float counter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        counter = values.jumpCooldownTime2;
        FindObjectOfType<Boss_OreoChocolateBoss>().Boulders_Fall();
        //Start spawning objects
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (counter <= 0)
        {
            animator.SetTrigger("chasePlayer");
            counter = values.jumpCooldownTime2;
        }

        else
        {
            counter -= Time.fixedDeltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
