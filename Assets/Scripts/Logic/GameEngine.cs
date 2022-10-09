using System.Collections;
using UnityEngine;


public class GameEngine : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        TimerMgr.instance.Loop(Time.deltaTime);
    }
}
