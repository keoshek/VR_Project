using UnityEngine;

public class Tablet : MonoBehaviour
{
    [SerializeField] private ScriptableRoomSelectionData[] _roomDatas;


    private TabletUI TabletUI => _tabletUI = GetComponentInChildren<TabletUI>();
    private TabletUI _tabletUI;
    public ScriptableRoomSelectionData[] RoomDatas => _roomDatas;
    public LevelManager LevelManager { get; private set; }


    public void Initialize(LevelManager _levelManager)
    {
        LevelManager = _levelManager;

        TabletUI.Initialize(LevelManager, this);
    }
}
