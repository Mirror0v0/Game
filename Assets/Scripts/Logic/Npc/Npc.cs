using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Npc:Creature 
{
    public override void Init(CreateSceneCreature serverData, CreatureSceneDatabase tableData)
    {
        base.Init(serverData, tableData);
    }

    private void Awake()
    {
        NpcMgr.instance.CreateSceneNpc(this);
    }
}

