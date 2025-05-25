using UnityEngine;
using UnityEngine.UI;

// 버프 UI 요소
public class UI_BuffElement : MonoBehaviour
{
    Slider _slider;
    Image _image;
    Buff _buff;

    void Awake()
    {
        _slider = GetComponent<Slider>();
        _image = GetComponentInChildren<Image>();
    }

    public void Init(Buff buff)
    {
        _buff = buff;
        _image.sprite = _buff.buff.icon;
        UpdateBuffState();
        _buff.ui_BuffElement = this;
    }

    public void UpdateBuffState()
    {
        _slider.maxValue = _buff._activeTime;
    }

    public void SetSliderValue(float value)
    {
        _slider.value = value;
    }
}