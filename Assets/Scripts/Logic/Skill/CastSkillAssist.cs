using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 施法辅助器
/// </summary>
public class CastSkillAssist
{

    private Creature _owner;//施法者

    public void Init(Creature owner)
    {
        _owner = owner;
    }


    //选择一个目标，这个目标要返回出去
    public Creature SelectEnemy()
    {
        //针对NPC来说：找所有的NPC，从所有NPC中找离我最近的敌人（通过NPCMgr来找，如果没有对应方法则扩展一个）
        //目前自动施法只针对NPC来做

        //最小值的初始化取无限大的数
        var minDis = float.MaxValue;

        Npc minDisNpc = null;

        //选择最近的可攻击NPC
        foreach (var npc in NpcMgr .instance .allNpc)//（字典取到的东西是Pair，是一个打包对象，是一个键值对，它的value才是NPC）
        {

            //判断NPC是否能被攻击(或者就行)
            //if(!npc .CanBeAttack (_owner ))
            //{
            //    continue;
            //}
            //if(npc .HP <=0)
            //{
                
            //}

            var dis = Util.Distance2D(npc.transform .position, _owner.transform .position);
            if(dis>GameSetting .MaxAutoSelectDis )
            {
                continue;//如果距离大于感知距离，则不管，直接跳过（短路写法）
            }

            if(dis<minDis )
            {
                minDis = dis;
                minDisNpc = npc;
            }

        }
        return minDisNpc;//这里如果上面一直没找到则返回null

    }
}

