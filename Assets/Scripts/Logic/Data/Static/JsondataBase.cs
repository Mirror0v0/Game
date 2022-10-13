using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class JsondataBase : Singleton <JsondataBase>
{
    public string path = null;
    public JsonData jsonData = null;
    //public JsondataBase ()
    //{
    //    jsonData = Load(path);
    //}
    //virtual public JsonData Load(string path)
    //{
    //    TextAsset asset = Resources.Load<TextAsset>(path);
    //    string json = asset.text;
    //    return JsonMapper.ToObject(json);
    //}

    //virtual public int GetHp(int id)
    //{
    //    var data = jsonData;// Load(path);
    //    JsonData item = data[id];
    //    return (int)item["HP"];
    //}

    //virtual public int GetMaxHp(int id)
    //{
    //    var data = jsonData;// Load(path);
    //    JsonData item = data[id];
    //    return (int)item["MaxHP"];
    //}

    //virtual public int GetDefend(int id)
    //{
    //    var data = jsonData;// Load(path);
    //    JsonData item = data[id];
    //    return (int)item["Defend"];
    //}

    //virtual public int GetAttackRange(int id)
    //{
    //    var data = jsonData;// Load(path);
    //    JsonData item = data[id];
    //    return (int)item["AttackRange"];
    //}

    //virtual public int GetAttack(int id)
    //{
    //    var data = jsonData;// Load(path);
    //    JsonData item = data[id];
    //    return (int)item["Attack"];
    //}




}
