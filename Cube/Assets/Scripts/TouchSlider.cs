using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TouchSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction OnPointerDownEvent;
    public UnityAction<float> OnPointerDragEvent;
    public UnityAction OnPointerUpEvent;

    private Slider _uiSlider;

    private void Awake()
    {
        _uiSlider = GetComponent<Slider>();
        _uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnPointerDownEvent != null)
        {
            OnPointerDownEvent.Invoke();
        }

        if (OnPointerDragEvent != null)
        {
            OnPointerDragEvent.Invoke(_uiSlider.value);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        if (OnPointerDragEvent != null)
        {
            OnPointerDragEvent.Invoke(value);
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnPointerUpEvent != null)
        {
            OnPointerUpEvent.Invoke();
        }

        _uiSlider.value = 0f;
    }

    private void OnDestroy()
    {
        _uiSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
