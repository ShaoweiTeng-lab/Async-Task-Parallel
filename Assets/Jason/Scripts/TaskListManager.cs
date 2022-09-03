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
         
        Btn.onClick.AddListener(Excute);
        ListTask();
    }
 
    async Task DoWork() {
        Debug.Log("Hi" + Task.CurrentId);
        await Task.Delay(0);

    }
    async void Excute() {
        await Locker.WaitAsync();//�H�D�P�B�覡���Զi�J SemaphoreSlim
        
        Debug.Log("Excute  : " );
        await Task.Delay(5000);
        Locker.Release();
    }
    async void ListTask()
    {   
        for (int i = 0; i < 5; i++)
        {
            TaskList.Add(DoWork());
            await Task.Delay(1000);
        }
        await Task.WhenAll(TaskList);
        
        Debug.Log("Finish");
    }

}
