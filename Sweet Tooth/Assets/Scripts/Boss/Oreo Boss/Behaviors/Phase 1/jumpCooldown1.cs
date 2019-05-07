using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpCooldown1 : StateMachineBehaviour
{
    [SerializeField] private OreoBossScriptableObject values;

    float counter;

    [HideInInspector] public Boss_OreoChocolateBoss boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        counter = values.jumpCooldownTime1;        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public void Jump_Cooldown_1 (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (counter <= 0)
        {
            animator.SetTrigger("chasePlayer");
            counter = values.jumpCooldownTime1;
        }

        else
        {
            counter -= Time.fixedDeltaTime;
        }
    }
}
