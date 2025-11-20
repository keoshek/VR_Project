using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Utilities;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject nextButton;
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
            LevelAction action = levelActions[actionIndex];
            nextButton.SetActive(action.NextButtonVisibility);
            action.Event?.Invoke();
        }
    }


    private void InvokeActionAtIndex(int value)
    {
        actionIndex = value;

        LevelAction action = levelActions[actionIndex];
        nextButton.SetActive(action.NextButtonVisibility);
        action.Event?.Invoke();
    }


    public void QuitApplication()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}


[Serializable]
public class LevelAction
{
    [TextArea(1, 5)] public string Description;
    public bool NextButtonVisibility;
    public UnityEvent Event;
}
