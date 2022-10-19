using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public CreateSceneCreature serverData;
    public CreatureSceneDatabase tableData;
    public Creature curTarget;
    protected Animator _animator;
    //血量变化监听<当前血量，最大血量>
    public event Action<int, int> HPChangeCallback;

    //生命体的当前状态
    protected CreatureState curState = CreatureState.Idle;

    protected SkillMgr _skillMgr = new SkillMgr();

    public Transform backPos;

    //能否放技能
    public bool isCastingSkill
    {
        get
        {
            return _skillMgr.IsCastingSkill;//技能相关都找_skillMgr
        }
    }

    //判断生命体是否还活着
    public bool IsAlive
    {
        get
        {
            return curState != CreatureState.Die;
        }
    }

    //能否放技能（或者并且没有在放技能）
    public bool CanCastSkill
    {
        get
        {
            return IsAlive && !isCastingSkill;
        }
    }

    //血量属性
    public int HP
    {
        get
        {
            return serverData.hp;
        }
        set
        {
            //保证血量要大于0
            var newHP = Mathf.Max(0, value);
            var oldHP = serverData.hp;
            serverData.hp = newHP;
            //检测死亡
            if (serverData.hp <= 0 && oldHP > 0)
            {
                OnDie();
            }

            //重生
            if (oldHP <= 0 && serverData.hp > 0)
            {

                //重生
                OnRespawn();
            }
            if (HPChangeCallback != null)
            {
                HPChangeCallback(value, serverData.maxHp);
            }
        }
    }
    virtual protected void OnRespawn()
    {
        Debug.Log("重生");

        //设置重生状态（播放重生动画）
        SetState(CreatureState.Respawn);

        TimerMgr.instance.CreateTimerAndStart(0.667f, 1, () =>
        {
            if (GetState() == CreatureState.Respawn)
            {
                SetState(CreatureState.Idle);
            }
        });
    }


    virtual protected void OnDie()//每个子类分别响应各自的死亡事件
    {
        Debug.Log("死亡");
        //停止移动

        //设置死亡状态
        SetState(CreatureState.Die);
    }

    virtual public void OnDestroy()
    {
        
    }

    virtual public void Update()
    {
        RunLoop();
    }

    public virtual void Init(CreateSceneCreature serverData,CreatureSceneDatabase tableData)
    {
        this.serverData = serverData;
        this.tableData = tableData;
        _animator = this.GetComponent<Animator>();
        _skillMgr.Init(this);
    }

    //技能的施放
    public void CastSkill(int index)
    {
        //如果不能放技能，直接返回
        if (!CanCastSkill)
        {
            return;
        }

        _skillMgr.TryCastSkill(index);
    }

    private void RunLoop()
    {
        _skillMgr.Loop();
    }

    public void SetState(CreatureState newState)
    {
        curState = newState;
        SetAni((int)curState);
    }

    public CreatureState GetState()
    {
        return curState;
    }

    public void SetAni(int motionType)
    {
        _animator.SetInteger("animation", motionType);
    }

    public int GetAnim()
    {
        return _animator.GetInteger("animation");
    }
}


//生命体的逻辑状态
public enum CreatureState
{

    None = -1,
    Idle = 1,
    Move = 10,
    NormalAttack = 20,//普通攻击
    SpecialAttack=30,//背刺
    Die = 40,
    Respawn = 50,//重生
}
