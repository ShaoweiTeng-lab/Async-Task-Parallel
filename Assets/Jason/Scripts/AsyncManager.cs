using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;//�D�P�B�ϥ�
using System.IO;
using System.Text;

public class AsyncManager : MonoBehaviour
{/*
   �P�B�M���B�D�n�Ω�׹���k�C

   �P�B:
   ��@�Ӥ�k�Q�եήɡA�եΪ̻ݭn���ݸӤ�k���槹���ê�^�~���~�����A�ڭ̺ٳo�Ӥ�k�O�P�B��k�F
   ------------------------------------------------------------------------------------------------------------------------------------------------
   ���B:
   ��@�Ӥ�k�Q�եήɥߧY��^�A������@�ӽu�{����Ӥ�k�������~�ȡA�եΪ̤��ε��ݸӤ�k���槹���A�ڭ̺ٳo�Ӥ�k�����B��k�C
   ------------------------------------------------------------------------------------------------------------------------------------------------
�@ ���B���n�B�b��D����(�եνu�{���|�Ȱ�����h���ݤl�u�{����)�A�]���ڭ̧�@�Ǥ��ݭn�ߧY�ϥε��G�B���Ӯɪ����ȳ]�����B����A�i�H�����{�Ǫ��B��Ĳv 
   ===============================================================================================================================================
   async �^�ǫ��O
   https://www.youtube.com/watch?v=gxaJyuf-2dI
   async �����P await�f�t, await�i�^�ǭ� ��_coritine ���Φh
   await �N��O ���ݦ��浲�����~���U����(�e���O �Ӥ�k �R�W�� async)
   �ŧi�� async ��.NET ��k�����Ǧ^(await _)�H�U�T�ث��O���@�G
   task = �@������ �i�����ϥ� ���@�w�n��bvoid ��
   1. Task
         �@�~�����ɱN�����v�ٵ��I�s��
   2. Task<T>
         �@�~�����ɦ^�ǫ��O�� T �����󵹩I�s��
   3. void
        �Įg�ᤣ�z(Fire-and-Forget)���ǡA�I�s��Y���h�x��
     Start is called before the first frame update
    */
   void Start()
    {

        #region �D�P�B����
        //AsyncFunction();
        //NoAsync();
        #endregion
        #region Task���B����
        //NoAsync();
        //TaskRun();
        #endregion
        #region �����Ӱj�� + ���B 
        //AsyncFunction();
        //NoAsync();
        //NoAsync2();
        #endregion ���B ���o���
        #region  ���B���o��� 
        //AsyncGetString();
        //NoAsync();
        #endregion
        #region ���� task
        //task_1();
        //Debug.Log("Hello World");//���|���ݪ������� ���M�b task ���|���� ��� ���O �~�����|��task���� �A���D �� start �[�W async �åB ��task �[�Wawait
        #endregion
        #region async task
        //taskFunction();
        #endregion
        #region await task delay
        //TaskRun();
        #endregion
        #region �ϥ� task �o�S�ϥ� async
        //�S�[async ����k ���|���ݰ���
        VoidNoAsyncFunc();
        #endregion


    }


    #region ���`�P�B ��k���ݰ��槹�~���U����
    void NoAsync()
    {
        Debug.Log("�}�l�P�B����");
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(string.Format("�j������ {0}  ��", i));
        }
        Debug.Log("�P�B���槹 �j��");
    }
    void NoAsync2()
    {
        Debug.Log("�}�l�P�B2����");
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(string.Format("�P�B2�j������ {0}  ��", i));
        }
        Debug.Log("�P�B2���槹 �j��");
    }
    #endregion

    #region �D�P�B���� �J��await ���^�D�{�� ����L��k�u������ �� coritine �� yield
    async void AsyncFunction()
    {   // �P�@��k�� �Y���O task�t�~�I�s�h����k���^�����᩹�U����
        Debug.Log("�}�l �D�P�B����");
        for (int i = 0; i < 3; i++)
        {
            await Task.Delay(1000); //����D�j�鵥��k���槹
            Debug.Log(string.Format("�D�P�B�j������ {0}  ��", i));
        }
        Debug.Log("�D�P�B���槹 �j��");

    }
    #endregion

    #region task�t�~�g�ŧi �i�����U����
    async void taskFunction()
    {
        Task M_aSYNC = new Task(() =>{ m_async(); });
        M_aSYNC.Start();
        //Task.Run(()=>{ m_async(); });
        Debug.Log("async���椤");
    }
    #endregion

    /// <summary>
    /// �^��task
    /// </summary>
    /// <returns></returns>
    async Task m_async()
    {
        await Task.Delay(5000);
        Debug.Log("m_async ���槹");
       
    }
    async void AsyncGetString()
    {
        Debug.Log("GetString �}�l");
        string getString = await strAsync("strAsync");
        Debug.Log("GetString :�@"+getString);
    }
    ///�i�^�ǭ�
    /// <summary>
    /// �^��task �x��
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    async Task<string> strAsync(string GetString)
    {
        await Task.Delay(3000);
        return GetString;
    }

    async Task task_1()
    {
        Debug.Log("start wait");
        await Task.Delay(3000);

        Debug.Log("Finish");

    }
    async void TaskRun()
    {
        Debug.Log("Start Task Run");
        await Task.Delay(3000);
        Debug.Log("Task wait finish");




    }
    /// <summary>
    /// Void ���[ async �u�b ��k�����I�s����
    /// </summary>
    void VoidNoAsyncFunc()
    {
        Debug.Log("Function ����");
        VoidNoAsync();//�Y�n���ݦ���k���槹�~�~�� �h����ϥ�await �� async �A�_�h�����U����
        Debug.Log("Function ���浲��");
    }
    async Task VoidNoAsync()
    {
        Debug.Log("VoidNoAsync start");
        await Task.Delay(3000);
        Debug.Log("VoidNoAsync finish");
    }

    #region ���BŪ����󤺮e
    //���BŪ����󤺮e
    async static Task<string> GetContentAsync(string filename)
    {

        FileStream fs = new FileStream(filename, FileMode.Open);
        var bytes = new byte[fs.Length];
        //ReadAync��k���BŪ�����e�A������u�{
        Debug.Log("�}�lŪ�����");
        int len = await fs.ReadAsync(bytes, 0, bytes.Length);
        string result = Encoding.UTF8.GetString(bytes);
        return result;
    }
    //�P�BŪ����󤺮e
    static string GetContent(string filename)
    {
        FileStream fs = new FileStream(filename, FileMode.Open);
        var bytes = new byte[fs.Length];
        //Read��k�P�BŪ�����e�A����u�{
        int len = fs.Read(bytes, 0, bytes.Length);
        string result = Encoding.UTF8.GetString(bytes);
        return result;
    }
    #endregion
}
