using DG.Tweening;
using UnityEngine;

public class BtnSelector : MonoBehaviour
{ 
    [SerializeField] private Transform rotationImage;
    private Sequence sequence;
    
    public void Select()
    {
        transform.localScale = Vector3.one * 1.2f;
    }

    public void Deselect()
    {
        transform.localScale = Vector3.one;
    }

    public void UpdateSelect()
    {
        rotationImage.Rotate(Time.deltaTime * 45f * Vector3.forward);
    }

    public void Move()
    {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(Vector3.one * 0.8f, 0.3f));
        sequence.Append(transform.DOScale(Vector3.one, 0.3f));
    }

    public void Submit()
    {
        print("Submit");
    }

    public void Cancel()
    {
        print("Cancel");
    }
    
}