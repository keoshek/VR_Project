using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool _isSimulationRoom;
    [SerializeField] private bool autoStart;
    [SerializeField] private int startIndex;
    [SerializeField] private LevelAction[] levelActions;
    [SerializeField] private Tablet tablet;


    public bool IsSimulationRoom => _isSimulationRoom;


    private int actionIndex;


    private void Start()
    {
        if (autoStart) InvokeActionAtIndex(startIndex);

        if (tablet != null) tablet.Initialize(this);
    }


    public void NextAction()
    {
        int newIndex = ++actionIndex % levelActions.Length;

        if (newIndex >= actionIndex)
        {
            LevelAction action = levelActions[actionIndex];
            action.Event?.Invoke();
        }
    }


    public void InvokeActionAtIndex(int value)
    {
        actionIndex = value;

        LevelAction action = levelActions[actionIndex];
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


    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}


[Serializable]
public class LevelAction
{
    [TextArea(1, 5)] public string Description;
    public UnityEvent Event;
}
