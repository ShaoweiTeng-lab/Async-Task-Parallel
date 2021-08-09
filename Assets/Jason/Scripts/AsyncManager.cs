using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;//非同步使用
public class AsyncManager : MonoBehaviour
{
    //   async 回傳型別
    //https://www.youtube.com/watch?v=gxaJyuf-2dI
    //async 必須與 await搭配 ,await可回傳值 比起coritine 有用多
    // await 意思是 等待此行結束玩才往下執行 (前提是 該方法 命名為 async )
    //宣告為 async 的.NET 方法必須傳回(await _)以下三種型別之一：
    // task = 一項任務 可直接使用 不一定要放在void 中
    //1. Task
    //     作業結束時將控制權還給呼叫端
    //2. Task<T>
    //     作業結束時回傳型別為 T 的物件給呼叫端
    //3. void
    //    採射後不理(Fire-and-Forget)哲學，呼叫後即失去掌握 
    // Start is called before the first frame update
    void Start()
    {

        //AsyncFunction();

        //AsyncGetString();
        //TaskRun();
        //NoAsync();
        //NoAsync2();
        //ShowNovoid();
        //taskFunction();
        //TaskRun();
        //沒加async 之方法 不會等待執行
        VoidNoAsyncFunc();
        //NoAsync();
       

    }


    #region 正常同步 方法等待執行完才往下執行
    void NoAsync()
    {
        Debug.Log("開始同步執行");
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(string.Format("迴圈執行第 {0}  圈", i));
        }
        Debug.Log("同步執行完 迴圈");
    }
    void NoAsync2()
    {
        Debug.Log("開始同步2執行");
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(string.Format("同步2迴圈執行第 {0}  圈", i));
        }
        Debug.Log("同步2執行完 迴圈");
    }
    #endregion

    #region 非同步執行 遇到await 切回主程式 讓其他方法優先執行 像 coritine 的 yield
    async void AsyncFunction()
    {   // 同一方法內 若不是 task另外呼叫則此方法此回圈執行後往下執行
        Debug.Log("開始 非同步執行");
        for (int i = 0; i < 3; i++)
        {
            await Task.Delay(1000); //跳到主迴圈等方法執行完
            Debug.Log(string.Format("非同步迴圈執行第 {0}  圈", i));
        }
        Debug.Log("非同步執行完 迴圈");

    }
    #endregion

    #region task另外寫宣告 可先往下執行
    async void taskFunction() {
        Task M_aSYNC = m_async();
        Debug.Log("async執行中"); 
    }
    #endregion
   
    /// <summary>
    /// 回傳task
    /// </summary>
    /// <returns></returns>
    async Task m_async()
    {
        await Task.Delay(5000);
        Debug.Log("m_async 執行完");

    }
    async void AsyncGetString() {
        Debug.Log("GetString 開始");
        string getString = await strAsync("strAsync");
        Debug.Log(getString);
    }
    ///可回傳值
    /// <summary>
    /// 回傳task 泛型
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    async Task<string> strAsync(string GetString) {
        await Task.Delay(3000); 
        return GetString;
    }

    async Task ShowNovoid() {
        Debug.Log("start wait");
        await Task.Delay(3000);

        Debug.Log("Finish");
    
    }
    async void TaskRun() {
        await Task.Run(()=>{
            Task.Delay(3000);
            Debug.Log("wait finish");
        
        
        
        });
    
    }
    /// <summary>
    /// Void 不加 async 只在 方法內部呼叫任務
    /// </summary>
   void VoidNoAsyncFunc() {
        Debug.Log("Function 執行");
        VoidNoAsync();//若要等待此方法執行完才繼續 則必續使用await 及 async ，否則先往下執行
        Debug.Log("Function 執行結束");
    }
    async Task VoidNoAsync() {
        Debug.Log("VoidNoAsync start");
        await Task.Delay(3000);
        Debug.Log("VoidNoAsync finish");
    }
}
