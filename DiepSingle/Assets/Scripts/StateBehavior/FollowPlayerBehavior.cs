using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerBehavior : StateMachineBehaviour
{
    public float speed;
    

    private Transform EnemyPosition;
    private Transform playerPosition;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyPosition = GameObject.FindGameObjectWithTag("Enemy").transform;
        
    }

     //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyPosition.transform.position = Vector2.MoveTowards(EnemyPosition.transform.position, playerPosition.position, speed * Time.deltaTime);
        if (Vector2.Distance(EnemyPosition.transform.position, playerPosition.position) < 1f)
        {
            animator.SetBool("Attack", true);
            
        }
        
            
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
