using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashCooldown2 : StateMachineBehaviour
{

    [SerializeField] private OreoBossScriptableObject values;

    private float counter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        counter = values.dashCoolDown2;

        FindObjectOfType<Boss_OreoChocolateBoss>().Spread_Shot();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (counter <= 0)
        {
            if (values.noOfDashes >= values.dashCount2 - 1)
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
                Debug.Log(values.noOfDashes);
            }

            counter = values.dashCoolDown2;
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
