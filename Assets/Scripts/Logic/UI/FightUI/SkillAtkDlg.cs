using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SkillAtkDlg
{
    private GameObject _root;

    private List<SkillPanel> _allSkillPanel = new List<SkillPanel>();

    public Action<int> OnSkillBtnClick;

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
            skillPanel.btn.onClick.AddListener(() => { OnSkillClick(index); });
            _allSkillPanel.Add(skillPanel);
        }
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

}

