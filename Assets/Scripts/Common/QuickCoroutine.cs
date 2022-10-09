using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class MonoClass : MonoBehaviour
{ 

}



/// <summary>
/// 协程管理器(该类需要到游戏一开始进行初始化)
/// </summary>
public class QuickCoroutine:Singleton <QuickCoroutine> 
{

    GameObject _coroutineRoot;
    /// <summary>
    /// 用来跑协程的Mono
    /// </summary>
    MonoBehaviour _coroutineMono;

    public void Init()
    {
        _coroutineRoot = new GameObject("QuickCoroutine");
        GameObject.DontDestroyOnLoad(_coroutineRoot);
        _coroutineMono = _coroutineRoot.AddComponent<MonoClass>();//有些版本直接添加Monobehavior会出错，可以在创建一个内部类再继承Monobehavior,在挂载上来即可
    }

    //internal void StartCoroutine(object onHitEnd)
    //{
    //    throw new NotImplementedException();
    //}

    /// <summary>
    /// 用单例将协程的开启进行封装
    /// </summary>
    /// <param name="routine"></param>
    //public void StartCoroutine(IEnumerator routine)
    //{
    //    //协程的可迭代函数类型
    //    _coroutineMono.StartCoroutine(routine);
    //}
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        //协程的可迭代函数类型
        return _coroutineMono.StartCoroutine(routine);
    }

}

