using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// 界面管理器（单例）
/// 管理所有的系统界面
/// </summary>
public class BaseUIMgr : Singleton <BaseUIMgr>
{

    private List<BaseUI> _allDlg = new List<BaseUI>();

    public T Open<T>()where T :BaseUI ,new ()
    {
        var dlg = new T();
        _allDlg.Add(dlg);
        return dlg;
    }


    public void Close(BaseUI dlg)
    {
        dlg.CloseSelf();
        _allDlg.Remove(dlg);
    }

    public void CloseAll()
    {
        foreach (var dlg in _allDlg )
        {
            dlg.CloseSelf();
        }
        _allDlg.Clear();
    }

}


//作为所有系统UI基类
public abstract class BaseUI
{
    protected GameObject _root;

    public bool IsAlive
    {
        get
        {
            return _root != null;//界面是否还活着就看_root有没有
        }
    }
    protected void Load(string uiPath)
    {
        _root = UIManager.instance.Add(uiPath);

        var btnClose = _root.Find<Button>("bg/BtnClose");
        if(btnClose !=null )
        {
            Debug.Log("找到了按钮");
            btnClose.onClick.AddListener(OnClose);
        }
    }

    virtual protected void OnClose()
    {
        CloseSelf();
    }

    virtual public void CloseSelf()
    {
        UIManager.instance.Remove(_root);
    }
}


