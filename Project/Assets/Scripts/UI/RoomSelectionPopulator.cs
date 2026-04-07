using UnityEngine;

public class RoomSelectionPopulator : MonoBehaviour
{
    [SerializeField] private RoomSelection roomSelectionPrefab;
    [SerializeField] private RectTransform container;


    private TabletUI source;


    public void Populate(TabletUI _source)
    {
        source = _source;

        ScriptableRoomSelectionData[] _roomDatas = source.Tablet.RoomDatas;

        foreach (ScriptableRoomSelectionData data in _roomDatas)
        {
            RoomSelection instance = Instantiate(roomSelectionPrefab, container);
            instance.Initialize(data, source);

            Vector2 containerPrevSize = container.sizeDelta;
            containerPrevSize.y += instance.RectTransform.sizeDelta.y;
            container.sizeDelta = containerPrevSize;
        }
    }
}
