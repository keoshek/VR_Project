using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool autoStart;
    [SerializeField] private int startIndex;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private LevelAction[] levelActions;


    private int actionIndex;


    private void Start()
    {
        if (autoStart) InvokeActionAtIndex(startIndex);
    }


    public void NextAction()
    {
        int newIndex = ++actionIndex % levelActions.Length;

        if (newIndex >= actionIndex)
        {
            LevelAction action = levelActions[actionIndex];
            if (nextButton != null) nextButton.SetActive(action.NextButtonVisibility);
            action.Event?.Invoke();
        }
    }


    public void InvokeActionAtIndex(int value)
    {
        actionIndex = value;

        LevelAction action = levelActions[actionIndex];
        if (nextButton != null) nextButton.SetActive(action.NextButtonVisibility);
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


    public void RestartApplication()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


[Serializable]
public class LevelAction
{
    [TextArea(1, 5)] public string Description;
    public bool NextButtonVisibility;
    public UnityEvent Event;
}
