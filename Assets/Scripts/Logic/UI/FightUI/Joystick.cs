using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 摇杆
/// </summary>
public class Joystick
{
    public Action<Vector2> OnMoveDir;
    public Action OnMoveEnd;
    private GameObject _root;
    private RectTransform _innerBall;
    private RectTransform _outBall;
    private float _radius = 0;
    private Vector2 _centerPos;
    private Vector2 _dir;

    public Joystick()
    {
        _root = UIManager.instance.Add("UI/FightUI/Joystick",UILayer.FightUI);
        _outBall = _root.Find<RectTransform >("bgImage");
        _innerBall = _root.Find<RectTransform >("bgImage/innerImage");
        _radius = _outBall.rect.width * 0.5f;
        _centerPos = new Vector2(0, 0);
        var touchEx = _outBall.GetComponent<TouchEX>();
        touchEx.DragCallback = OnDrag;
        touchEx.PointDownCallback = OnPointDown;
        touchEx.PointUpCallback = OnPointUp;

        TimerMgr.instance.CreateTimerAndStart(0.02f, -1, OnLoop);
    }

    public void Reset()
    {
        //松手的时候方向应该恢复到原点位置
        _dir = Vector2.zero;
        _innerBall.localPosition = _centerPos;//（位置重置）
    }

    private void OnLoop()
    {
        if(_dir==Vector2 .zero)
        {
            return;
        }      
        if (OnMoveDir != null)
        {
            OnMoveDir(_dir);
        }
    }


    private void OnPointUp(PointerEventData eventData)
    {
        //先重置状态
        Reset();
        if (OnMoveEnd != null)
        {
            OnMoveEnd();
        }
    }

    private void OnPointDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    private void OnDrag(PointerEventData eventData)
    {
        Vector2 targetPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_outBall, eventData.position, eventData.pressEventCamera, out targetPos);
        var dir = targetPos - _centerPos;
        _dir = dir.normalized;
        _innerBall.localPosition = _dir * Mathf.Min(dir.magnitude, _radius);

        //if(OnMoveDir !=null )
        //{
        //    OnMoveDir(_dir);
        //}
    }
}

