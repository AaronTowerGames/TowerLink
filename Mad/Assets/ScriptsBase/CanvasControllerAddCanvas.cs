using UnityEngine;

public class CanvasControllerAddCanvas : MonoBehaviour
{
    [SerializeField]
    private string canvasname = "";
    [SerializeField]
    private Canvas thiscanvas = null;

    void Start()
    {
        TryGetComponent<Canvas>(out Canvas canvas);
        if (canvas == null)
        {
            return;
        }

        thiscanvas = canvas;
        canvasname = canvas.name;


        if (canvasname != "")
        {
            EventBus.AddCanvas.Invoke(canvas);
        }

        //thiscanvas.enabled = false;
    }
}
