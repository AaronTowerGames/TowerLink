using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notice : MonoBehaviour
{

    public static Action OnHideNotice;

    [SerializeField]
    private TMP_Text noticeText;

    [SerializeField]
    private Color color;

    private float lifetime = 1.2f, deathtime = 2.5f, nowtime = 0f;
    private int stage = 0;

    public bool isShow = false;

    private string code;

    [SerializeField]
    private float
        dt = 1f, //Время цикла
        nt = 0.0f, //счётчик до времени цикла
        kf = 0.7f; //коэфициент изменения цвета

    private void Start()
    {
        color = GameData.Instance.COLORS[10];
    }
    public void SetCode(string _code)
    {
        code = _code;
    }

    public void SetText( string text)
    {
        if (String.IsNullOrEmpty(code))
        {
            noticeText.text = text;
        }
        else
        {
            noticeText.text = Translater.Instance.GetTranslate(code);
        }
        
    }

    public void SetTimeout(int sec, int secFade)
    {
        stage = 0;
        nowtime = Time.deltaTime;
        dt = sec/1000f;
        lifetime = nowtime + sec/1000f;
        deathtime = lifetime + secFade/1000f;
    }

    public void Show()
    {
        isShow = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        isShow = false;
        gameObject.SetActive(false);
        OnHideNotice?.Invoke();
    }

    private void Update()
    {
        if (isShow)
        {
            nowtime += Time.deltaTime;

            if (stage == 0)
            {
                if (nowtime > lifetime)
                {
                    stage = 1;
                }
            }
                

            if (stage == 1)
            {
                nt += Time.deltaTime;
                noticeText.color = Color.Lerp(noticeText.color, color, kf * nt / dt);
                if (nowtime > deathtime)
                {
                    stage = 3;
                }
            }

            if (stage == 3)
            {
                noticeText.color = GameData.Instance.COLORS[4];
                nt = 0;
                Hide();
            }
            
        }
        
    }
}
