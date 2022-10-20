using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 普通攻击
/// </summary>
public class SkillNormalAttackLogic:SkillLogicBase
{
    protected override void InitTimeLine()
    {
        _timeLine.AddEvent(0, 0, OnSkillStart);//技能开始
        _timeLine.AddEvent(0, 20, OnAction);//播放动画
        _timeLine.AddEvent(0.2f, 0, OnNormalHit);//直接进行伤害结算
        _timeLine.AddEvent(0.833f, 20, OnActionEnd);//停止动画
        _timeLine.AddEvent(0.833f, 0, OnSkillEnd);//技能结束                                    
    }

    //伤害结算，目前不需要参数
    private void OnNormalHit(int __null)
    {
        if (_target == null)
        {
            Debug.Log("普通攻击未找到目标");
            return;
        }
        OnHitSomething(_target);
    }

    private void OnHitSomething(Creature target)
    {
        //造成伤害（伤害计算）
        DamageMgr.instance.Damage(_caster, target);
        FightUIMgr.instance.SetHideImage(true);
    }
}

