using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : BaseUI 
{
    private Button _btnEnter;
    private Button _btnExit;
    
    public Login ()
    {
        _root = UIManager.instance.Add("UI/Login/Login");
        _btnEnter = _root.Find<Button>("Image/btn_enter");
        _btnExit = _root.Find<Button>("Image/btn_exit");
        _btnEnter.onClick.AddListener(OnBtnEnterClick);
        _btnExit.onClick.AddListener(OnBtnExitClick);
    }

    private void OnBtnExitClick()
    {
        Debug.Log("�˳���Ϸ");
        Application.Quit();
    }

    private void OnBtnEnterClick()
    {
        SceneMgr.instance.LoadScene("Level1", OnLoadEnd);
    }

    private void OnLoadEnd()
    {
        BaseUIMgr.instance.Close(this);
        FightUIMgr.instance.Init();
    }
}