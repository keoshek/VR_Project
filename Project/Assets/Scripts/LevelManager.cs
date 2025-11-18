using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Utilities;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelAction[] levelActions;


    private int actionIndex;


    private void Start()
    {
        InvokeActionAtIndex(actionIndex);
    }


    public void NextAction()
    {
        int newIndex = ++actionIndex % levelActions.Length;

        if (newIndex >= actionIndex)
        {
            levelActions[actionIndex].InvokeAction();
        }
    }


    private void InvokeActionAtIndex(int value)
    {
        actionIndex = value;

        levelActions[value].InvokeAction();
    }
}


[Serializable]
public class LevelAction
{
    [TextArea(1, 5)] public string Description;

    public UnityEvent Event;

    public void InvokeAction()
    {
        Event?.Invoke();
    }
}
