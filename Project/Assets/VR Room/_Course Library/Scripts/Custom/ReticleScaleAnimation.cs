using UnityEngine;

public class ReticleScaleAnimation : MonoBehaviour
{
    [SerializeField] private float maxScale = 1f;
    [SerializeField] private float minScale = .8f;
    [SerializeField] private float scaleSpeed = 1;


    private int scaleDirection = 1;


    private void Update()
    {
        float currentScale = transform.localScale.x;

        // scale action
        transform.localScale = Vector3.one * (currentScale + scaleDirection * scaleSpeed * Time.deltaTime);
        
        // is maximizing
        if (scaleDirection > 0 && currentScale > maxScale)
            scaleDirection = -1;
        // is minimizing
        else if (scaleDirection < 0 && currentScale < minScale)
            scaleDirection = 1;
    }
}
