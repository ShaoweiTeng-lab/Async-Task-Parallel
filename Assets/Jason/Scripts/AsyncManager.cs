using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;//�D�P�B�ϥ�
public class AsyncManager : MonoBehaviour
{/*
   �P�B�M���B�D�n�Ω�׹���k�C

   �P�B:
   ���@�Ӥ�k�Q�եήɡA�եΪ̻ݭn���ݸӤ�k���槹���ê�^�~���~�����A�ڭ̺ٳo�Ӥ�k�O�P�B��k�F
   ------------------------------------------------------------------------------------------------------------------------------------------------
   ���B:
   ���@�Ӥ�k�Q�եήɥߧY��^�A������@�ӽu�{����Ӥ�k�������~�ȡA�եΪ̤��ε��ݸӤ�k���槹���A�ڭ̺ٳo�Ӥ�k�����B��k�C
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

        //AsyncFunction();

        //AsyncGetString();
        //TaskRun();
        //NoAsync();
        //NoAsync2();
        //ShowNovoid();
        //taskFunction();
        //TaskRun();
        //�S�[async ����k ���|���ݰ���
        VoidNoAsyncFunc();
        //NoAsync();


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
        Task M_aSYNC = m_async();
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
        Debug.Log(getString);
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

    async Task ShowNovoid()
    {
        Debug.Log("start wait");
        await Task.Delay(3000);

        Debug.Log("Finish");

    }
    async void TaskRun()
    {
        await Task.Run(() =>
        {
            Task.Delay(3000);
            Debug.Log("wait finish");



        });

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
}