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
    public override void Init(CreateSceneCreature serverData, CreatureSceneDatabase tableData)
    {
        base.Init(serverData, tableData);
        BindingControlEvent();
    }

    private void Awake()
    {
        RoleMgr.instance.CreateSceneRole(this);
    }

    public override void Update()
    {
        base.Update();
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
}

