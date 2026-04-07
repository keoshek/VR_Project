using Ata.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : MonoBehaviour
{
    [SerializeField] private float deadTime = 1;
    [SerializeField] private UnityEvent onPressed, onReleased;
    [SerializeField] private bool functions;
    [SerializeField] private AudioSource onPressAudioSource;

    [Header("Blink")]
    [SerializeField] private float blinkPeriod;
    [SerializeField] private Material blinkMaterial;


    private bool deadTimeActive;
    Coroutine blinkCoroutine = null;


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


    public void StartBlinking()
    {
        StopBlinking();

        blinkCoroutine = StartCoroutine(Utilities.RunAlternating(blinkPeriod, () => { EnableEmission(true); }, () => { EnableEmission(false); }));
    }


    public void StopBlinking()
    {
        if (blinkCoroutine != null) StopCoroutine(blinkCoroutine);
        EnableEmission(false);
    }


    private void EnableEmission(bool value)
    {
        if (value)
            blinkMaterial.EnableKeyword("_EMISSION");
        else
            blinkMaterial.DisableKeyword("_EMISSION");
    }
}
