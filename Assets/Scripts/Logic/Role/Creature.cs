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
    //Ѫ���仯����<��ǰѪ�������Ѫ��>
    public event Action<int, int> HPChangeCallback;

    //������ĵ�ǰ״̬
    protected CreatureState curState = CreatureState.Idle;

    protected SkillMgr _skillMgr = new SkillMgr();

    public Transform backPos;
    //protected Rigidbody2D rig;
    protected Collider2D collider;

    //�ܷ�ż���
    public bool isCastingSkill
    {
        get
        {
            return _skillMgr.IsCastingSkill;//������ض���_skillMgr
        }
    }

    //�ж��������Ƿ񻹻���
    public bool IsAlive
    {
        get
        {
            return curState != CreatureState.Die;
        }
    }

    //�ܷ�ż��ܣ����߲���û���ڷż��ܣ�
    public bool CanCastSkill
    {
        get
        {
            return IsAlive && !isCastingSkill;
        }
    }

    //Ѫ������
    public int HP
    {
        get
        {
            return serverData.hp;
        }
        set
        {
            //��֤Ѫ��Ҫ����0
            var newHP = Mathf.Max(0, value);
            var oldHP = serverData.hp;
            serverData.hp = newHP;
            //�������
            if (serverData.hp <= 0 && oldHP > 0)
            {
                OnDie();
            }

            //����
            if (oldHP <= 0 && serverData.hp > 0)
            {

                //����
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
        Debug.Log("����");

        //��������״̬����������������
        SetState(CreatureState.Respawn);

        TimerMgr.instance.CreateTimerAndStart(0.667f, 1, () =>
        {
            if (GetState() == CreatureState.Respawn)
            {
                SetState(CreatureState.Idle);
            }
        });
    }


    virtual protected void OnDie()//ÿ������ֱ���Ӧ���Ե������¼�
    {
        Debug.Log("����");
        //ֹͣ�ƶ�
        if(curState ==CreatureState.Die)
        {
            return;
        }
        //��������״̬
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
        //rig = this.GetComponent<Rigidbody2D>();
        collider = this.GetComponent<Collider2D>();
    }

    //���ܵ�ʩ��
    public void CastSkill(int index)
    {
        //������ܷż��ܣ�ֱ�ӷ���
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


//��������߼�״̬
public enum CreatureState
{

    None = -1,
    Idle = 1,
    Move = 10,
    NormalAttack = 20,//��ͨ����
    SpecialAttack=30,//����
    Die = 40,
    Respawn = 50,//����
}
