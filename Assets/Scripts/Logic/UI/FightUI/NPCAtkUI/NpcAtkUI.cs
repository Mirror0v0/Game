using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class NpcAtkUI
{
    private GameObject _root;

    private Creature _owner;


    public void Init(Creature owner)
    {
        _owner = owner;
        _root = UIManager.instance.Add("UI/FightUI/atkArrangeUI",UILayer.Scene);
        TimerMgr.instance.CreateTimerAndStart(0.02f, -1, FollowTarget);
    }

    public void SetActive(bool isActive)
    {
        if (_root == null)
        {
            return;
        }
        _root.SetActive(isActive);
    }

    private void FollowTarget()
    {
        CalculatePos();
    }

    public void CalculatePos()
    {
        if(_owner ==null )
        {
            return;
        }
        Vector2 pos = Camera.main.WorldToScreenPoint((_owner as Npc).atkArragePos . transform.position);
        _root.transform.position = pos;
    }
}

