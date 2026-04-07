using UnityEngine;
using Ata.StateMachine;
using UnityEngine.UI;

public class TabletHomeState : IState
{
    public HomeAction HomeAction => _homeAction;


    private Transform TabContainer => _tabContainer = _tabContainer != null ? _tabContainer : source.TabPlace.Find(findName);
    private Transform _tabContainer;
    private CanvasGroup TabBackgroundCanvasGroup => _tabBackgroundCanvasGroup = _tabBackgroundCanvasGroup != null ? _tabBackgroundCanvasGroup : TabContainer.Find("Background").GetComponent<CanvasGroup>();
    private CanvasGroup _tabBackgroundCanvasGroup;
    private Button TabButton
    {
        get
        {
            if (_tabButton == null)
            {
                _tabButton = TabContainer.Find("Button").GetComponent<Button>();
                _tabButton.onClick.AddListener(ClickTabButton);
            }
            return _tabButton;
        }
    }
    private Button _tabButton;


    private CanvasGroup ContentCanvasGroup => _contentCanvasGroup = _contentCanvasGroup != null ? _contentCanvasGroup : source.ContentPlace.Find(findName).GetComponent<CanvasGroup>();
    private CanvasGroup _contentCanvasGroup;
    private Button HomeButton
    {
        get
        {
            if (_homeButton == null)
            {
                _homeButton = ContentCanvasGroup.transform.GetChild(0).Find("HomeButton").GetComponent<Button>();
                _homeButton.onClick.AddListener(ClickHomeButton);
            }
            return _homeButton;
        }
    }
    private Button _homeButton;
    private Button ReloadButton
    {
        get
        {
            if (_reloadButton == null)
            {
                _reloadButton = ContentCanvasGroup.transform.GetChild(0).Find("ReloadButton").GetComponent<Button>();
                _reloadButton.onClick.AddListener(ClickReloadButton);
            }
            return _reloadButton;
        }
    }
    private Button _reloadButton;
    private Button QuitButton
    {
        get
        {
            if (_quitButton == null)
            {
                _quitButton = ContentCanvasGroup.transform.GetChild(0).Find("QuitButton").GetComponent<Button>();
                _quitButton.onClick.AddListener(ClickQuitButton);
            }
            return _quitButton;
        }
    }
    private Button _quitButton;


    private readonly TabletUI source;
    private readonly string findName = "Home";
    private HomeAction _homeAction;


    public TabletHomeState(TabletUI _source)
    {
        source = _source;

        Debug.Log("initialize " + TabButton);
        Debug.Log("initialize " + HomeButton);
        Debug.Log("initialize " + ReloadButton);
        Debug.Log("initialize " + QuitButton);
    }


    public void Enter()
    {
        TabBackgroundCanvasGroup.alpha = 0;
        ContentCanvasGroup.alpha = 1;
        ContentCanvasGroup.blocksRaycasts = true;
    }


    public void Update()
    {

    }


    public void Exit()
    {
        TabBackgroundCanvasGroup.alpha = 1;
        ContentCanvasGroup.alpha = 0;
        ContentCanvasGroup.blocksRaycasts = false;
    }


    #region Button Clicks

    private void ClickTabButton()
    {
        source.State.TransitionTo(this);
    }

    private void ClickHomeButton()
    {
        _homeAction = HomeAction.Home;
        source.PushAreYouSure();
    }

    private void ClickReloadButton()
    {
        _homeAction = HomeAction.Reload;
        source.PushAreYouSure();
    }

    private void ClickQuitButton()
    {
        _homeAction = HomeAction.Quit;
        source.PushAreYouSure();
    }

    #endregion
}


public enum HomeAction
{
    Home,
    Reload,
    Quit
}