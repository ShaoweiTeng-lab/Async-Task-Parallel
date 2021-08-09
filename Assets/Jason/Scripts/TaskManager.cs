using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;
using System.Threading;
public class TaskManager : MonoBehaviour
{   /*
    Task是在ThreadPool的基礎上推出的，ThreadPool中有若幹數量的線程，如果有任務需要處理時，會從線程池中獲取一個空閒的線程來執行任務，任務執行完畢後線程不會銷毀，
    而是被線程池回收以供後續任務使用。當線程池中所有的線程都在忙碌時，又有新任務要處理時，線程池才會新建一個線程來處理該任務，如果線程數量達到設置的最大值，任務
    會排隊，等待其他任務釋放線程後再執行。線程池能減少線程的創建，節省開銷   
    */
    // Start is called before the first frame update
    void Start()
    {

        //Task t1 = new Task(() => { Debug.Log("t1 Start new Task"); });//兩者是個別開新執行緒 所以執行順序不一定
        //Task tastVoid = new Task(Test);
        //t1.Start();
        //tastVoid.Start();
        //=================Task創建方式======================================================================================
        //Task創建方式有三種 ,ThreadPool不能控制線程的執行順序，我們也不能獲取線程池內線程取消/異常/完成的通知，不能有效監控和控制線程池中的線程
        //1.new方式實例化一個Task，需要通過Start方法啟動
        //Task task = new Task(() =>
        //{
        //    Thread.Sleep(100);
        //    Debug.Log($"hello, task1的線程ID為{Thread.CurrentThread.ManagedThreadId}");
        //});
        //task.Start();

        ////2.Task.Factory.StartNew(Action action)創建和啟動一個Task
        //Task task2 = Task.Factory.StartNew(() =>
        //{
        //    Thread.Sleep(100);
        //    Debug.Log($"hello, task2的線程ID為{ Thread.CurrentThread.ManagedThreadId}");
        //});

        ////3.Task.Run(Action action)將任務放在線程池隊列，返回並啟動一個Task
        //Task task3 = Task.Run(() =>
        //{
        //    Thread.Sleep(100);
        //    Debug.Log($"hello, task3的線程ID為{ Thread.CurrentThread.ManagedThreadId}");
        //});


        //===========Task的延續操作(WhenAny/WhenAll/ContinueWith) 實現阻塞線程===============================================================
        /*
         Wait/WaitAny/WaitAll方法返回值為void，這些方法單純的實現阻塞線程。我們現在想讓所有task執行完畢(或者任一task執行完畢)後，開始執行後續操作，這時就可以用到WhenAny/WhenAll方法，
         這些方法執行完成返回一個task實例。task.WhenAll(Task[] tasks) 表示所有的task都執行完畢後再去執行後續的操作， task.WhenAny(Task[] tasks) 表示任一task執行完畢後就開始執行後續操作
        */
        Task task1 = new Task(() => {
            Debug.Log("進入線程1 準備 sleep");
            Thread.Sleep(500);
            Debug.Log("線程1執行完畢！");
        });
        task1.Start();
        Task task2 = new Task(() => {
            Debug.Log("進入線程2 準備 sleep");
            Thread.Sleep(3000);
            Debug.Log("線程2執行完畢！");
        });
        task2.Start();
        //task1，task2執行完了後執行後續操作
        Task.WhenAll(task1, task2).ContinueWith((t) => {
            Debug.Log("進入線程3 準備 sleep");
            Thread.Sleep(100);
            Debug.Log("執行後續操作完畢！");
        });

    }

    // Update is called once per frame
    void Update()
    {

    }
    void Test()
    {

        Debug.Log("Test Task");
    }
}
