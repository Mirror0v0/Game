using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 模拟服务器数据基类
/// </summary>
public class Cmd 
{
   
}

//动态数据
public class CreateSceneCreature:Cmd
{
    public int attack;//攻击力

    public int defend;//防御力

    public int hp;//血量

    public int maxHp;//最大血量
}
