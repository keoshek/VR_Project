using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Hover Events")]
    public UnityEvent onHover;
    public UnityEvent onUnhover;

    public void OnPointerEnter(PointerEventData eventData)
    {
        onHover?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onUnhover?.Invoke();
    }
}
