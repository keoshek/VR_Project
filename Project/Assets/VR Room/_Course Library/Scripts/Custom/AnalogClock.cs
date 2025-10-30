using System;
using UnityEngine;

public class AnalogClock : MonoBehaviour
{
    [SerializeField] private Transform hour;
    [SerializeField] private Transform minute;
    [SerializeField] private Transform second;
    [SerializeField] private AudioSource audioSource;


    private int prevSecond;


    private void Update()
    {
        DateTime currentTime = DateTime.Now;

        hour.localEulerAngles = new(0, currentTime.Hour * 30, 0);
        minute.localEulerAngles = new(0, currentTime.Minute * 6, 0);
        second.localEulerAngles = new(0, currentTime.Second * 6, 0);

        if (prevSecond != currentTime.Second) 
            audioSource.Play();

        prevSecond = currentTime.Second;
    }
}
