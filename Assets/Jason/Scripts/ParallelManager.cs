using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
public class ParallelManager : MonoBehaviour
{   //https://www.itread01.com/content/1534966852.html
    /*
     C#中實現多線呈的另一個方式是使用Parallel類
    Parallel類使用多个task,因此使用多個線程完成作業
    Parallel.For()和 Parallel.ForEach()方法多次調用同一個方法,而 Parallel.Invoke()方法允許同时調用不同的方法

    1.Parallel.Invoke 主要用於任務的並行 主要用於任務的並行，就是並發執行一系列任務，然後等待所有完成。和Task比起來，省略了Task.WaitAll這一步，自然也缺少了Task的相關管理功能。
        它有兩種形式:
　　    Parallel.Invoke( params Action[] actions);//定義委派陣列 並執行
　　    Parallel.Invoke(Action[] actions,TaskManager manager,TaskCreationOptions options); 
    2.Parallel.For方法，主要用於處理針對數組元素的並行操作(數據的並行)
    3、Foreach方法，主要用於處理泛型集合元素的並行操作(數據的並行)
   */
    static System.Object _lock = new System.Object();
    static SemaphoreSlim _sem = new SemaphoreSlim(3); //最多可以 3 人一起買票
    static int ticketQty = 5;                         //而票只有 5 張
    // Start is called before the first frame update
    void Start()
    {
        /*
           Parallel.For()
     
         */
        #region Parallel.Invoke 
        // Parallel.Invoke 內的未派沒執行完部不會跳出 Parallel執行緒(但是  Parallel內部會因 thread.sleap而改變順序)
        Action<string> ActionTest = (string data) =>{ Thread.Sleep(5000);  Debug.Log($" ActionTest  ,thread :  {Thread.CurrentThread.ManagedThreadId}  data : {data}");  };
        Action<string> ActionTest2 = (string data) => { Thread.Sleep(200); Debug.Log($"ActionTest2  ,thread :  {Thread.CurrentThread.ManagedThreadId}  data : {data}"); };
        Action[] actions = new Action[] //定義委派陣列 並執行
        {
            ()=>{ActionTest("test1"); },
            ()=>{ActionTest2("test2"); },
            ()=>{ActionTest("test3"); } 
        };
        Debug.Log("Parallel.Invoke 1 Test");
        Parallel.Invoke(actions);
        Debug.Log("結束");
        #endregion
        #region 情境題
        /*
         考慮以下情境:

            1. 有 9 個人想買票

            2. 售票亭一次最多同時 3 人進去買

            3. 但票只有 5 張
        結合 SemaphoreSlim 
         */
        //Parallel.For(1, 10, i =>        //每個代表一線呈              //但有 9 個人想買
        //{
        //    Thread.Sleep(300);

        //    BuyTicket(i.ToString("00"));
        //});
        #endregion
    }
    /// <summary>
    /// 購買 票
    /// </summary>
    /// <param name="id"></param>
    static void BuyTicket(string id)
    {
        

        _sem.Wait();//封鎖目前的執行緒，直到這個執行緒可以進入 SemaphoreSlim 為止
        Debug.Log($"Thread : {Thread.CurrentThread.ManagedThreadId},  {id} +  進入售票亭");
        if (HasTicket())
        {
            Debug.Log(id + " 買票中 ( 要花 5 秒 )");
            Thread.Sleep(5000);
            Debug.Log(id + " 付款離開");
        }
        else
        {
            Debug.Log(id + " 沒票直接離開");
        }
        _sem.Release();
    }
    /// <summary>
    /// 判斷有無票
    /// </summary>
    /// <returns></returns>
    static bool HasTicket()
    {
        if (ticketQty <= 0) return false;

        lock (_lock)
        {
            if (ticketQty <= 0) return false;

            ticketQty--;
            return true;
        }
    }
}
