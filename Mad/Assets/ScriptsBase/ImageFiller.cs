using UnityEngine;
using UnityEngine.UI;

public class ImageFiller : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    public void SetFill(float amount)
    {
        _image.fillAmount = amount;
    }
}
