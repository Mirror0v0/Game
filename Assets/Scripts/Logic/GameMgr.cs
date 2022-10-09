using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 游戏管理器
/// </summary>
public class GameMgr:Singleton <GameMgr>
{
    //游戏引擎的根节点
    private GameObject _engineRoot;


    public void Init()
    {
        if(_engineRoot ==null )
        {

            //初始化游戏引擎
            _engineRoot = new GameObject("GameEngine");
            GameObject.DontDestroyOnLoad(_engineRoot);
            _engineRoot.AddComponent<GameEngine>();
        }

        //UI分层的初始化
        UIManager.instance.Init();

        //协程的初始化
        QuickCoroutine.instance.Init();

        //资源管理器的初始化
        //ResMgr.instance.Init();

        //跳转第一个游戏逻辑界面
        SceneManager.LoadScene("Login");
        UIManager.instance.Replace("UI/Login/Login", UILayer.Normal);

    }

}

