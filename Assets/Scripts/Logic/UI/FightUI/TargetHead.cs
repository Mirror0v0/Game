using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TargetHead
{
    private GameObject _root;
    private Slider _hpSlider;

    public TargetHead()
    {
        _root = UIManager.instance.Add("UI/FightUI/TargetHead",UILayer.FightUI);
        _hpSlider = _root.Find<Slider>("hpSlider");
    }

    private void SetActive(bool bActive)
    {
        if (_root == null)
        {
            return;
        }
        _root.SetActive(bActive);
    }


    public void SetInfo(float curHp, float maxHp,bool active=true)
    {
        SetActive(true);
        _hpSlider.value = curHp / maxHp;
    }

    public void Hide()
    {
        SetActive(false);
    }


    public void Show()
    {
        SetActive(true);
    }
}

