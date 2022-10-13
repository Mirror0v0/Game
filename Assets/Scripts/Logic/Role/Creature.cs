using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public CreateSceneCreature serverData;
    public CreatureSceneDatabase tableData;

    public virtual void Init(CreateSceneCreature serverData,CreatureSceneDatabase tableData)
    {
        this.serverData = serverData;
        this.tableData = tableData;
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
