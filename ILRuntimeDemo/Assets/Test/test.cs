using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using RealisticEyeMovements;
public class test : MonoBehaviour
{
    public Button button1;
    AsyncCallback asyncCallback;
    // Start is called before the first frame update
    private void Start()
    {

        //GetComponent<LookTargetController>().LookAtPoiDirectly(GameObject.Find("GameObject").transform);
        //test1Webclient();
        //button1.onClick.AddListener(delegate
        //{
        //    Debug.Log("click");
        //});

        //button1.transition = Selectable.Transition.ColorTint;
        //// button1. = Selectable.SelectionState.Pressed;
        //ColorBlock cb = new ColorBlock();
        //cb.normalColor = Color.red;
        //cb.highlightedColor = Color.green;
        //cb.pressedColor = Color.blue;
        //cb.disabledColor = Color.black;
        //button1.colors = cb;


    }
    private void OnGUI()
    {
        if (GUILayout.Button("Auto Button click"))
        {
            ExecuteEvents.Execute<IPointerClickHandler>(button1.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        }
        if (GUILayout.Button("Auto Button submit"))
        {
            //按钮点击的变色
            ExecuteEvents.Execute<ISubmitHandler>(button1.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
        if (GUILayout.Button("Auto Button down"))
        {
            //按钮点击的变色
            ExecuteEvents.Execute<IPointerDownHandler>(button1.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }
        if (GUILayout.Button("Auto Button enter"))
        {
            //按钮点击的变色
            ExecuteEvents.Execute<IPointerEnterHandler>(button1.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
        }
        if (GUILayout.Button("Auto Button exit"))
        {
            //按钮点击的变色
            ExecuteEvents.Execute<IPointerExitHandler>(button1.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
        }
        if (GUILayout.Button("Auto Button up"))
        {
            //按钮点击的变色
            ExecuteEvents.Execute<IPointerUpHandler>(button1.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
        }
    }

    // Update is called once per frame

    string currDownFile = "test.bundle";
    string url = "http://192.168.10.33/test/StandaloneWindows64/";

    private void test1Webclient()
    {
        using (WebClient client = new WebClient())
        {
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            client.DownloadFileAsync(new System.Uri(url), currDownFile);
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
