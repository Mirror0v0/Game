using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





/// <summary>
/// 时间线
/// </summary>
public class TimeLine
{

    public float TimeSpeed//这个是扩展，可以扩展时间线的执行速度（暂时保留）
    {
        get;
        set;
    }

    //是否开始
    private bool m_isStart;

    //是否暂停
    private bool m_isPause;

    //当前计时
    private  float m_curTime;

    //重置事件
    private Action m_reset;

    //每帧回调
    private Action<float> m_update;

    public TimeLine ()
    {
        TimeSpeed = 1;

        Reset();
    }


    /// <summary>
    /// 添加事件（事件的挂载每个事件只能挂载一次，不能是放一次技能挂载一次）时间线描述的是整个技能的流程，而不是技能放一次的流程，支持放多遍，如果有减等，整个定义就被修改
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="id"></param>
    /// <param name="method"></param>
    public void AddEvent(float delay,int id,Action <int>method)
    {
        LineEvent param = new LineEvent(delay, id, method);
        m_update += param.Invoke;//m_update委托（这里理解为事件），他在Update中，在这里是Loop中，每帧都会进入时间线事件（LineEvent）的Invoke，来检查当前事件的触发条件，时间线的每一帧都会对每一个时间线上挂载的事件进行条件判断
        m_reset += param.Reset;//这里m_update与m_reset通篇只有加等没有减等，用减等也可以实现下面的事件只执行一次，但是会有问题，事件执行一次仍没问题，但减等会改变时间线，
                               //时间线的定义其实是在角色初始化技能的时候执行一次，运行一次技能与运行十次技能都不应该会受到改变，

    }

    //开始时间线
    public void Start()
    {
        Reset();//开始前reset保证每次时间线的重置来记录技能施放

        m_isStart = true;
        m_isPause = false;
    }

    //暂停
    public void Pause()
    {
        m_isPause = true;
    }

    //重新开始(这里应该是继续)
    public void Resume()
    {
        m_isPause = false;
    }

    //重置（）还原
    public void Reset()
    {
        m_curTime = 0;//时间线计时归零
        m_isStart = false;//不开始
        m_isPause = false;//没开始就不用谈暂停

        if(null !=m_reset )
        {
            m_reset();//在时间线的LineEvent里面去调用所有事件（Event）的reset函数，所有的时间线事件（LineEvent）也要归零
        }

    }

    public void Loop(float deltaTime)//deltaTime在外面是传进来了Time.deltaTime
    {
        //当（isStart&&!Pause）时，执行时间线
        if(!m_isStart ||m_isPause )//时间线开始并且没有被暂停就进入下面
        {
            return;
        }
        m_curTime += deltaTime;
        if(null !=m_update )
        {
            m_update(m_curTime);//m_curTime是deltaTime的累加,时间线开始到当前的已经经过的时间，
                                //这里传给时间线（LineEvent）事件的时间就是时间线开始到目前为止的时间
        }
    }

    private class LineEvent
    {

        public float Delay { get; protected set; }

        public int Id { get; protected set; }

        public Action<int> Method { get; protected set; }

        public bool m_isInvoke = false;

        public LineEvent (float delay,int id,Action <int >method)
        {
            Delay = delay;
            Id = id;
            Method = method;

            Reset();//重置各种状态，刚开始不调用也没关系（习惯）
        }

        public void Reset()//Reset就是reset这个标记位,标记为reset，则时间线就可以重新来一次
        {
            m_isInvoke = false;//委托是有个Invoke函数的，其效果与直接执行的效果是一样的
        }


        //每帧执行（自己驱动的帧）,time是从时间线开始，到目前为止经过的时间
        public void Invoke(float time)//这个Time是上面的m_update 传过来，因为Invoke只和update产生了关联
        {
            //当前事件还没到延迟时间，直接返回
            if(time <Delay )
            {
                return;
            }

            //m_isInvoke表示这个事件本次是不是已经执行过了，TimeLine运行一次，
            //则挂载的事件只能运行一次（用来标记事件本身是否已经执行过了）

            //下面这个if是当时间线大于延迟时间时都能走过来，但只有刚超过延迟时间的第一次执行事件，后面再也不执行
            //m_isInvoke就是为此而生的，记录这个事件是不是已经执行了，并且是不是注册了回调函数Method，没注册回调函数事件就没有什么用
            if (!m_isInvoke &&null !=Method )//执行时间，已经超过了事件（Event）的延迟时间
            {//只能用标记位，不能用减等，标记这次时间线的执行当前事件已经执行过了，Reset就是reset这个标记位
                m_isInvoke = true;//将事件标记为已执行，就算时间线一直往后走，这个时间线也一直进不来
                Method(Id);//保证Method在时间线的整个生存周期内只会执行一次
            }
        }

        

    }
}



