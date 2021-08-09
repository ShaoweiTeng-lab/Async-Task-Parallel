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
        //=================Task�Ыؤ覡======================================================================================
        //Task�Ыؤ覡���T�� ,ThreadPool���౱��u�{�����涶�ǡA�ڭ̤]��������u�{�����u�{����/���`/�������q���A���঳�ĺʱ��M����u�{�������u�{
        //1.new�覡��ҤƤ@��Task�A�ݭn�q�LStart��k�Ұ�
        //Task task = new Task(() =>
        //{
        //    Thread.Sleep(100);
        //    Debug.Log($"hello, task1���u�{ID��{Thread.CurrentThread.ManagedThreadId}");
        //});
        //task.Start();

        ////2.Task.Factory.StartNew(Action action)�ЫةM�Ұʤ@��Task
        //Task task2 = Task.Factory.StartNew(() =>
        //{
        //    Thread.Sleep(100);
        //    Debug.Log($"hello, task2���u�{ID��{ Thread.CurrentThread.ManagedThreadId}");
        //});

        ////3.Task.Run(Action action)�N���ȩ�b�u�{�����C�A��^�ñҰʤ@��Task
        //Task task3 = Task.Run(() =>
        //{
        //    Thread.Sleep(100);
        //    Debug.Log($"hello, task3���u�{ID��{ Thread.CurrentThread.ManagedThreadId}");
        //});


        //===========Task������ާ@(WhenAny/WhenAll/ContinueWith) ��{����u�{===============================================================
        /*
         Wait/WaitAny/WaitAll��k��^�Ȭ�void�A�o�Ǥ�k��ª���{����u�{�C�ڭ̲{�b�Q���Ҧ�task���槹��(�Ϊ̥��@task���槹��)��A�}�l�������ާ@�A�o�ɴN�i�H�Ψ�WhenAny/WhenAll��k�A
         �o�Ǥ�k���槹����^�@��task��ҡCtask.WhenAll(Task[] tasks) ��ܩҦ���task�����槹����A�h������򪺾ާ@�A task.WhenAny(Task[] tasks) ��ܥ��@task���槹����N�}�l�������ާ@
        */
        Task task1 = new Task(() => {
            Debug.Log("�i�J�u�{1 �ǳ� sleep");
            Thread.Sleep(500);
            Debug.Log("�u�{1���槹���I");
        });
        task1.Start();
        Task task2 = new Task(() => {
            Debug.Log("�i�J�u�{2 �ǳ� sleep");
            Thread.Sleep(3000);
            Debug.Log("�u�{2���槹���I");
        });
        task2.Start();
        //task1�Atask2���槹�F��������ާ@
        Task.WhenAll(task1, task2).ContinueWith((t) => {
            Debug.Log("�i�J�u�{3 �ǳ� sleep");
            Thread.Sleep(100);
            Debug.Log("�������ާ@�����I");
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
