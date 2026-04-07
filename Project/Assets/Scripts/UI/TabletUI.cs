using Ata.StateMachine;
using UnityEngine;
using UnityEngine.UI;

public class TabletUI : MonoBehaviour
{
    [SerializeField] private TabletStateMachine _state;
    
    [Header("References")]
    [SerializeField] private RectTransform _tabPlace;
    [SerializeField] private RectTransform _contentPlace;
    [SerializeField] private CanvasGroup _areYouSureContainer;
    [SerializeField] private Button _areYouSureYesButton;
    [SerializeField] private Button _areYouSureNoButton;
    [SerializeField] private CanvasGroup _startSimulationContainer;
    [SerializeField] private Button _startSimulationButton;


    public TabletStateMachine State => _state;
    public RectTransform TabPlace => _tabPlace;
    public RectTransform ContentPlace => _contentPlace;
    public LevelManager LevelManager { get; private set; }
    public Tablet Tablet { get; private set; }


    public void Initialize(LevelManager _levelManager, Tablet _tablet)
    {
        LevelManager = _levelManager;
        Tablet = _tablet;

        _state = new(this);
        _state.Initialize(_state.HomeState);
        RegisterClickEvents();
        PullAreYouSure();

        EnableStartSimulationPage(LevelManager.IsSimulationRoom);
    }


    public void TestDebug()
    {
        Debug.LogWarning("test");
    }


    private void RegisterClickEvents()
    {
        _areYouSureYesButton.onClick.AddListener(ClickAreYouSureYes);
        _areYouSureNoButton.onClick.AddListener(ClickAreYouSureNo);
    }


    public void EnableStartSimulationPage(bool enable)
    {
        if (enable)
        {
            _startSimulationContainer.alpha = 1;
            _startSimulationContainer.blocksRaycasts = true;

            _startSimulationButton.onClick.AddListener(() => { 
                LevelManager.InvokeActionAtIndex(0);
                EnableStartSimulationPage(false);
            });
        } 
        else
        {
            _startSimulationContainer.alpha = 0;
            _startSimulationContainer.blocksRaycasts = false;
        }
    }


    #region State

    public void EnterHomeState()
    {
        _state.TransitionTo(_state.HomeState);
    }

    public void EnterSettingsState()
    {
        _state.TransitionTo(_state.SettingsState);
    }

    public void EnterRoomsState()
    {
        _state.TransitionTo(_state.RoomsState);
    }

    #endregion


    #region Are You Sure

    public void PushAreYouSure()
    {
        _areYouSureContainer.alpha = 1;
        _areYouSureContainer.blocksRaycasts = true;
    }

    private string roomNameToLoad;
    public void PushAreYouSure(string _roomNameToLoad)
    {
        _areYouSureContainer.alpha = 1;
        _areYouSureContainer.blocksRaycasts = true;
        roomNameToLoad = _roomNameToLoad;
    }

    public void PullAreYouSure()
    {
        _areYouSureContainer.alpha = 0;
        _areYouSureContainer.blocksRaycasts = false;
    }

    private void ClickAreYouSureYes()
    {
        PullAreYouSure();

        // what to do
        if (State.IsCurrentState(State.HomeState))
        {
            switch (State.HomeState.HomeAction)
            {
                case HomeAction.Home:
                    LevelManager.LoadScene("Main");
                    break;
                case HomeAction.Reload:
                    LevelManager.ReloadCurrentScene();
                    break;
                case HomeAction.Quit:
                    LevelManager.QuitApplication();
                    break;
                default:
                    break;
            }
        }
        else if (State.IsCurrentState(State.RoomsState))
        {
            LevelManager.LoadScene(roomNameToLoad);
        }
    }

    private void ClickAreYouSureNo()
    {
        PullAreYouSure();
    }

    #endregion

}
