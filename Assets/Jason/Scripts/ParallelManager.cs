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
        #region Parallel.Invoke 
        // Parallel.Invoke 內的委派沒執行完不不會跳出 Parallel執行緒(但是  Parallel內部執行會因 thread.sleap而改變順序)
        //Action<string> ActionTest = (string data) =>{ Thread.Sleep(5000);  Debug.Log($" ActionTest  ,thread :  {Thread.CurrentThread.ManagedThreadId}  data : {data}");  };
        //Action<string> ActionTest2 = (string data) => { Thread.Sleep(200); Debug.Log($"ActionTest2  ,thread :  {Thread.CurrentThread.ManagedThreadId}  data : {data}"); };
        //Action[] actions = new Action[] //定義委派陣列 並執行
        //{
        //    ()=>{ActionTest("test1"); },
        //    ()=>{ActionTest2("test2"); },
        //    ()=>{ActionTest("test3"); } 
        //};
        //Debug.Log("Parallel.Invoke 1 Test");
        //Parallel.Invoke(actions);
        //Debug.Log("結束");
        #endregion
        #region Parallel.for
        /* 
           Parallel.For(int fromInclusive, int toExclusive, Action<int, ParallelLoopState> body);
           前两個参数定義了循環的開頭和结束。第 3個参数是一個Action<int>委托。 整数参数是循環的迭代次数,該參數被傳遞给Action<int>委托引用的方法，由於每次循環都開啟了新的任务和線呈，因此每个線程的直行行顺序是不能保正的。
        */
        //i 代表索引值
        //ParallelLoopResult result = Parallel.For(0, 10, i => {
        //    Debug.Log(i);
        //});
        //Debug.Log(result.IsCompleted);// 這個值表示工作是否已經完成。


        //int[] nums = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        //Parallel.For(0, nums.Length, (i) =>
        //{
        //    Debug.Log($"num[i] :  {nums[i]} ,索引為{i}, task : { Task.CurrentId} , thread : {Thread.CurrentThread.ManagedThreadId}");
        //});

        //========================中斷  (Stop/Break)========================================================
        /*
         
         Parallel.For()方法的一個多載接受第3個Action<int, ParallelLoopState>类型的参數。就可以调用ParalleLoopState的Break()或Stop()方法,以影響循環的结果。
         Stop和Break，可以分別用來控制Parallel.For的執行。 
         Stop  (像迴圈中的break): 
                表示Parallel.For的執行立刻停止，無論其他執行單元是否達到停止的條件。
         Break (像迴圈中的continune):
                則表示滿足條件的當前執行單元立刻停止， 而對於其他執行單元，其中滿足停止條件也會通過Break停止，
                其他未滿足停止條件的則會繼續執行下去，從而全部執行完畢，自然停止。當所有執行單元停止後, Parallel.For函式才停止執行並退出。
         
         */
        //int[] nums = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        ////break
        //Parallel.For(1, nums.Length, (i, ParallelLoopState) =>
        //{
        //    // 當某一個迴圈單元的數大於x，
        //    // 則跳出當前執行單元，等待其他執行單元結束
        //    // 所有執行單元結束後退出Parallel.For的執行

        //    if (nums[i] == 5)//其內容等於5  往下不執行  
        //    {
        //        // 跳出當前執行單元
        //        ParallelLoopState.Break();
        //        Debug.Log("break");
        //        return;//不加return，可能會發生該程序資源未釋放。
        //    }

        //    Debug.Log($"num[i] :  {nums[i]}  , 索引為{i} , task : { Task.CurrentId} , thread : {Thread.CurrentThread.ManagedThreadId}");
        //});
        //stop
        //Parallel.For(0, nums.Length, (int i, ParallelLoopState pls) =>
        //{
        //    // 當某一個迴圈單元的數大於x，
        //    // 則停止Parallel.For的執行

        //if (nums[i] > 5)//其內容大於5 
        //{
        //    // 跳出當前執行單元
        //    ParallelLoopState.Break();
        //    Debug.Log("break");
        //    return;//不加return，可能會發生該程序資源未釋放。
        //}
        //    Debug.Log($"num[i] :  {nums[i]}  , task : { Task.CurrentId} , thread : {Thread.CurrentThread.ManagedThreadId}");
        //});
        //Debug.Log("結束");

        #endregion
        #region Parallel.ForEach()
        List<int> nums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        //ParallelLoopResult result = Parallel.ForEach<int>(nums, (data) =>
        //{
        //    Debug.Log(data);
        //});
        //============================中斷==========================
        //Parallel.ForEach<int>(nums, (data, ParallelLoopState) =>
        //{
        //    if (data ==5)
        //    {
        //        ParallelLoopState.Break();
        //        return;
        //    }
        //    Debug.Log($"num[i] :  {data}  , task : { Task.CurrentId} , thread : {Thread.CurrentThread.ManagedThreadId}");
             
        //});
        Parallel.ForEach<int>(nums, (data, ParallelLoopState) =>
        {
            if (data > 5)
            {
                ParallelLoopState.Stop();
                return;
            }
            Debug.Log($"num[i] :  {data}  , task : { Task.CurrentId} , thread : {Thread.CurrentThread.ManagedThreadId}");

        });
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
