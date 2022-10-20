using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUI : BaseUI 
{
    private Button _btnLevel1;
    private Button _btnExit;
    
    public LevelSelectUI()
    {
        _root = UIManager.instance.Add("UI/LevelSelect/LevelSelectUI");
        _btnLevel1 = _root.Find<Button>("bgImage/btnLevel1");
        _btnExit = _root.Find<Button>("bgImage/btn_exit");
        _btnLevel1.onClick.AddListener(OnBtnLevel1Click);
        _btnExit.onClick.AddListener(OnBtnExitClick);
    }

    private void OnBtnExitClick()
    {
        BaseUIMgr.instance.Close(this);
        BaseUIMgr.instance.Open<Login>();
    }

    private void OnBtnLevel1Click()
    {
        BaseUIMgr.instance.Close(this);
        //SceneMgr.instance.LoadSceneAsync("Level1", OnLoadEnd);
        SceneMgr.instance.LoadScene("Level1", OnLoadEnd);
    }

    private void OnLoadEnd()
    {
        //BaseUIMgr.instance.Close(this);
        FightUIMgr.instance.Init();
        Debug.Log("场景进入后初始化");
    }
}
