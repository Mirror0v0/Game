using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;




/// <summary>
/// touch����չ�����ڽ��մ�����ص��¼�
/// </summary>
public class TouchEX : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    //�϶��¼�
    public Action<PointerEventData> DragCallback;


    public Action<PointerEventData> PointDownCallback;


    public Action<PointerEventData> PointUpCallback;


    public void OnDrag(PointerEventData eventData)
    {
        if(DragCallback !=null )
        {
            DragCallback(eventData);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PointDownCallback != null)
        {
            PointDownCallback(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (PointUpCallback != null)
        {
            PointUpCallback(eventData);
        }
    }
}
