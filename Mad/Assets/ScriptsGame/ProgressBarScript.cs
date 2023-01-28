using UnityEngine;
using UnityEngine.UI;

public class ProgressBarScript : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    private Image fill;

    public void SetMax( float max)
    {
        slider.maxValue = max;
        SetCurrent(0f);
    }

    public void SetCurrent( float current)
    {
        slider.value = current;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if (slider.value >= slider.maxValue)
        {
            slider.value = 0f;
        }
    }
}
