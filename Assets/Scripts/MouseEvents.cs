using System;
using DG.Tweening;
using UnityEngine;

public class MouseEvents : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private Vector3 offset;

    private void OnMouseEnter()
    {
        transform.DOScale(Vector3.one * 1.2f, 0.3f);
    }

    private void OnMouseOver()
    {
        transform.Rotate(Time.deltaTime * 45f * Vector3.forward);
    }

    private void OnMouseExit()
    {
        transform.DOScale(Vector3.one, 0.3f);
    }

    private void OnMouseDown()
    {
        var pos = camera.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        offset = transform.position - pos;

        transform.DOScale(Vector3.one * 1.5f, 0.3f);
    }

    private void OnMouseDrag()
    {
        var pos = camera.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        pos += offset;
        transform.position = pos;
    }

    private void OnMouseUp()
    {
        transform.DOScale(Vector3.one * 1.3f, 0.3f);
    }

    private void OnMouseUpAsButton()
    {
        print("Click");
    }
}
