using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Menu
{
    private GameObject _root;
    private Button _menuBtn;

    public Menu()
    {
        _root = UIManager.instance.Add("UI/FightUI/Menu",UILayer.FightUI);
        _menuBtn = _root.Find<Button>("btnMenu");
        _menuBtn.onClick.AddListener(OnMenuBtnClick);
    }

    private void OnMenuBtnClick()
    {
        Time.timeScale = 0;
        BaseUIMgr.instance.Open<MenuSet>();
    }
}

