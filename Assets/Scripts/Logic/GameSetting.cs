using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;



//游戏中通用的规则都有其控制(可以读表，这里一个静态的类就可以)
public static class GameSetting
{
    //const本身就包含static的意思,常量本身就是static，没有静态不静态的意义
    //移动停止的最近距离
    public const float stopDistance = 0.1f;


    public static int MainRoleLayer;


    static GameSetting ()
    {
        MainRoleLayer = LayerMask.NameToLayer("MainRole");
    }

    //最大的视觉距离（用于控制超过这个距离，就认为看不到这个对象，用于TargetHead自动隐藏）
    //public static float MaxVisualSenseDis = 10f;
    public const float MaxVisualSenseDis = 10f;

    //视觉距离理论上来说就是选择距离（但最好分开，一个变量只管一个事情）
    public const float MaxAutoSelectDis = 10f;

    //每个角色的最大技能数量
    public const int MaxSkillNum = 5;
}

