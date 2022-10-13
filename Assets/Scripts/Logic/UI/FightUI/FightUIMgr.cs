using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FightUIMgr:Singleton<FightUIMgr>
{
    //摇杆
    private Joystick _joystick;

    public void Init()
    {
        if (_joystick == null)
        {
            _joystick = new Joystick();
        }
    }

    public void BindingJoystick(Action<Vector2> onJoystickMove, Action onJoystickMoveEnd)
    {
        if (_joystick == null)
        {
            return;
        }
        _joystick.OnMoveDir = onJoystickMove;
        _joystick.OnMoveEnd = onJoystickMoveEnd;
    }

    public void ReleaseJoystick()
    {

        if (_joystick == null)
        {
            return;
        }
        _joystick.OnMoveDir = null;
        _joystick.OnMoveEnd = null;
    }

    public void Reset()
    {
        //这里要重置，则绑定的所有事件都要重置
        ReleaseJoystick();
        ResetJoystick();
    }

    private void ResetJoystick()
    {
        if (_joystick == null)
        {
            return;
        }
        //摇杆的小球要归位
        _joystick.Reset();
    }
}

