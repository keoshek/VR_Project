using UnityEngine;
using Ata.StateMachine;
using UnityEngine.UI;

public class TabletSettingsState : IState
{
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


    private readonly TabletUI source;
    private readonly string findName = "Settings";


    public TabletSettingsState(TabletUI _source)
    {
        source = _source;

        Debug.Log("initialize " + TabButton);
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

    #endregion
}
