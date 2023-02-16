using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Translate : MonoBehaviour
{
    [SerializeField]
    private string _code;

    [SerializeField]
    private string _default;

    private Text _text;
    private TMP_Text _text_PRO;

    private void Start()
    {
        TryGetComponent<Text>(out _text);

        if (_text != null)
        {
            _text.text = Translater.Instance.GetTranslate(_code);
        }
        else
        {
            _text_PRO = GetComponent<TMP_Text>();
            _text_PRO.text = Translater.Instance.GetTranslate(_code);
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
        return Translater.Instance.GetTranslate(_code);
    }

    private void ChangeText(string _text)
    {
        if (this._text != null)
        {
            this._text.text = _text;
        }
        else
        {
            _text_PRO.text = _text;
        }
    }
    
}
