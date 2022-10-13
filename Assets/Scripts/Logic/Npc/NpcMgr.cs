using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class NpcMgr : Singleton <NpcMgr>
{
    //public Dictionary<int, Role> AllRole = new Dictionary<int, Role>();//就一个主角（留着以后拓展）

    public void CreateSceneNpc(Npc npc)//npc初始化的时候要注意是什么类型（）
    {
        //配置表读取静态数据
        CreatureSceneDatabase database = new CreatureSceneDatabase();
        database.attack = RoleJsondata.instance.GetAttack(0);
        database.defend = RoleJsondata.instance.GetDefend(0);
        database.hp = RoleJsondata.instance.GetHp(0);
        database.maxHp = RoleJsondata.instance.GetMaxHp(0);

        //动态数据配置
        CreateSceneCreature serverData = new CreateSceneCreature();
        serverData.attack = database.attack;
        serverData.defend = database.defend;
        serverData.hp = database.hp;
        serverData.maxHp = database.maxHp;

        npc.Init(serverData, database);
    }
}

