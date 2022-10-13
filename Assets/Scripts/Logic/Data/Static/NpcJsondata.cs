using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NpcJsondata:Singleton<NpcJsondata> 
{
    public string path = null;
    public JsonData jsonData = null;

    public NpcJsondata()
    {
        path= "Json/NPCTable";
        jsonData = Load(path);
    }

    //public override JsonData Load(string path)
    //{
    //    return base.Load(path);
    //}

    virtual public JsonData Load(string path)
    {
        TextAsset asset = Resources.Load<TextAsset>(path);
        string json = asset.text;
        return JsonMapper.ToObject(json);
    }

    virtual public int GetHp(int id)
    {
        var data = jsonData;// Load(path);
        JsonData item = data[id];
        return (int)item["HP"];
    }

    virtual public int GetMaxHp(int id)
    {
        var data = jsonData;// Load(path);
        JsonData item = data[id];
        return (int)item["MaxHP"];
    }

    virtual public int GetDefend(int id)
    {
        var data = jsonData;// Load(path);
        JsonData item = data[id];
        return (int)item["Defend"];
    }

    virtual public int GetAttackRange(int id)
    {
        var data = jsonData;// Load(path);
        JsonData item = data[id];
        return (int)item["AttackRange"];
    }

    virtual public int GetAttack(int id)
    {
        var data = jsonData;// Load(path);
        JsonData item = data[id];
        return (int)item["Attack"];
    }
}

