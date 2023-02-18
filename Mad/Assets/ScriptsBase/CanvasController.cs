using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private List<Canvas> canvass = new List<Canvas>(10);

    [SerializeField]
    private HashSet<string> namess = new HashSet<string>();

    private Dictionary<string, string[]> groupWindow = new Dictionary<string, string[]>();

    private Dictionary<string, Canvas> canvasNames = new Dictionary<string, Canvas>();

    [SerializeField]
    private string currentCanvasName = string.Empty;

    [SerializeField]
    private List<string> tryshowwindows = new List<string>();
    [SerializeField]
    private List<string> tryclosewindows = new List<string>();

    [SerializeField]
    private Stack<string> currentStack = new Stack<string>();

    //private IEnumerator tryshowcoroutine = null;
    [SerializeField]
    private bool running;

    private void Awake()
    {
        groupWindow.Clear();

        groupWindow.Add("StartLoadingCanvas", new string[2] { "LoadingCanvas", "ProgressCanvas" });
        groupWindow.Add("WindowCanvas", new string[2] { "WindowEarnCanvas", "WindowLoseCanvas" });
    }

    private void Start()
    {
        running = false;
        if (canvass.Count > 0)
        {
            foreach (Canvas c in canvass)
            {
                namess.Add(c.name);
                canvasNames[c.name] = c;
            }
        }
    

        //StartCoroutine(RefreshScreen());

        currentStack.Clear();
    }

    private void PushCanvas(string canvas)
    {
        currentStack.Push(canvas);
        ////Notification.Instance.ShowNotice(currentStack.Peek());
    }

    private string PopCanvas()
    {
        if (currentStack.Count > 0)
            return currentStack.Pop();
        return string.Empty;
    }

    private string PeekCanvas()
    {
        if (currentStack.Count > 0)
            return currentStack.Peek();
        return string.Empty;
    }

    private void Show(string canvasname)
    {
        if (canvasNames.ContainsKey(canvasname))
        {
            ////Notification.Instance.ShowNotice("SHOW CANVAS SOLO: " + canvasname);
            canvasNames[canvasname].enabled = true;
            PushCanvas(canvasname);
            tryshowwindows.Remove(canvasname);
            return;
        }

        if (groupWindow.ContainsKey(canvasname))
        {
            ////Notification.Instance.ShowNotice("SHOW GROUP: " + canvasname);
            PushCanvas(currentCanvasName);
            tryshowwindows.Remove(canvasname);
            foreach (var item in groupWindow[canvasname])
            {
                if (canvasNames.ContainsKey(item))
                    canvasNames[item].enabled = true;
            }
        }
    }

    private void Close(string canvasname)
    {
        ////Notification.Instance.ShowNotice("CLOSE CANVAS: " + canvasname);
        if (canvasNames.ContainsKey(canvasname))
        {
            canvasNames[canvasname].enabled = false;
            PopCanvas();
            currentCanvasName = PeekCanvas();
            tryclosewindows.Remove(canvasname);
            ////Notification.Instance.ShowNotice("CLOSE LAYER 1 " + canvasNames[canvasname]);
            return;
        }

        if (groupWindow.ContainsKey(canvasname))
        {
            PopCanvas();
            currentCanvasName = PeekCanvas();
            tryclosewindows.Remove(canvasname);
            foreach (var item in groupWindow[canvasname])
            {
                if (canvasNames.ContainsKey(item))
                {
                    canvasNames[item].enabled = false;
                    ////Notification.Instance.ShowNotice("CLOSE GROUP " + canvasNames[item] + " | " + currentCanvasName);
                }
            }
        }

        PopCanvas();
        currentCanvasName = PeekCanvas();
        ////Notification.Instance.ShowNotice("CLOSE LAYER 2 " + currentCanvasName);
    }

    private IEnumerator TryCloseCanvas(string canvasname)
    {
        ////Notification.Instance.ShowNotice("TRY CLOSE: " + canvasname);
        running = true;
        while (tryclosewindows.Count > 0)
        {

            //foreach (var item in tryclosewindows)
            {
                var canvasn = tryclosewindows[0];

                if (canvasNames.ContainsKey(canvasn))
                {
                    tryclosewindows.Remove(canvasn);
                    ////Notification.Instance.ShowNotice("CLOSE SOLO " + canvasn);
                    Close(canvasname);
                }

                if (groupWindow.ContainsKey(canvasn))
                {
                    ////Notification.Instance.ShowNotice("CLOSE GROUP " + canvasn);
                    tryclosewindows.Remove(canvasn);
                    Close(canvasname);
                }
            }

            yield return new WaitForFixedUpdate();
        }
        if (tryshowwindows.Count > 0)
        {
            foreach (var item in tryshowwindows)
            {
                StartCoroutine(TryShowCanvas(item)); 
                break;
            }
        }
        else if (tryclosewindows.Count > 0)
        {
            foreach (var item in tryclosewindows)
            {
                StartCoroutine(TryCloseCanvas(item));
                break;
            }
        }
        else
        {
            running = false;
        }

        yield break;
    }

    private void TryClose(string canvasname)
    {
        ////Notification.Instance.ShowNotice("CLOSE LAYER " + canvasname);
        tryclosewindows.Add(canvasname);
        if (!running)
        {
            StartCoroutine(TryCloseCanvas(canvasname));
        }
    }

    private IEnumerator TryShowCanvas(string canvasname)
    {
        ////Notification.Instance.ShowNotice("WANT: " + canvasname);
        running = true;
        while (tryshowwindows.Count > 0)
        {
            //foreach (var item in tryshowwindows)
            {
                var canvasn = tryshowwindows[0];

                ////Notification.Instance.ShowNotice("TRY SHOW: " + canvasn);
                if (canvasNames.ContainsKey(canvasname))
                {
                    ////Notification.Instance.ShowNotice("SHOW SOLO: " + canvasname);
                    Show(canvasn);

                }

                if (groupWindow.ContainsKey(canvasn))
                {
                    ////Notification.Instance.ShowNotice("SHOW GROUP: " + canvasname);
                    Show(canvasn);
                }
            }

            yield return new WaitForFixedUpdate();            
        }
        if (tryclosewindows.Count > 0)
        {
            foreach (var item in tryclosewindows)
            {
                StartCoroutine(TryCloseCanvas(item));
                break;
            }
        } 
        else if (tryshowwindows.Count > 0)
        {
            foreach (var item in tryshowwindows)
            {
                StartCoroutine(TryCloseCanvas(item));
                break;
            }
        }
        else
        {
            running = false;
        }
    }

    private void TryShow(string canvasname)
    {
        ////Notification.Instance.ShowNotice($"TRY SHOW {canvasname}");
        currentCanvasName = canvasname;
        tryshowwindows.Add(canvasname);
        if(!running)
        {
            StartCoroutine(TryShowCanvas(canvasname));
        }
        
    }

    private void OnEnable()
    {
        EventBus.Show.Subscribe(TryShow);
        EventBus.Close.Subscribe(TryClose);
        EventBus.AddCanvas.Subscribe(AddCanvas);
    }

    private void OnDestroy()
    {
        EventBus.Show.Unsubscribe(TryShow);
        EventBus.Close.Unsubscribe(TryClose);
        EventBus.AddCanvas.Unsubscribe(AddCanvas);
    }

    private void ClosePopUp(string canvasname)
    {
        ////Notification.Instance.ShowNotice("CLOSE CANVAS: "+canvasname);
        
        currentCanvasName = PopCanvas();
        if (canvasNames.ContainsKey(canvasname))
        {
            canvasNames[canvasname].enabled = false;
            return;
        }

        if (groupWindow.ContainsKey(canvasname))
        {
            foreach (var item in groupWindow[canvasname])
            {
                canvasNames[item].enabled = false;
            }
        }
    }

    private void AddCanvas(Canvas obj)
    {
        if (!canvasNames.ContainsKey(obj.name))
        {
            canvass.Add(obj);
            namess.Add(obj.name);
        }
        //obj.enabled = false;
        canvasNames[obj.name] = obj;
        ////Notification.Instance.ShowNotice("CANVAS NAME 12: " + obj.name+" / "+currentCanvasName);
    }

    private void Hide(string canvasname)
    {
        if (canvasNames.ContainsKey(currentCanvasName))
        {
            ////Notification.Instance.ShowNotice("HIDE CANVAS: " + canvasname);
            canvasNames[canvasname].enabled = true;
            return;
        }

        if (groupWindow.ContainsKey(canvasname))
        {
            ////Notification.Instance.ShowNotice("HIDE GROUP: " + canvasname);
            foreach (var item in groupWindow[canvasname])
            {
                if (canvasNames.ContainsKey(item))
                    canvasNames[item].enabled = true;
            }
        }
    }
    private void HideAll()
    {
        if (canvasNames.Count > 0)
            foreach (var canvas in canvass)
            {
                canvas.enabled = false;
            }
    }

    private void ShowOnce(string canvasname)
    {
        HideAll();
        currentStack.Clear();
        PushCanvas(canvasname);
        TryShow(canvasname);
    }
}
