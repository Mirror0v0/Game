using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.EventSystems;



/// <summary>
/// UI管理器（单例）
/// </summary>
public class UIManager : Singleton<UIManager>
{

    private GameObject _uiRoot;

    private EventSystem _event;

    //UILayer:层次     GameObject：每一层的根节点
    private Dictionary<UILayer, GameObject> _uiLayerRoot = new Dictionary<UILayer, GameObject>();

    //事件系统启用
    public bool EventSystemEnabled
    {
        set
        {
            _event.enabled = value;
        }
        get
        {
            return _event.enabled;
        }
    }

    public void Init()
    {
        if(_uiRoot ==null )
        {
            _uiRoot = ResMgr.instance.GetInstance("UI/UISystem");
            GameObject.DontDestroyOnLoad(_uiRoot);

            _event = _uiRoot.Find<EventSystem>("EventSystem");//生成的时候同时保存事件系统
        }
        

        //UI分五层（保存每一层的根节点）
        _uiLayerRoot.Add(UILayer.Scene, _uiRoot.Find<Transform>("Canvas/Scene").gameObject);
        _uiLayerRoot.Add(UILayer.Touch, _uiRoot.Find<Transform>("Canvas/Touch").gameObject);
        _uiLayerRoot.Add(UILayer.FightUI, _uiRoot.Find<Transform>("Canvas/FightUI").gameObject);
        _uiLayerRoot.Add(UILayer.Normal, _uiRoot.Find<Transform>("Canvas/Normal").gameObject);
        _uiLayerRoot.Add(UILayer.Top, _uiRoot.Find<Transform>("Canvas/Top").gameObject);
    }


    //删除掉某一层的全部UI
    public void RemoveLayer(UILayer uILayer = UILayer.Normal)
    {
        _uiLayerRoot[uILayer].DestroyAllChildren();
    }

    //添加某个界面上的UI   
    //uiPath：resources下面的预制件
    //uILayer：UI层次
    public GameObject Add(string uiPath,UILayer uILayer = UILayer.Normal)
    {
        var root = ResMgr.instance.GetInstance(uiPath);
        root.transform.SetParent(_uiLayerRoot[uILayer].transform, false);

        return root;
    }

    public GameObject  Replace(string uiPath, UILayer layer=UILayer.Normal)//给UIlayer默认值，如果是normal就可以省略不写
    {
        //先Remove,删除这个；layer下面所有的节点
        //注意，不写重复的代码，可以写相同的函数
        //_uiLayerRoot[layer].DestroyAllChildren();
        RemoveLayer(layer);

        //在Add
        return Add(uiPath, layer);
    }

    //UI根节点（这里的删除有可能并不是真正的删除，可能是隐藏，可能是回收到对象池中）
    public void Remove(GameObject ui)
    {
        //资源从ResMgr里面加载，就从里面回收
        ResMgr.instance.Release(ui);
    }

}


/// <summary>
/// UI层次
/// </summary>
public enum UILayer
{
    Scene,
    Touch,
    FightUI,
    Normal,
    Top
}

