using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasePlayer1 : StateMachineBehaviour
{
    [SerializeField] private OreoBossScriptableObject values;

    private Vector3 tempPos;

    private float chaseCounter;

    [HideInInspector] public Boss_OreoChocolateBoss boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chaseCounter = values.chaseDuration1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       /* if (animator.GetComponent<Rigidbody2D>().isKinematic)
        {
            if (chaseCounter <= 0)
            {
                animator.SetTrigger("startDash");
                chaseCounter = values.chaseDuration1;
            }

            else
            {
                if (Vector3.Distance(animator.transform.position, FindObjectOfType<PlayerController>().gameObject.transform.position) >= values.chaseRadius)
                {
                    tempPos = Vector3.MoveTowards(animator.transform.position, FindObjectOfType<PlayerController>().gameObject.transform.position, values.chaseSpeed1 * Time.fixedDeltaTime);
                    animator.gameObject.GetComponent<Rigidbody2D>().MovePosition(tempPos);
                    chaseCounter -= Time.fixedDeltaTime;
                }
            }

        }*/
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public void Chase_Player1 (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Chase");

        if (animator.GetComponent<Rigidbody2D>().isKinematic)
        {
            if (chaseCounter <= 0)
            {
                animator.SetTrigger("startDash");
                chaseCounter = values.chaseDuration1;
            }

            else
            {
                if (Vector3.Distance(animator.transform.position, FindObjectOfType<PlayerController>().gameObject.transform.position) >= values.chaseRadius)
                {
                    tempPos = Vector3.MoveTowards(animator.transform.position, FindObjectOfType<PlayerController>().gameObject.transform.position, values.chaseSpeed1 * Time.fixedDeltaTime);
                    animator.gameObject.GetComponent<Rigidbody2D>().MovePosition(tempPos);
                    chaseCounter -= Time.fixedDeltaTime;
                }
            }

        }
    }

}
