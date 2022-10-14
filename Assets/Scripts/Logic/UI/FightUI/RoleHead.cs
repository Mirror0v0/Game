using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RoleHead
{
    private GameObject _root;
    private Slider _hpSlider;

    public RoleHead()
    {
        _root = UIManager.instance.Add("UI/FightUI/RoleHead",UILayer.FightUI);
        _hpSlider = _root.Find<Slider>("hpSlider");
    }

    public void SetInfo(float curHp, float maxHp)
    {
        _hpSlider.value = curHp / maxHp;
    }
}

