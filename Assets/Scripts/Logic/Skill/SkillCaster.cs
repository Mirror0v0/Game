using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





//负责技能的施放
public class SkillCaster
{

    private Creature _owner;
    public bool IsCasting
    {
        get
        {
            return _castingSkill != null;
        }
    }
    private SkillLogicBase _castingSkill;
    //技能逻辑结束
    private Action skillLogicEndCallback;
    internal void CastSkill(SkillLogicBase skillLogic,Creature target)
    {
        _castingSkill = skillLogic;
        skillLogic.Start(target,OnSkillLogicEnd);
    }

    private void OnSkillLogicEnd()
    {
        //将当前施放的技能清空
        _castingSkill = null;
        //通知技能逻辑结束
        if(skillLogicEndCallback !=null)
        {
            skillLogicEndCallback();
        }
    }

    public void Init(Creature owner,Action skillEndCallback=null)
    {
        _owner = owner;
        skillLogicEndCallback = skillEndCallback;
    }

    public void Loop()
    {
        //驱动正在施放的技能//驱动时也就驱动当前不为空的技能
        if(_castingSkill !=null )
        {
            _castingSkill.Loop();
        }
    }
}

