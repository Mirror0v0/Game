using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightWin 
{
    private GameObject _root;
    private Button _btnRestart;
    private Button _btnReturn;
    
    public FightWin()
    {
        _root = UIManager.instance.Add("UI/FightUI/FightWin",UILayer.FightUI);
        _btnRestart = _root.Find<Button>("bgImage/Image/btnRestart");
        _btnReturn = _root.Find<Button>("bgImage/Image/btnReturn");
        _btnRestart.onClick.AddListener(OnBtnRestartClick);
        _btnReturn.onClick.AddListener(OnBtnReturnClick);
    }

    private void OnBtnReturnClick()
    {
        Time.timeScale = 1;
        FightUIMgr.instance.Reset();
        SceneMgr.instance.LoadScene("Login", ()=>BaseUIMgr.instance.Open<Login>());
    }

    private void OnBtnRestartClick()
    {
        FightUIMgr.instance.Reset();
        Time.timeScale = 1;
        SceneMgr.instance.LoadScene(SceneMgr.instance.GetCurrentSceneName(),()=>FightUIMgr .instance .Init ());
    }

    public void SetFightWinUIActive(bool isActive)
    {
        if (_root == null)
        {
            return;
        }
        _root.SetActive(isActive);
    }
}
