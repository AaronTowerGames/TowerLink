using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Translate : MonoBehaviour
{
    [SerializeField]
    private string code;

    private Text text;
    private TMP_Text text_PRO;

    private void Start()
    {
        text = GetComponent<Text>();
        if (text != null)
        {
            text.text = Translater.Instance.GetTranslate(code);
        }
        else
        {
            text_PRO = GetComponent<TMP_Text>();
            text_PRO.text = Translater.Instance.GetTranslate(code);
        }
    }

    private void OnEnable()
    {
        EventBus.OnSetNewLanguage.Subscribe(DoTranslate);
    }

    private void OnDisable()
    {
        EventBus.OnSetNewLanguage.Unsubscribe(DoTranslate);
    }

    private void DoTranslate()
    {
        ChangeText(GetText());
    }

    private string GetText()
    {
        return Translater.Instance.GetTranslate(code);
    }

    private void ChangeText(string _text)
    {
        if (text != null)
        {
            text.text = _text;
        }
        else
        {
            text_PRO.text = _text;
        }
    }
    
}
