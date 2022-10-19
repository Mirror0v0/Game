using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;





/// <summary>
/// 技能对象（）技能实体
/// </summary>
public class SkillObject
{
    //技能静态数据
    public SkillDatabase tableData;//技能所在的表的行

    //技能逻辑（动态数据）
    public SkillLogicBase logic;//技能的逻辑

}

/// <summary>
/// 技能管理
/// </summary>
public class SkillMgr
{

    private Creature _owner;

    private List<SkillObject> _allSkill = new List<SkillObject>();

    private SkillCaster _skillCaster=new SkillCaster ();//施放器

    private CastSkillAssist _skillAssit = new CastSkillAssist();

    public Action<Creature> autoSelectCallback;
    public bool IsCastingSkill
    {
        get
        {
            return _skillCaster.IsCasting;
        }
    }

    public void Init(Creature owner)
    {
        _owner = owner;

        //初始化时将技能的拥有者传进来
        _skillCaster.Init(_owner);
        _skillAssit.Init(_owner);
        //初始化技能
        //1：普攻    2:交互       3.背刺
        //测试（后面读配置表）
        SkillObject skillObj1 = new SkillObject();
        skillObj1.logic = new SkillNormalAttackLogic();
        skillObj1.tableData = new SkillDatabase();
        skillObj1.tableData.castRange = -2;
        skillObj1.tableData.cd = 2;
        skillObj1.tableData.cost = 2;
        skillObj1.tableData.damage = 2;
        skillObj1.tableData.preTime = 2;
        skillObj1.logic.Init(_owner, skillObj1.tableData);

        SkillObject skillObj2 = new SkillObject();
        skillObj2.logic = new SkillNormalAttackLogic();
        skillObj2.tableData = new SkillDatabase();
        skillObj2.tableData.castRange = -2;
        skillObj2.tableData.cd = 2;
        skillObj2.tableData.cost = 2;
        skillObj2.tableData.damage = 2;
        skillObj2.tableData.preTime = 2;
        skillObj2.logic.Init(_owner, skillObj2.tableData);

        SkillObject skillObj3 = new SkillObject();
        skillObj3.logic = new SkillNormalAttackLogic();
        skillObj3.tableData = new SkillDatabase();
        skillObj3.tableData.castRange = -2;
        skillObj3.tableData.cd = 2;
        skillObj3.tableData.cost = 2;
        skillObj3.tableData.damage = 2;
        skillObj3.tableData.preTime = 2;
        skillObj3.logic.Init(_owner, skillObj3.tableData);

        _allSkill.Add(skillObj1);
        _allSkill.Add(skillObj2);
        _allSkill.Add(skillObj3);

    }

    public void TryCastSkill(int index)
    {
        if (_skillCaster.IsCasting)
        {
            return;
        }

        //找到技能
        var skillObj = GetSkillObject(index);
        if(skillObj ==null )
        {
            Debug.LogError("TryCastSkill 未找到技能对象，索引号："+index);
            return;
        }

        if (skillObj .tableData .castRange > 0)
        {
            //放了一个需要目标的技能，而且没有选择技能，则这里自动选择附近的敌人
            if(_owner .curTarget ==null )
            {
                Debug.Log("未选中目标");
                //var selectTarget = _skillAssit.SelectEnemy();
                //if(selectTarget !=null )
                //{
                //    Util.SafeCall(autoSelectCallback, selectTarget);
                //}
            }

            if (_owner.curTarget == null)
            {
                return;
            }

            //if (!_owner.curTarget.CanBeAttack(_owner))
            //{
            //    return;
            //}
            var dis = Util.Distance2D(_owner.transform.position, _owner.curTarget.transform.position);
            if (dis > skillObj.tableData.castRange)
            {
                Debug.Log("距离过远" + dis + "," + "施法距离为：" + skillObj.tableData.castRange);
                //_owner.TraceTo(_owner.curTarget, skillObj.tableData.CastRange, () => OnAssitFinish(skillObj.logic));//将技能辅助施放封装成方法
                return;
            }
        }
        _owner .curTarget = _skillAssit.SelectEnemy();
        if (_owner.curTarget != null)
        {
            Util.SafeCall(autoSelectCallback, _owner.curTarget);
            float enemyDis = Vector2.Distance(_owner.curTarget.transform.position, _owner.transform.position);
            Vector2 backPos = _owner.curTarget.backPos.transform.localToWorldMatrix.MultiplyPoint(_owner.curTarget.backPos.transform.localPosition);
            float backDis = Vector2.Distance(backPos, _owner.transform.position);
            if(backDis >enemyDis )
            {
                //
                Debug.Log("面向敌人前方");
                return;
            }
        }
        Debug.Log("找到目标可以放技能了");
        CastSkill(skillObj.logic);
    }


    private SkillObject GetSkillObject(int index)
    {
        return _allSkill[index - 1];
    }
    private void OnAssitFinish(SkillLogicBase logic)
    {
        CastSkill(logic);
    }

    private void CastSkill(SkillLogicBase logic)//施放技能时就放该逻辑
    {
        //停止移动，在放技能
        _skillCaster.CastSkill(logic, _owner.curTarget);
    }

    internal void Loop()
    {
        _skillCaster.Loop();
    }
}

