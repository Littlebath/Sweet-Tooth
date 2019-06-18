using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : StateMachineBehaviour
{
    private Enemy_Invulnerable stats;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        stats = animator.GetComponent<Enemy_Invulnerable>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(Vector3.Distance(animator.gameObject.transform.position, stats.points[stats.pointIndicator].position));

        if (Vector3.Distance(animator.gameObject.transform.position, stats.points[stats.pointIndicator].position) <= 0.2f)
        {
            //Switch targets
            if (stats.pointIndicator < stats.points.Length - 1)
            {
                stats.pointIndicator += 1;
            }

            else
            {
                stats.pointIndicator = 0;
            }
        }

        else
        {
            //Patrolling
            Vector3 tempPos = Vector3.MoveTowards(animator.gameObject.transform.position, stats.points[stats.pointIndicator].position, stats.moveSpeed);
            animator.GetComponent<Rigidbody2D>().MovePosition(tempPos);
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
