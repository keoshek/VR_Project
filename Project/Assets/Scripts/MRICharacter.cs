using Pathfinding;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MRICharacter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AIPath aiPath;
    [SerializeField] private AIDestinationSetter destinationSetter;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform initialPosition;
    [SerializeField] private Transform bedWalkTarget;
    [SerializeField] private Transform bedLyingTarget;


    [Header("Values")]
    [SerializeField] private float animationLerpSpeed = 5;
    [SerializeField] private float walkSpeed = 1;


    [Header("Events")]
    [SerializeField] private UnityEvent OnLieToBed;


    private float _animationSpeedBlend;
    private readonly LazyAnimatorHash _animIDSpeed = new("Speed");
    private readonly LazyAnimatorHash _animIDLying = new("Lying");
    private readonly LazyAnimatorHash _animIDLocomotion = new("Locomotion");


    private void LateUpdate()
    {
        SyncAnimatorLocomotionParams();
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


    public void WalkToBed()
    {
        animator.Play(_animIDLocomotion);
        aiPath.maxSpeed = walkSpeed;
        destinationSetter.target = bedWalkTarget;

        StartCoroutine(CheckReached());

        IEnumerator CheckReached()
        {
            while (true)
            {
                if (aiPath.reachedDestination) break;

                yield return null;
            }

            ResetWalk();

            LieOnBed();
        }
    }


    private void ResetWalk()
    {
        aiPath.maxSpeed = 0;
        destinationSetter.target = null;
    }


    private void LieOnBed()
    {
        EnableCollider(false);
        animator.Play(_animIDLying);
        SyncPositionRotationToBedTarget();

        OnLieToBed?.Invoke();
    }


    public void SyncPositionRotationToBedTarget()
    {
        transform.SetPositionAndRotation(bedLyingTarget.position, bedLyingTarget.rotation);
    }


    private void EnableCollider(bool value)
    {
        characterController.enabled = value;
    }


    public void GetUpAndWalkToInitialPosition()
    {
        transform.SetPositionAndRotation(bedWalkTarget.position, bedWalkTarget.rotation);
        EnableCollider(true);

        animator.Play(_animIDLocomotion);
        aiPath.maxSpeed = walkSpeed;
        destinationSetter.target = initialPosition;

        StartCoroutine(CheckReached());

        IEnumerator CheckReached()
        {
            while (true)
            {
                if (aiPath.reachedDestination) break;

                yield return null;
            }

            ResetWalk();

            gameObject.SetActive(false);
        }
    }
}
