using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;



/// <summary>
/// 资源管理器
/// </summary>
public class ResMgr:Singleton <ResMgr>
{

    public GameObject GetInstance(string resPath)
    {
        var temp = GetResources<GameObject>(resPath);
        if (temp == null)
        {
            Debug.LogError("未找到资源：" + resPath);
            return null;
        }
        return (GameObject)GameObject.Instantiate(temp);
    }

    public GameObject GetInstance(string resPath, Vector2 pos)
    {
        var temp = GetResources<GameObject>(resPath);
        if (temp == null)
        {
            Debug.LogError("未找到资源：" + resPath);
            return null;
        }
        //var realPos = pos != null ? pos.Value : temp.transform.position;


        return (GameObject)GameObject.Instantiate(temp, pos, temp.transform.rotation);
    }

    public GameObject GetInstance(string resPath, Vector3? pos=null)
    {
        var temp = GetResources<GameObject>(resPath);
        if (temp == null)
        {
            Debug.LogError("未找到资源：" + resPath);
            return null;
        }
        var realPos = pos != null ? pos.Value : temp.transform.position;

      
        return (GameObject)GameObject.Instantiate(temp, realPos, temp.transform.rotation);
    }

    public T GetResources<T>(string resPath)where T : UnityEngine.Object
    {
        return Resources.Load<T>(resPath);
    }

    public void Release(GameObject ui)
    {
        GameObject.Destroy(ui);
    }
}

