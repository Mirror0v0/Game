using System;

using UnityEngine;




/// <summary>
/// 技能逻辑基类
/// </summary>
public abstract class SkillLogicBase
{

    public SkillDatabase TableData;
    protected Creature _caster;
    protected Creature _target;
    protected TimeLine _timeLine = new TimeLine();
    protected Action _skillEndCallback;
    public void Init(Creature caster,SkillDatabase tableData)//将技能表数据在初始化的时候作为参数传入,因为技能里面要用
    {
        _caster = caster;
        this.TableData = tableData;
        //初始化时间线
        InitTimeLine();
    }

    protected virtual void InitTimeLine()//每个技能的初始化都不同，父类中其实不用写时间线的初始化内容（其实完全可以用抽象方法）
    {
        Debug.LogError("子类没有初始化时间线：" + GetType().ToString());//获取到当前对象的类型并打印出来
    }


    /// <summary>
    /// 开始施放技能
    /// </summary>
    public void Start(Creature target, Action skillEndCallback)
    {
        _target = target;
        _skillEndCallback = skillEndCallback;
        _timeLine.Start();
    }


    public void Loop()
    {
        _timeLine.Loop(Time.deltaTime);
    }


    //技能开始
    protected virtual void OnSkillStart(int __null)
    {
        Debug.Log("OnSkillStart");
        //施法者(是否要)朝向面向目标
        if (_target != null)
        {
            //_caster.LookAt(_target);
        }
    }

    protected virtual void OnSkillEnd(int __null)
    {
        Debug.Log("OnSkillEnd");
        //暂时就是先清空目标
        _target = null;
        //通知外界技能结束
        if (_skillEndCallback != null)
        {
            _skillEndCallback();
        }
    }


    //播放结束动画
    protected virtual void OnActionEnd(int actionID)
    {
        Debug.Log("OnActionEnd");
        if (_caster.GetAnim() == actionID)
        {
            _caster.SetAni(1);
        }
    }

    //播放相应动画
    protected virtual void OnAction(int actionID)
    {
        Debug.Log("OnAction");
        _caster.SetAni(actionID);
    }



}