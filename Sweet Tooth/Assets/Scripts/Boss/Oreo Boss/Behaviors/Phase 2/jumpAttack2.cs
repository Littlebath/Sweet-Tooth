using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpAttack2 : StateMachineBehaviour
{
    [SerializeField] private OreoBossScriptableObject values;

    Vector3 playerPos;
    Vector3 origin;
    float animation;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = FindObjectOfType<PlayerController>().transform.position;
        origin = animator.transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public void Jump_Attack_2 (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(animator.transform.position, playerPos) <= 0.1f)
        {
            animator.SetTrigger("jumpCooldown");
        }

        else
        {
            animation += Time.fixedDeltaTime;
            animation = animation % 5;
            Vector3 tempPos = MathParabola.Parabola(origin, playerPos, 1f, animation / 1f);
            animator.GetComponent<Rigidbody2D>().MovePosition(tempPos);
        }
    }
}
