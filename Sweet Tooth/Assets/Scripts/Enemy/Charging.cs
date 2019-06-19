using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charging : StateMachineBehaviour
{
    private Vector3 playerPos;
    private Vector3 dir;

    private float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = animator.GetComponent<Enemy_Invulnerable>().chargeDuration;
        playerPos = FindObjectOfType<PlayerController>().transform.position;
        dir = (playerPos - animator.transform.position).normalized;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("stopCharge");
            animator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        else
        {
            animator.GetComponent<Rigidbody2D>().velocity = dir * animator.GetComponent<Enemy_Invulnerable>().moveSpeed;
            timer -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
