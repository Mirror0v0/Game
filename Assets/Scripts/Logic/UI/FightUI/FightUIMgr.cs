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
    //主角小头像
    private RoleHead _roleHead;
    //敌人血条
    private TargetHead _targetHead;
    //技能按钮
    private SkillAtkDlg _skillDlgAtk;

    public void Init()
    {
        if (_joystick == null)
        {
            _joystick = new Joystick();
        }
        if(_roleHead ==null )
        {
            _roleHead = new RoleHead();
        }
        if(_targetHead ==null)
        {
            _targetHead = new TargetHead();
            _targetHead.Hide();//初始化后先隐藏
        }
        if (_skillDlgAtk == null)
        {
            _skillDlgAtk = new SkillAtkDlg();
            SetHideImage(false, 1);
            SetHideImage(true, 0);
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
        ResetSkillAtkDlg();
        ResetTargetHead();
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

    public void SetTargetInfo(int curHp, int maxHp, bool bActive = true)//要对接血条，因此要继续加参数，并且给血量数值默认参数都为-1（不要给默认值）
    {
        SetTargetActive(true);
        _targetHead.SetInfo(curHp, maxHp, bActive);
    }

    public void SetTargetActive(bool bActive)
    {
        if (bActive)
        {
            _targetHead.Show();
        }
        else
        {
            _targetHead.Hide();
        }
    }

    public void BindSkillBtn(Action<int> skillBtnCallback)
    {
        if (_skillDlgAtk == null)
        {
            return;
        }
        _skillDlgAtk.OnSkillBtnClick = skillBtnCallback;
    }

    private void ResetSkillAtkDlg()
    {
        if (_skillDlgAtk == null)
        {
            return;
        }
        _skillDlgAtk.OnSkillBtnClick = null;
    }

    private void ResetTargetHead()
    {
        if (_targetHead == null)
        {
            return;
        }
        _targetHead.Hide();
    }

    public void SetHideImage(bool isActive,int index=0)
    {
        _skillDlgAtk.HideImageSetActive(isActive, index);
    }

}

