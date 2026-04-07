using UnityEngine;

public class LyingAnimationState : StateMachineBehaviour
{
    private CharacterWalkingLying character;
    private bool initialized;

    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!initialized) 
        {
            character = animator.GetComponent<CharacterWalkingLying>();
            initialized = true;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character.SyncPositionRotationToBedTarget();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
