using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingToPlayer : StateMachineBehaviour
{
    [SerializeField] private ChocolateBossScriptableObject values;
    private Vector3 playerPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = FindObjectOfType<PlayerController>().transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 tempPos = Vector3.MoveTowards(animator.transform.position, playerPos, values.chargeSpeed * Time.deltaTime);
        animator.GetComponent<Rigidbody2D>().MovePosition(tempPos);

        if (Vector3.Distance (animator.transform.position, playerPos) <= 0.2f)
        {
            animator.SetTrigger("dashCool");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
