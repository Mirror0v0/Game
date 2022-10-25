using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCollection
{
    private GameObject _root;
    private Slider _hpSlider;

    public EnemyCollection()
    {
        _root = UIManager.instance.Add("UI/FightUI/EnemyCollection", UILayer.FightUI);
        _hpSlider = _root.Find<Slider>("bgImage/Slider");
    }

    private void SetActive(bool bActive)
    {
        if (_root == null)
        {
            return;
        }
        _root.SetActive(bActive);
    }


    public void SetInfo(float curHp, float maxHp, bool active = true)
    {
        SetActive(true);
        _hpSlider.value = curHp / maxHp;
    }

    public void ResetInfo()
    {
        _hpSlider.value = 0;
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

