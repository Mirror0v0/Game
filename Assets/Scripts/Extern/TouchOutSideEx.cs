using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.EventSystems;



/// <summary>
/// 点击到范围外，关闭
/// 挂在menu上面
/// 要在所有子节点都添加之后再添加此脚本
/// </summary>
public class TouchOutSideEx:MonoBehaviour 
{

    private Transform[] _allTransform;

    private Timer _timer;


    public Action OutSideCallBack;

    private void Awake()
    {
        _allTransform = this.gameObject.GetComponentsInChildren<Transform>();

        EventSystem.current.SetSelectedGameObject(gameObject);//让整个UI系统选中当前这个menu

        _timer = TimerMgr.instance.CreateTimerAndStart(0.1f, -1, CheckOutSide);//每隔0.1秒来无限循环检测是否点击了menu以外的东西
    }

    private void OnDestroy()
    {
        _timer.Stop();
    }


    private void CheckOutSide()
    {
        bool bOutSide = true;
        foreach (var tr in _allTransform )
        {
            //如果子物体与这个事件UI相同，则说明没有选中外面
            //选中的对象是不是这个menu下面的菜单子项
            //EventSystem .current .currentSelectedGameObject改变的时机，点中带有EventSystem事件的就会选中谁
            if (tr.gameObject ==EventSystem .current .currentSelectedGameObject )
            {
                bOutSide = false;
                break;
            }
        }

        //否则选中外面
        if(bOutSide )
        {
            if(OutSideCallBack !=null )
            {
                OutSideCallBack.Invoke();//触发外面的事件
            }
            _timer.Stop();//定时器停止检测
        }



    }
}

