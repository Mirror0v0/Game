using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//public class Singleton
//{

//    /************写法一（懒人单例）*********************
//    private Singleton()
//    {

//    }

//    public static readonly Singleton instance = new Singleton();
//    */

//    /*************写法二（标准单例）*******************************************
//    private static Singleton _instance = new Singleton();
//    private Singleton ()
//    {

//    }

//    public static Singleton Instance
//    {
//        get { return _instance; }
//    }
//    */
//}



/// <summary>
/// 单例泛型，单例，用instance获取
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : new()
{
    private static T _instance;

    static Singleton()
    {
        _instance = new T();
    }

    public static T instance
    {
        get
        {
            return _instance;
        }
    }
}



