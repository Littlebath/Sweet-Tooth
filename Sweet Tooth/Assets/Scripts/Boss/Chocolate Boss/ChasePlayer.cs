using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : StateMachineBehaviour
{
    [SerializeField] private ChocolateBossScriptableObject values;

    private Vector3 tempPos;

    private float chaseCounter;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chaseCounter = values.chaseDuration;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (chaseCounter <= 0)
        {
            animator.SetTrigger("startDash");
            chaseCounter = values.chaseDuration;
        }

        else
        {
            if (Vector3.Distance(animator.transform.position, FindObjectOfType<PlayerController>().gameObject.transform.position) >= 1f)
            {
                Debug.Log("Start chase");
                tempPos = Vector3.MoveTowards(animator.transform.position, FindObjectOfType<PlayerController>().gameObject.transform.position, values.chaseSpeed * Time.deltaTime);
                animator.gameObject.GetComponent<Rigidbody2D>().MovePosition(tempPos);
                chaseCounter -= Time.deltaTime;
            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    } 
}
