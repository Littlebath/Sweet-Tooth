using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeUpDuration : StateMachineBehaviour
{
    private float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = animator.GetComponent<Enemy_Invulnerable>().timeTillCharge;
        animator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(timer);

        if (timer <= 0)
        {
            animator.SetTrigger("charge");
   
            animator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        else
        {
            timer -= Time.deltaTime;
            animator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = animator.GetComponent<Enemy_Invulnerable>().timeTillCharge;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
