using UnityEngine;
using UnityEngine.Events;

public class MRIRemoteController : MonoBehaviour
{
    [SerializeField] private bool controlEnabled;
    [SerializeField] private UnityEvent action;


    public void EnableControl(bool value)
    {
        controlEnabled = value;
    }


    public void DoFunction()
    {
        if (controlEnabled)
        {
            action?.Invoke();
        }
    }
}
