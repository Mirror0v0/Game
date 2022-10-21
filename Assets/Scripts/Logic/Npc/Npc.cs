using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Npc:Creature 
{

    public Transform[] waypoints;
    private int curPoint = 0;
    [SerializeField]
    private bool _isPatrol = true;
    [SerializeField]
    private float _moveSpeed = 1f;
    private NpcAtkUI _npcAtkUI;
    private NpcWarningLineUI _npcWarningLine;
    public Transform atkArragePos;
    public Transform warningLinePos;
    private Role _targetRole;
    
    public  float curWarningValue = 0;
    public  float maxWarningValue = 100f;
    //扇形角度
    [SerializeField]
    private float _attackAngle = 60f;
    //扇形半径
    [SerializeField]
    private float _radius = 1f;
    //与主角距离（做攻击判定）
    [SerializeField]
    private float _targetDis = 1f;
    [SerializeField]
    private float _lineIncreaseValue = 1f;
    private float _lineDecreaseValue = 1f;
    public bool isTurn = false;
    private float _warningDeclineTimer = 5f;
    private float _warningDeclineTime = 0;
    private bool _warningCanDecline = true;

    public override void Init(CreateSceneCreature serverData, CreatureSceneDatabase tableData)
    {
        base.Init(serverData, tableData);
        atkArragePos = this.gameObject.Find<Transform>("atkArrangePos");
        warningLinePos = this.gameObject.Find<Transform>("warningLinePos");
        backPos = this.gameObject.Find<Transform>("back");

        _npcAtkUI = new NpcAtkUI();
        _npcAtkUI.Init(this);

        _npcWarningLine = new NpcWarningLineUI();
        _npcWarningLine.Init(this);
        _npcWarningLine.onLineFull = OnLineFull;
        Debug.Log("敌人初始化");
    }

    private void SetUIActiveFalse()
    {
        _npcAtkUI.SetActive(false);
        _npcWarningLine.SetActive(false);
    }
    private void OnLineFull()
    {
        Debug.Log("游戏失败");
    }

    private void Awake()
    {
        NpcMgr.instance.CreateSceneNpc(this);
    }

    private void Start()
    {
        //_targetRole = RoleMgr.instance.roleList[RoleMgr .instance .roleList .Count -1];//就一个主角，这里简单获取
        _targetRole = RoleMgr.instance.roleList[0];//就一个主角，这里简单获取
        //DrawTool.DrawSectorSolid(transform, transform.localPosition, 60, 3);
        //DrawTool.DrawSector(transform, transform.localPosition, 60, 3);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        _npcWarningLine = null;
        _npcAtkUI = null;
        NpcMgr.instance.allNpc.Remove(this);
    }

    protected override void OnDie()
    {
        base.OnDie();
        collider.isTrigger = true;
        SetUIActiveFalse();
    }

    public override void Update()
    {
        if (!IsAlive)
        {
            return;
        }
        base.Update();
        if(_isPatrol )
        {
            isTurn = this.transform.position.x - waypoints[curPoint].position.x < 0;
            if (!isTurn)
            {
                this.transform.localScale = new Vector3(-MathF.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            }
            else
            {
                this.transform.localScale = new Vector3(MathF.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            }
            this.transform.position = Vector2.MoveTowards(this.transform.position, waypoints[curPoint].position, _moveSpeed * Time.deltaTime);
            if(Vector2 .Distance (this.transform .position ,waypoints [curPoint ].position )<0.01f)
            {
                curPoint = (curPoint + 1) % waypoints.Length;
            }
        }
        if(IsInRange ())//在扇形攻击范围内则警戒线变化
        {
            _warningDeclineTime = 0;
            _npcWarningLine.WarningLineIncrease(_lineIncreaseValue);
        }
        else
        {
            if(_warningCanDecline)
            {
                _warningDeclineTime += Time.deltaTime;
                if (_warningDeclineTime > _warningDeclineTimer)
                {
                    _npcWarningLine.WarningLineDecrease(_lineDecreaseValue);
                }
            }
            
        }
        //_npcWarningLine.WarningLineDecline();
    }

    //是否在扇形范围内
    public bool IsInRange()
    {
        
        if(Vector2 .Distance (this.transform .position,_targetRole.transform .position )>_targetDis)
        {
            return false;
        }
        Vector2 direction =_targetRole .transform.position - this.transform .position;
        Debug.DrawLine(this.transform.position, _targetRole.transform.position,Color.red);
        Debug.DrawLine(this.transform.position, this.transform.forward, Color.green);
        //float offsetAngle = Vector2.Angle(this.transform.forward, direction);
        float offsetAngle = 0;
        if (!isTurn)
        {
            offsetAngle = Vector2.Angle(Vector2.right, direction);
        }
        else
        {
            offsetAngle = Vector2.Angle(-Vector2.right, direction);
        }
        Debug.Log("当前角度" + offsetAngle);
        return offsetAngle < _attackAngle * 0.5f ;
    }


}

