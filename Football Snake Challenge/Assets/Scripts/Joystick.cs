using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private GameObject _handle;

    [SerializeField] float MoveRadius;

    [SerializeField] private ShakeControl shakeControl;

    public Vector2 Position
    {
        get
        {
            return (_handle.transform.position - transform.position).normalized;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 inputPosition = Camera.main.ScreenToWorldPoint(eventData.position);

        Vector3 offset = inputPosition - gameObject.transform.position;

        offset = new Vector3(offset.x, offset.y, 0);

        _handle.gameObject.transform.position = gameObject.transform.position + Vector3.ClampMagnitude(offset, MoveRadius);

        shakeControl.MoveShake((_handle.transform.position - transform.position).normalized);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _handle.transform.localPosition = Vector3.zero;
    }
}
