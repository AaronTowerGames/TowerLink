using System;

[Serializable]
public class TranslateData
{
    public string code;
    public string language;
    public string text;
}

public class Translater : GSC<Translater>
{
    private bool IsNeedDownload { get; set; }

    private string FILENAME => "Translates_" + DataSettings.LANGUAGE.ToUpper() + ".json";

    const string TRANSLATES_JSON = "{\"Items\":[{\"code\":\"loading\",\"language\":\"ru\",\"text\":\"\u0417\u0430\u0433\u0440\u0443\u0437\u043a\u0430...\"},{\"code\":\"enter\",\"language\":\"ru\",\"text\":\"\u0412\u043e\u0439\u0442\u0438\"}]}";

    private TranslateData[] data;


    public string GetTranslate(string _code, string defaultText = "")
    {
        //Debug.Log("GET TRANSLATE");
        if (_code != null)
        {
            //Debug.Log("CODE NOT NULL: " + _code);
            if (data != null)
            {
                //Debug.Log("TRANSLATE DATA NOT NULL");
                bool find = false;
                foreach (var translate in data)
                {
                    //Debug.Log("SEARCH CODE: " + _code.ToLower() + " TRANSLATE CODE: " + translate.code.ToLower());
                    if (translate.code.ToLower() == _code.ToLower())
                    {
                        find = true;
                        //Debug.Log("FIND CODE: " + _code + " TRANSLATE LANG: " + translate.language + " GAME LANG: " + DataSettings.language);
                        if (translate.language.ToUpper() == DataSettings.LANGUAGE.ToUpper())
                        {
                            //Debug.Log("FIND TEXT: " + translate.text);
                            return translate.text;
                        }
                    }
                }

                if (find)
                {
                    var _data = FileController2.LoadJsonsData<TranslateData>("Translates_EN.json");

                    foreach (var translate in _data)
                    {
                        if (translate.code == _code)
                        {
                            return translate.text;
                        }
                    }

                }
                else
                {
                    return defaultText;
                }
            }
            else
            {
                return defaultText;
            }
        }

        return null;
    }

    private void LoadTranslatesData()
    {
        TranslateData[] _data;
        IsNeedDownload = true; 
        if (IsNeedDownload)
        {
            _data = null;
        }
        else
        {
            _data = FileController2.LoadJsonsData<TranslateData>(FILENAME);
        }

        if (_data != null)
        {
            DatasLoaded(_data);
            return;
        }
        
        _data = JsonHelper.FromJson<TranslateData>(TRANSLATES_JSON);
        DatasLoaded(_data);
    }

    private void DatasLoaded(TranslateData[] _data)
    {        
        if (_data != null)
        {
            FileController2.SaveJsonsData(_data, FILENAME);

            data = _data;
            EventBus.OnLoadedTranslates.Invoke();
        }
        else
        {
            Notification.Instance.ShowNotice("TEXTS HAS NOT BEEN LOADED");
            //LoadTranslatesData(); //TODO: цикл без выхода
        }
    }

    private void OnEnable()
    {
        EventBus.LoadTranslates.Subscribe(LoadTranslatesData);
    }

    private void OnDisable()
    {
        EventBus.LoadTranslates.Unsubscribe(LoadTranslatesData);
    }
}
