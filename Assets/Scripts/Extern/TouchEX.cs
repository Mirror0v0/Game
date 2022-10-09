using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;




/// <summary>
/// touch的扩展，用于接收触摸相关的事件
/// </summary>
public class TouchEX : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    //拖动事件
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
