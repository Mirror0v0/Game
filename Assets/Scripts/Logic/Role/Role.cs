using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Role:Creature 
{

    public float moveSpeed = 3.0f;

    public bool isTurn = true;

    private SelectMgr _selectMgr = new SelectMgr();

    private Npc _targetNpc;

    public override void Init(CreateSceneCreature serverData, CreatureSceneDatabase tableData)
    {
        base.Init(serverData, tableData);
        _selectMgr.Init(this);
        _skillMgr.autoSelectCallback += SelectCreature;
        BindingControlEvent();
        Debug.Log("主角初始化");
    }

    private void Awake()
    {
        RoleMgr.instance.CreateSceneRole(this);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        RoleMgr.instance.roleList.Remove(this);
    }

    public override void Update()
    {
        base.Update();

        _targetNpc = _skillMgr.FindNpc() as Npc;
        if(_targetNpc !=null&&_targetNpc .HP >0 )
        {
            float enemyDis = Vector2.Distance(this.transform.position, _targetNpc.transform.position);
            Vector2 backPos = _targetNpc .backPos.transform.localToWorldMatrix.MultiplyPoint(_targetNpc.backPos.transform.localPosition);
            float backDis = Vector2.Distance(backPos, this.transform.position);
            if (backDis > enemyDis)
            {
                FightUIMgr.instance.SetHideImage(true);
                //Debug.Log("面向敌人前方");
                return;
            }
            FightUIMgr.instance.SetHideImage(false);
        }
        else
        {
            FightUIMgr.instance.SetHideImage(true);
        }
        
    }

    private void BindingControlEvent()
    {
        //绑定摇杆
        Debug.Log("摇杆事件绑定初始化");
        FightUIMgr.instance.BindingJoystick(OnJoystickMove, OnJoystickMoveEnd);
        //绑定技能事件
        FightUIMgr.instance.BindSkillBtn(OnSkill);
    }

    private void OnJoystickMoveEnd()
    {
        SetState(CreatureState.Idle);
    }

    private void OnJoystickMove(Vector2 moveDir)
    {
        if(GetState () ==CreatureState.NormalAttack)
        {
            return;
        }
        SetState(CreatureState.Move);
        if(moveDir .x<0)
        {
            isTurn = false;
        }
        else
        {
            isTurn = true;
        }
        if(!isTurn)
        {
            this.transform.localScale = new Vector3(-MathF .Abs( this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else
        {
            this.transform.localScale = new Vector3(MathF.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        this.transform.position  = this.transform.position+ new Vector3 (moveDir .x ,moveDir .y,0).normalized * Time .deltaTime * moveSpeed;
    }

    private void OnSkill(int index)
    {
        CastSkill(index);
    }

    private void SelectCreature(Creature creature)
    {
        _selectMgr.Select(creature);
    }
}

