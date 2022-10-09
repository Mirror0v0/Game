using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public static class UnityExtern
{

    public static T Find<T>(this GameObject parent ,string path="")where T :class 
    {

        var targetObj = parent.transform.Find(path);
        if(targetObj ==null )
        {
            return null;
        }

        return targetObj.GetComponent<T>();
    }

    /// <summary>
    /// 删除所有的子节点
    /// </summary>
    /// <param name="parent"></param>
    public static void DestroyAllChildren(this GameObject parent)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            var child = parent.transform.GetChild(i);
            GameObject.Destroy(child.gameObject);
        }
    }
}

