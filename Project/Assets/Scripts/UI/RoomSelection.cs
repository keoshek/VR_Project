using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelection : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;


    public RectTransform RectTransform => transform as RectTransform;


    private string sceneName;
    private TabletUI source;


    public void Initialize(ScriptableRoomSelectionData data, TabletUI _source)
    {
        _icon.sprite = data.Icon;
        _name.text = data.Name;
        sceneName = data.SceneName;

        source = _source;
    }


    public void OnPress()
    {
        source.PushAreYouSure(sceneName);
    }
}
