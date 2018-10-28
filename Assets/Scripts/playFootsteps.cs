using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playFootsteps : StateMachineBehaviour {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            animator.gameObject.GetComponent<AudioSource>().Play();
        }
        
    }
}
