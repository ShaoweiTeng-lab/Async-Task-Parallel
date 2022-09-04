using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine.UI;

public class TaskListManager : MonoBehaviour
{
    public List<Task> TaskList = new List<Task>();
    private static SemaphoreSlim Locker = new SemaphoreSlim(1);// –Ω秈磅︽狐ぇ 计秖
    public Button Btn;
    void Start()
    {
         
       // Btn.onClick.AddListener(()=> { Excute(); });
        ListTask();
    }
 
    async Task DoWork()
    {//ㄏノTask┪ノTask<T>﹍沧ㄏノasync / await
        await Locker.WaitAsync();
        var tasklist = new List<Task>() { 
            Task.Run(async ()=>{  
            await Task.Delay(3000);
             Debug.Log("Hi");
            })
        };
        Locker.Release();
        await Task.WhenAll(tasklist);

    }
    async Task Excute() {
        await Locker.WaitAsync();//獶˙よΑ单秈 SemaphoreSlim
        
        Debug.Log("Excute  : " );
        await Task.Delay(5000);
        Locker.Release();
    }
    async void ListTask()
    {
       
        for (int i = 0; i < 5; i++)
        {
            Debug.Log($"{i}   Start");
            await Task.Delay(1000);
            TaskList.Add(DoWork());
        }
        await Task.WhenAll(TaskList); //单场磅︽
        
        Debug.Log("Finish");
    }

}
