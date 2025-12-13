using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlaySingleSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private UnityEvent onPlayEnd;


    public void PlaySound()
    {
        audioSource.Play();

        StartCoroutine(wait());

        IEnumerator wait()
        {
            while (audioSource.isPlaying) yield return null;

            onPlayEnd?.Invoke();
        }
    }


    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;

        audioSource.Play();

        StartCoroutine(wait());

        IEnumerator wait()
        {
            while (audioSource.isPlaying) yield return null;

            onPlayEnd?.Invoke();
        }
    }
}
