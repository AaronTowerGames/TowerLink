using System;
using UnityEngine;

[Serializable]
public class NotificationData
{
    public string code;
}

public class Notification : GSC<Notification>
{
    [SerializeField]
    private GameObject prefabNoticeField;

    private NoticeField noticeField = null;

    private void CreateField()
    {
        noticeField = FindObjectOfType<NoticeField>();
        if (noticeField == null)
        {
            var cams = FindObjectsOfType<Camera>();
            foreach (Camera cam in cams)
            {
                if (cam.enabled)
                {

                    GameObject noticeFieldObject = (GameObject)Instantiate(prefabNoticeField, Vector3.zero, Quaternion.identity, cam.transform);
                    noticeField = noticeFieldObject.GetComponent<NoticeField>();
                }
                break;
            }
        }        
    }

    public void ShowNotice(string text, int sec = 1000, int secFade = 2500)
    {
        if (noticeField == null)
        {
            CreateField();
        }

        noticeField.Show();

        noticeField.CreateNotice(text, sec, secFade);
    }

    public void ShowNotice(string text, string _code, int sec = 1000, int secFade = 2500)
    {
        if (noticeField == null)
        {
            CreateField();
        }
        noticeField.CreateNotice(text, _code, sec, secFade);
        noticeField.Show();
    }
}
