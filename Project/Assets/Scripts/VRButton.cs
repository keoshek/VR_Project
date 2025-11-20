using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : MonoBehaviour
{
    [SerializeField] private float deadTime = 1;
    [SerializeField] private UnityEvent onPressed, onReleased;
    [SerializeField] private bool functions;
    [SerializeField] private AudioSource onPressAudioSource;


    private bool deadTimeActive;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button") && !deadTimeActive)
        {
            if (functions) onPressed?.Invoke();
            Debug.Log("Been Pressed.");
            onPressAudioSource.Play();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Button") && !deadTimeActive)
        {
            if (functions) onReleased?.Invoke();
            Debug.Log("Been Released.");
            StartCoroutine(WaitForDeadTime());
        }
    }


    IEnumerator WaitForDeadTime()
    {
        deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        deadTimeActive = false;
    }


    public void SetFunctionality(bool value)
    {
        functions = value;
    }
}
