using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class EventTriggerDelegate : MonoBehaviour
{
    [SerializeField] private EventTrigger eventTrigger;

    private void Reset()
    {
        eventTrigger = GetComponent<EventTrigger>();
    }

    private void Start()
    {
        var entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };

        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData) data); });
        
        eventTrigger.triggers.Add(entry);
    }

    private void OnPointerDownDelegate(PointerEventData data)
    {
        transform.localScale = Vector3.one * 1.2f;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Pointer over game object");
        }
    }
}