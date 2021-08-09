using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;
using System.Threading;
public class TaskManager : MonoBehaviour
{   /*
    Task�O�bThreadPool����¦�W���X���AThreadPool�����Y�F�ƶq���u�{�A�p�G�����Ȼݭn�B�z�ɡA�|�q�u�{��������@�ӪŶ����u�{�Ӱ�����ȡA���Ȱ��槹����u�{���|�P���A
    �ӬO�Q�u�{���^���H�ѫ�����ȨϥΡC��u�{�����Ҧ����u�{���b���L�ɡA�S���s���ȭn�B�z�ɡA�u�{���~�|�s�ؤ@�ӽu�{�ӳB�z�ӥ��ȡA�p�G�u�{�ƶq�F��]�m���̤j�ȡA����
    �|�ƶ��A���ݨ�L��������u�{��A����C�u�{�����ֽu�{���ЫءA�`�ٶ}�P   
    */
    // Start is called before the first frame update
    void Start()
    {

        //Task t1 = new Task(() => { Debug.Log("t1 Start new Task"); });//��̬O�ӧO�}�s����� �ҥH���涶�Ǥ��@�w
        //Task tastVoid = new Task(Test);
        //t1.Start();
        //tastVoid.Start();
        //======================================
        //Task�Ыؤ覡���T�� ,ThreadPool���౱��u�{�����涶�ǡA�ڭ̤]��������u�{�����u�{����/���`/�������q���A���঳�ĺʱ��M����u�{�������u�{
        //1.new�覡��ҤƤ@��Task�A�ݭn�q�LStart��k�Ұ�
        Task task = new Task(() =>
        {
            Thread.Sleep(100);
            Debug.Log($"hello, task1���u�{ID��{Thread.CurrentThread.ManagedThreadId}");
        });
        task.Start();

        //2.Task.Factory.StartNew(Action action)�ЫةM�Ұʤ@��Task
        Task task2 = Task.Factory.StartNew(() =>
        {
            Thread.Sleep(100);
            Debug.Log($"hello, task2���u�{ID��{ Thread.CurrentThread.ManagedThreadId}");
        });

        //3.Task.Run(Action action)�N���ȩ�b�u�{�����C�A��^�ñҰʤ@��Task
        Task task3 = Task.Run(() =>
        {
            Thread.Sleep(100);
            Debug.Log($"hello, task3���u�{ID��{ Thread.CurrentThread.ManagedThreadId}");
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
