using UnityEngine;

public class CrosshairHP : MonoBehaviour
{
    [SerializeField]
    private ImageFiller _image;

    private int _maxHP;

    private void OnEnable()
    {
        EventBus.OnSetHero.Subscribe(SetHP);
        EventBus.OnChangeHeroHP.Subscribe(ChangeHP);
    }

    private void OnDestroy()
    {
        EventBus.OnSetHero.Unsubscribe(SetHP);
        EventBus.OnChangeHeroHP.Unsubscribe(ChangeHP);
    }

    private void ChangeHP(int obj)
    {
        _image.SetFill(obj / (float)_maxHP);
    }

    private void SetHP(HeroData obj)
    {
        _maxHP = obj.hp;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            EventBus.HeroDamage.Invoke(5);
        }
    }
}
