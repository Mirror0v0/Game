using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class NpcWarningLineUI
{
    private GameObject _root;

    private Slider _hpSlider;

    private Creature _owner;

    //private float _curWarningValue = 0;
    //private float _maxWarningValue = 100f;


    private float _warningLineTimer = 0.02f;
    private float _warningLineTime = 0;

    public Action onLineFull;

    public void Init(Creature owner)
    {
        _owner = owner;
        //_owner.HPChangeCallback += OnTargetHPChanged;
        _root = UIManager.instance.Add("UI/FightUI/WarningLine", UILayer.Scene);
        _hpSlider = _root.Find<Slider>("UISlider");
        TimerMgr.instance.CreateTimerAndStart(0.02f, -1, FollowTarget);
    }

    private void FollowTarget()
    {
        CalculatePos();
    }

    public void SetActive(bool bActive)
    {
        if (_root == null)
        {
            return;
        }
        _root.SetActive(bActive);
    }

    public void SetInfo(float curHp, float maxHp, bool bActive = true)
    {
        //SetActive(bActive);
        _hpSlider.value = curHp / maxHp;
    }
    private void OnWarningLineChanged(int curWarningValue, int maxWarningValue)
    {
        Debug.Log(curWarningValue );
        Debug.Log(maxWarningValue);
    }

    public void CalculatePos()
    {
        if(_owner ==null )
        {
            return;
        }
        Vector3 pos = Camera.main.WorldToScreenPoint((_owner as Npc).warningLinePos.transform.position);
        _root.transform.position = pos;
    }

    public void WarningLineIncrease(float lineValue)
    {
        _warningLineTime += Time.deltaTime; 
        if(_warningLineTime >=_warningLineTimer )
        {
            (_owner as Npc).curWarningValue += lineValue;
            SetInfo((_owner as Npc).curWarningValue, (_owner as Npc).maxWarningValue);
            _warningLineTime -= _warningLineTimer;
            if((_owner as Npc).curWarningValue>= (_owner as Npc).maxWarningValue)
            {
                if(onLineFull !=null )
                {
                    onLineFull();
                }
            }
        }
    }

    public void WarningLineDecrease(float lineValue)
    {
        if((_owner as Npc).curWarningValue<=0)
        {
            return;
        }
        _warningLineTime += Time.deltaTime;
        if (_warningLineTime >= _warningLineTimer)
        {
            (_owner as Npc).curWarningValue -= lineValue;
            SetInfo((_owner as Npc).curWarningValue, (_owner as Npc).maxWarningValue);
            _warningLineTime -= _warningLineTimer;
            if((_owner as Npc).curWarningValue<=0)
            {
                (_owner as Npc).curWarningValue = 0;
            }
        }
    }
}

