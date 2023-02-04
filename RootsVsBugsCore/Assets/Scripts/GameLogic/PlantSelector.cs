using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlantSelector : MonoBehaviour, IPointerClickHandler
{
    public Action<PlantSelector> OnClicked;
    public Plant plantPrefab;

    private bool _selected = false;
    public UnityEvent<bool> OnSelected;
    public bool Selected { get => _selected; set { _selected = value; OnSelected?.Invoke(value); } }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked?.Invoke(this);
    }
}
