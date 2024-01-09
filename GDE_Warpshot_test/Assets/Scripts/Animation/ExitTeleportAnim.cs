using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTeleportAnim : StateMachineBehaviour
{
    private GameObject player;
    private Rigidbody2D playerRb;
    private PlayerController playerController;
    private AudioSource audioSource;
    public AudioClip teleportExitSound;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isTeleporting", false);
        animator.SetBool("reachedDestination", false);
        player = GameObject.Find("Player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    // }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerRb = player.GetComponent<Rigidbody2D>();
        audioSource = player.GetComponent<AudioSource>();

        // Removes player constraints
        playerRb.isKinematic = false;
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;

        playerController.Teleporting = false;
        audioSource.PlayOneShot(teleportExitSound);

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
