using Pathfinding;
using UnityEngine;

public class MRICharacter : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AIPath aiPath;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;

    [Header("Values")]
    [SerializeField] private float animationLerpSpeed = 5;


    private float _animationSpeedBlend;


    private readonly LazyAnimatorHash _animIDSpeed = new("Speed");


    private void LateUpdate()
    {
        SyncAnimatorLocomotionParams();
    }


    public void WalkToBed()
    {
        
    }


    private void SyncAnimatorLocomotionParams()
    {
        float velMag = aiPath.maxSpeed == 0 ? 0 : characterController.velocity.magnitude;

        // update animation positionPower blend
        _animationSpeedBlend = Mathf.Lerp(_animationSpeedBlend, velMag, Time.deltaTime * animationLerpSpeed);
        if (_animationSpeedBlend < 0.01f) _animationSpeedBlend = 0f;

        // update animator if using _playerTransform
        animator.SetFloat(_animIDSpeed, _animationSpeedBlend);
    }
}
