using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSet : BaseUI 
{

    private Button _btnRestart;
    private Button _btnContinue;
    private Button _btnReturn;
    
    public MenuSet ()
    {
        _root = UIManager.instance.Add("UI/FightUI/MenuSet");
        _btnRestart = _root.Find<Button>("Image/Image/btnRestart");
        _btnContinue = _root.Find<Button>("Image/Image/btnContinue");
        _btnReturn = _root.Find<Button>("Image/Image/btnReturn");
        _btnRestart.onClick.AddListener(OnBtnRestartClick);
        _btnContinue.onClick.AddListener(OnBtnContinueClick);
        _btnReturn.onClick.AddListener(OnBtnReturnClick);
    }

    private void OnBtnReturnClick()
    {
        BaseUIMgr.instance.Close(this);
        FightUIMgr.instance.Reset();
        SceneMgr.instance.LoadScene("Login", ()=>BaseUIMgr.instance.Open<Login>());
    }

    private void OnBtnContinueClick()
    {
        Time.timeScale = 1;
        BaseUIMgr.instance.Close(this);
    }

    private void OnBtnRestartClick()
    {
        BaseUIMgr.instance.Close(this);
        FightUIMgr.instance.Reset();
        Time.timeScale = 1;
        SceneMgr.instance.LoadScene(SceneMgr.instance.GetCurrentSceneName(),()=>FightUIMgr .instance .Init ());
    }
}
