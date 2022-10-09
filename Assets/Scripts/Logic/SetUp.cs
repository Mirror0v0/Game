using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUp : MonoBehaviour
{
    private void Awake()
    {
        GameMgr.instance.Init();
    }
}
