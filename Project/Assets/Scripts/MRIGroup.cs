using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MRIGroup : MonoBehaviour
{
    [SerializeField] private ShowMessagesController messageController;
    [SerializeField] private Animator mriBedAnimator;
    [SerializeField] private UnityEvent onBedMoveEnd;


    public void MoveBed(string animationName)
    {
        mriBedAnimator.Play(animationName);

        StartCoroutine(WaitUntilFinish());

        IEnumerator WaitUntilFinish()
        {
            while (true)
            {
                yield return null;

                if (!mriBedAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName)) break;
            }

            onBedMoveEnd?.Invoke();
        }
    }
}
