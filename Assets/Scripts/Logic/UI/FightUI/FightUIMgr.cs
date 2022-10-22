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
    //菜单键
    private Menu _menu;
    //胜利界面
    private FightWin _fightWin;
    //失败界面
    private FightLose _fightLose;

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
        if(_menu ==null )
        {
            _menu = new Menu();
        }
        if(_fightWin ==null )
        {
            _fightWin = new FightWin();
            FightWinSetActive(false);
        }
        if(_fightLose ==null )
        {
            _fightLose = new FightLose();
            FightLoseSetActive(false);
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
        _joystick = null;
    }

    public void Reset()
    {
        //这里要重置，则绑定的所有事件都要重置
        ReleaseJoystick();
        ResetJoystick();
        ResetSkillAtkDlg();
        ResetTargetHead();
        ReserRoleHead();
        Resetmenu();
        UIManager.instance.RemoveLayer(UILayer.FightUI);
        UIManager.instance.RemoveLayer(UILayer.Scene);
        //ReserRoleHead();
        //Resetmenu();
    }

    public void FightWinSetActive(bool isActive)
    {
        _fightWin.SetFightWinUIActive(isActive);
    }

    public void FightLoseSetActive(bool isActive)
    {
        _fightLose.SetFightLoseUIActive(isActive);
    }

    private void Resetmenu()
    {
        _menu = null;
    }
    private void ReserRoleHead()
    {
        _roleHead = null;
    }

    private void ResetJoystick()
    {
        if (_joystick == null)
        {
            return;
        }
        //摇杆的小球要归位
        _joystick.Reset();
        //_joystick.ResetTimer();
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
        _skillDlgAtk = null;
    }

    private void ResetTargetHead()
    {
        if (_targetHead == null)
        {
            return;
        }
        _targetHead.Hide();
        _targetHead = null;
    }

    public void SetHideImage(bool isActive,int index=0)
    {
        if(_skillDlgAtk ==null )
        {
            return;
        }
        _skillDlgAtk.HideImageSetActive(isActive, index);
    }

}

