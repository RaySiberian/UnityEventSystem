using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Planet : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;

    private event Action OnPlanetClick;

    public void Setup(Sprite sprite, Action onClickCallback)
    {
        image.transform.localPosition = Vector3.zero;
        image.color = Color.white;
        image.sprite = sprite;

        OnPlanetClick = onClickCallback;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnPlanetClick?.Invoke();
        image.DOColor(Color.clear, 0.3f);
        image.raycastTarget = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.transform.DOScale(Vector3.one * 1.1f, 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.transform.DOScale(Vector3.one, 0.3f);
    }
}