using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCrosshairField : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private MoveCrosshair moveCrosshair;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == "ClickField")
        {
            moveCrosshair.MoveToPoint(Camera.main.ScreenToWorldPoint(eventData.pointerCurrentRaycast.screenPosition));
        }
    }
}
