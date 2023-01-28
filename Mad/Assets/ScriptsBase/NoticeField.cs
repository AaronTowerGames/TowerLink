using System.Collections.Generic;
using UnityEngine;

public class NoticeField : MonoBehaviour
{
    [SerializeField]
    private Transform parentForNotices;

    [SerializeField]
    private Notice prefabNotice;

    [SerializeField]
    private Queue<Notice> notices = new Queue<Notice>();

    private void OnEnable()
    {
        Notice.OnHideNotice += CheckNotices;
    }

    private void OnDisable()
    {
        Notice.OnHideNotice -= CheckNotices;
    }

    public void Show()
    {
        EventBus.PlaySound.Invoke("subdropdeep");
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        //gameObject.SetActive(false);
    }

    public void CreateNotice(string text, int sec = 1000, int secFade = 2500)
    {
        if (notices.Count == 0 || notices.Peek().isShow)
        {
            Notice notice = Instantiate(prefabNotice, Vector3.zero, Quaternion.identity, parentForNotices);
            notice.SetText(text);
            notice.SetTimeout(sec, secFade);
            notice.Show();
            notices.Enqueue(notice);
            //Debug.Log("F");
        }
        else
        {
            var notice = notices.Peek();
            notice.SetText(text);
            notice.SetTimeout(sec, secFade);
            notice.Show();
            notices.Dequeue();
            notices.Enqueue(notice);
            //Debug.Log("S");
        }
    }

    public void CreateNotice(string text, string _code, int sec = 1000, int secFade = 2500)
    {
        if (notices.Count == 0 || notices.Peek().isShow)
        {
            Notice notice = Instantiate(prefabNotice, Vector3.zero, Quaternion.identity, parentForNotices);
            notice.SetCode(_code);
            notice.SetText(text);
            notice.SetTimeout(sec, secFade);
            notice.Show();
            notices.Enqueue(notice);
            //Debug.Log("F2");
        }
        else
        {
            var notice = notices.Peek();
            notice.SetCode(_code);
            notice.SetText(text);
            notice.SetTimeout(sec, secFade);
            notice.Show();
            notices.Dequeue();
            notices.Enqueue(notice);
            //Debug.Log("S2");
        }
    }

    private void CheckNotices()
    {
        if (notices.Peek().isShow)
        {
            //Debug.Log("CN");
        }
        else
        {
            Hide();
            //Debug.Log("C");
        }
    }
}
