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
    private static SemaphoreSlim Locker = new SemaphoreSlim(1);//���� �C���i�J������� �ƶq
    public Button Btn;
    void Start()
    {
         
       // Btn.onClick.AddListener(()=> { Excute(); });
        ListTask();
    }
 
    async Task DoWork()
    {//�ϥή�Task�Φb�i�ή�Task<T>�l�רϥ�async / await
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
        await Locker.WaitAsync();//�H�D�P�B�覡���Զi�J SemaphoreSlim
        
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
        await Task.WhenAll(TaskList); //���ݥ�������
        
        Debug.Log("Finish");
    }

}
