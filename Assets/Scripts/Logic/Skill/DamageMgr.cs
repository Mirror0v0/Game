using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 伤害计算
/// </summary>
public class DamageMgr:Singleton<DamageMgr>
{
    public void Damage(Creature caster, Creature target)
    {
        var damage = Mathf.Max(1, caster.serverData.attack - target.serverData.defend);
        target.HP = target.HP - damage;
    }
}

