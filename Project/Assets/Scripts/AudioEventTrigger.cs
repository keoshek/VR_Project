using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioEventSystemWithID[] eventSystems;


    private Coroutine eventListenCoroutine = null;


    public void StartEventListening(string id)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("No attached audio source");
            return;
        }

        AudioEventSystemWithID evtSys = eventSystems.Where(evt => evt.ID == id).FirstOrDefault();

        if (evtSys == null)
        {
            Debug.LogWarning("No such event with id: " + id);
            return;
        }

        if (eventListenCoroutine != null) StopCoroutine(eventListenCoroutine);
        eventListenCoroutine = StartCoroutine(EventListeningProcess());

        IEnumerator EventListeningProcess()
        {
            int index = 0;

            while (audioSource.isPlaying)
            {
                if (index < evtSys.Events.Length && audioSource.time >= evtSys.Events[index].Time)
                {
                    evtSys.Events[index].Event?.Invoke();
                    index++;
                }

                yield return null;
            }
        }
    }
}


[Serializable]
public class AudioEvent
{
    [TextArea(1, 5)] public string Description;
    public float Time;
    public UnityEvent Event;
}


[Serializable]
public class AudioEventSystemWithID
{
    public string ID;
    public AudioEvent[] Events;
}