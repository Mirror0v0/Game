using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// 场景管理器
/// </summary>
public class SceneMgr : Singleton<SceneMgr>
{
    public void LoadScene(string sceneName, Action onLoadEnd=null)
    {
        //UIManager.instance.EventSystemEnabled = false;
        SceneManager.LoadScene(sceneName);
        if(onLoadEnd !=null )
        {
            onLoadEnd();
        }
        //UIManager.instance.EventSystemEnabled = true;
    }


    //场景异步加载
    public void LoadSceneAsync(string sceneName,Action onLoadEnd)
    {
        UIManager.instance.EventSystemEnabled = false;
        //加载场景之前应有个Loading界面将整个场景盖住，在UI得Top层（Loading显示界面放这儿）
        var ao = SceneManager.LoadSceneAsync(sceneName);
        QuickCoroutine.instance.StartCoroutine(LoadEnd(ao, onLoadEnd));
    }

    private IEnumerator LoadEnd(AsyncOperation ao, Action onLoadEnd)
    {
        while(!ao .isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        UIManager.instance.EventSystemEnabled = true;
        if (onLoadEnd !=null )
        {
            onLoadEnd();
        }
    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}

