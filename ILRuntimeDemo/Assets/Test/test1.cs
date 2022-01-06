using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using DownLoad;
public class test1 : MonoBehaviour
{
    UnityWebRequest m_webRequest;
    string currDownFile = "test.bundle";
    //string urls = "http://127.0.0.1/test/StandaloneWindows64/test.bundle";
    string urls = "file:///D:/RemoteRes/hotfix.dll";
    Callback<float, float> testss;
    private void Start()
    {
        testss += testeee;

        DownLoadFile.Instance.DownLoadRes(urls, @"E:/XiaoXue/", testss);
    }

    private void testeee(float progess, float kb)
    {

        Debug.Log("进度:" + (progess * 100f).ToString("f1") + "%");

        Debug.Log("网速:" + kb.ToString("f1") + "mb/s");

    }

    public void StartDownload(Action callback = null)
    {

        StartCoroutine(Download(callback));
    }

    IEnumerator Download(Action callback = null)
    {
        m_webRequest = UnityWebRequest.Get(urls + currDownFile);
        //StartDownload = true;
        m_webRequest.timeout = 30;  //设置超时，若m_webRequest.SendWebRequest()连接超时会返回，且isNetworkError为true
        yield return m_webRequest.SendWebRequest();
        //m_isStartDownload = false;

        if (m_webRequest.isNetworkError)
        {
            Debug.Log("Download Error:" + m_webRequest.error);
        }
        else
        {
            byte[] bytes = m_webRequest.downloadHandler.data;
            //创建文件
            //   FileTool.CreatFile(m_saveFilePath, bytes);
        }

        if (callback != null)
        {
            callback();
        }
    }

    public float GetProcess()
    {
        if (m_webRequest != null)
        {
            return m_webRequest.downloadProgress;
        }
        return 0;
    }

    public long GetCurrentLength()
    {
        if (m_webRequest != null)
        {
            return (long)m_webRequest.downloadedBytes;
        }
        return 0;
    }

    public long GetLength()
    {
        return 0;
    }

    public void ondestory()
    {
        if (m_webRequest != null)
        {
            m_webRequest.Dispose();
            m_webRequest = null;
        }
    }

    // Update is called once per frame

    // string currDownFile = "test.bundle";
    //  string url = "http://192.168.10.33/test/StandaloneWindows64/";

    private void test1Webclient()
    {
        using (WebClient client = new WebClient())
        {
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            client.DownloadFileAsync(new System.Uri(urls), currDownFile);
            //client.DownloadFileCompleted += DownLoadCompleted;
        }

    }

    private void DownLoadCompleted(object sender, AsyncCompletedEventArgs e)
    {
        Debug.Log("文件下载完成!");
    }

    private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        //下载的总量
        //PrecentData preData = new PrecentData();
        string total = string.Format("{0} MB / {1} MB", (e.BytesReceived / 1024d / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        Debug.Log("进度1---MB----:" + total);
        float precent = (float)e.BytesReceived / (float)e.TotalBytesToReceive;
        Debug.Log("进度2---%----:" + precent);


        string value = string.Format("{0} kb/s", (e.BytesReceived / 1024d / 1024d).ToString("0.00"));
        string speed = value;
        Debug.Log("进度3---kb/s----:" + value);
        //Loom.QueueOnMainThread((param) =>
        //{
        //    NotificationCenter.Get().ObjDispatchEvent(KEventKey.m_evDownload, preData);
        //}, null);


        //NotiData data = new NotiData(NotiConst.UPDATE_PROGRESS, value);
        //if (m_SyncEvent != null) m_SyncEvent(data);

        //if (e.ProgressPercentage == 100 && e.BytesReceived == e.TotalBytesToReceive)
        //{
        //    sw.Reset();

        //    data = new NotiData(NotiConst.UPDATE_DOWNLOAD, currDownFile);
        //    if (m_SyncEvent != null) m_SyncEvent(data);
        //}
    }
}
