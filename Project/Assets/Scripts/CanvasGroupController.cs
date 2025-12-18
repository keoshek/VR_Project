using UnityEngine;
using Ata.Utils;
using System.Collections;

public class CanvasGroupController : MonoBehaviour
{
    CanvasGroup canvasGroup;
    float vel;


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void SetVisibilitySmooth(bool value)
    {
        float target = value ? 1f : 0f;
        float current = canvasGroup.alpha;

        if (target == current) return;

        StartCoroutine(Method());

        IEnumerator Method()
        {
            while (Mathf.Abs(target - current) > .1f)
            {
                current = Utilities.SmoothFloat(current, target, ref vel, .1f);
                canvasGroup.alpha = current;
                yield return null;
            }

            canvasGroup.alpha = target;
        }
    }
}
