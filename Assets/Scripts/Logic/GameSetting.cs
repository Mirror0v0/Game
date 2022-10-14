using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;



//游戏中通用的规则都有其控制(可以读表，这里一个静态的类就可以)
public static class GameSetting
{

    public const float stopDistance = 0.1f;
    public static int MainRoleLayer;

    static GameSetting ()
    {
        MainRoleLayer = LayerMask.NameToLayer("MainRole");
    }

    public const float MaxVisualSenseDis = 10f;

    public const float MaxAutoSelectDis = 10f;

    public const int MaxSkillNum = 3;
}

