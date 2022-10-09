using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;



/// <summary>
/// 计时器（单例）
/// 管理所有的定时器
/// </summary>
public class TimerMgr:Singleton <TimerMgr>
{

   
    public event Action<float> TimerLoopCallBack;

    //创建定时器
    /// <summary>
    /// 
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <param name="repeatTimes">小于零则表示无限循环，其实就是调不调用Stop的原因</param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public Timer CreateTimer(float deltaTime,int repeatTimes,Action callBack)
    {
        var timer = new Timer();
        timer.DeltaTime = deltaTime;
        timer.RepeatTimes = repeatTimes;
        timer.CallBack = callBack;
        return timer;
    }

    public Timer CreateTimerAndStart(float deltaTime, int repeatTimes, Action callBack)
    {    
        var timer = CreateTimer(deltaTime, repeatTimes, callBack);
        timer.Start();
        return timer;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="deltaTime">计时器驱动间隔</param>
    public void Loop(float deltaTime)//相当于游戏引擎将TimerMgr驱动起来，
    {
        
        if (TimerLoopCallBack != null)
        {
            TimerLoopCallBack(deltaTime);
        }
    }

}


/// <summary>
/// 定时器
/// </summary>
public class Timer
{

    public float DeltaTime;//间隔时间
    public int RepeatTimes;//定时器重复的次数
    //
    public Action CallBack;//重复一次时内部需要做的事情

    //计时的持续时间（当前）
    private float _duringTime;
    //已执行了多少次（当前）
    private int _repeatedTimes;

    public bool IsRunning = false;


    private void Reset()
    {
        _duringTime = 0;
        _repeatedTimes = 0; 
        Pause();
    }

    //开始、结束
    public void Start()
    {

        Reset();
        IsRunning = true;
        TimerMgr.instance.TimerLoopCallBack += Loop;
    }

    public void Pause()
    {

        IsRunning = false;
        TimerMgr.instance.TimerLoopCallBack -= Loop;
    }


    //真正的Stop
    public void Stop()
    {
        //首先先停止，也就是暂停
        Pause();
        //然后重置所有数值
        Reset();

    }

    public void Loop(float deltaTime)
    {
        _duringTime += deltaTime;
      
        if (_duringTime >=DeltaTime ||Util .FloatEqual (_duringTime ,DeltaTime))//浮点数的相等不能用等号判断，有精度的问题，浮点数表示的都是近似值
        {
            ++_repeatedTimes;
            _duringTime -= DeltaTime;

            if (CallBack !=null )
            {
                CallBack();
            }

            if (RepeatTimes > 0
                &&_repeatedTimes >=RepeatTimes )
            {
                Stop();
            }
        }                
    }


    private void FixedUpdate()
    {
        //TimerMgr.instance.FixedLoop();//但其实也无法避免，fixupdate是0.02s运行一次，而想0.01秒执行一次还是很难做

    }
}


