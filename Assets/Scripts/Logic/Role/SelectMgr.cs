using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using UnityEngine;



/// <summary>
/// 角色当前的目标
/// </summary>
public class SelectMgr
{
    public Creature curTarget;

    private Role _owner;

    private Timer _timer;

    public void Init(Role owner)
    {
        _owner = owner;

        _timer = TimerMgr.instance.CreateTimerAndStart(0.3f, -1, CheckDistance);
    }

    /// <summary>
    /// 检查owner对象与目标之间的距离
    /// </summary>
    private void CheckDistance()
    {
        if(_owner ==null ||curTarget ==null )
        {
            return;
        }
        var dis = Util.Distance2D(_owner.transform.position, curTarget.transform.position);
        //如果距离较远则取消显示敌人血条
        if(dis>GameSetting .MaxVisualSenseDis)
        {
            FightUIMgr.instance.SetTargetActive (false);
        }
    }

    public void Select(Creature creature)
    {
        if (creature == curTarget)
        {
            return;
        }
        if(curTarget !=null )
        {
            curTarget.HPChangeCallback -= OnTargetHPChanged;
        }
        curTarget = creature;
        _owner.curTarget = creature;
        if (creature == null)
        {
            FightUIMgr.instance.SetTargetActive(false);
            return;
        }
        var dis = Util.Distance2D(_owner.transform.position, curTarget.transform.position);
        if (dis > GameSetting.MaxVisualSenseDis)
        {
            return;
        }
        creature.HPChangeCallback += OnTargetHPChanged;
        FightUIMgr.instance.SetTargetInfo(creature.serverData.hp, creature.serverData.maxHp);
    }

    private void OnTargetHPChanged(int curHp, int maxHp)
    {
        Debug.Log(curHp);
        Debug.Log(maxHp);
        FightUIMgr.instance.SetTargetInfo(curHp, maxHp);//这里来反映血量的变化值
    }
}

