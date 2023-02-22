using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("FIRE CLICK");
        StartCoroutine(OneShot());
    }
    private IEnumerator OneShot()
    {
        EventBus.FireButtonClicked.Invoke(true);
        yield return new WaitForSeconds(DataSettings.ONE_SHOOT_DELAY);
        EventBus.FireButtonClicked.Invoke(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("FIRE DOWN");
        EventBus.FireButtonClicked.Invoke(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("FIRE EXIT");
        EventBus.FireButtonClicked.Invoke(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("FIRE UP");
        EventBus.FireButtonClicked.Invoke(false);
    }
}
