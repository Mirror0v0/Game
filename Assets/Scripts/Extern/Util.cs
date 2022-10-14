using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;



/// <summary>
/// 通用工具类
/// </summary>
public static class Util
{

    
    public static bool FloatEqual(float a,float b)
    {

        return Mathf.Abs(a - b) < 0.001f;//严格的相等
    }


    /// <summary>
    /// 2D游戏的距离判断方式
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float Distance2D(Vector2 a,Vector2 b)
    {
        return  Vector2.Distance(new Vector2(a.x,a.y), 
            new Vector2(b.x, b.y));
    }

    internal static void SafeActionCallback(Action exitCallBack)
    {
        throw new NotImplementedException();
    }


    //封装委托的调用
    public static void SafeCall<T>(Action<T> callback, T arg)
    {
        if (callback != null)
        {
            callback(arg);
        }
    }


    //封装委托的调用
    public static void SafeCall(Action callback)
    {
        if (callback != null)
        {
            callback();
        }
    }
}

