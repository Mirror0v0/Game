using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillAtkDlg
{
    private GameObject _root;

    private List<SkillPanel> _allSkillPanel = new List<SkillPanel>();

    public Action<int> OnSkillBtnClick;

    public bool isPress = false;
    public Action<bool> OnPress;


    public SkillAtkDlg()
    {
        _root = UIManager.instance.Add("UI/FightUI/SkillAtk", UILayer.FightUI);
        for (int i = 0; i < GameSetting.MaxSkillNum; i++)
        {
            var skillPanel = new SkillPanel();
            var index = i + 1;
            string panelName = "SkillPanelButton" + index;
            skillPanel.btn = _root.Find<Button>(panelName);
            skillPanel.countDown = _root.Find<Image>(panelName + "/CountDownImage");//注意加斜杠
            skillPanel.cdText = _root.Find<Text>(panelName + "/CountDownText");
            skillPanel.hideImage = _root.Find<Image>(panelName + "/Hide");
            skillPanel.btn.onClick.AddListener(() => { OnSkillClick(index); });
            _allSkillPanel.Add(skillPanel);
        }

        var touchEx = _allSkillPanel[1].btn.gameObject.GetComponent<TouchEX>();
        touchEx.DragCallback = OnDrag;
        touchEx.PointDownCallback = OnPointDown;
        touchEx.PointUpCallback = OnPointUp;
        TimerMgr.instance.CreateTimerAndStart(0.02f, -1, OnLoop);
    }

    private void OnLoop()
    {
        if(OnPress !=null )
        {
            Debug.Log("交互键是否按下" + isPress);
            OnPress(isPress);
        }
    }

    private void OnPointUp(PointerEventData obj)
    {
        isPress = false;
    }

    private void OnPointDown(PointerEventData obj)
    {
        isPress = true;
    }

    private void OnDrag(PointerEventData obj)
    {
        
    }

    public void HideImageSetActive(bool isActive,int index)
    {
        _allSkillPanel[index].hideImage.enabled = isActive;
    }

    private void OnSkillClick(int index)
    {
        Debug.Log(index);
        if (OnSkillBtnClick != null)
        {
            OnSkillBtnClick(index);
        }
    }
}

public class SkillPanel
{
    //技能按钮
    public Button btn;
    //技能冷却的旋转图片
    public Image countDown;
    //技能冷却时间的计时
    public Text cdText;

    public Image hideImage;

}

