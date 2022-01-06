using DownLoad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CheckDll : MonoBehaviour
{

    enum Routing
    {
        URL_VERSION,
        URL_DLL,
    }


    Dictionary<Routing, string> routing = new Dictionary<Routing, string>()
    {
        { Routing.URL_VERSION,"/version.txt"},
        { Routing.URL_DLL,"/hotfix.dll"},
    };

    public string hotfixAddress;
    private float progress = 0;
    // Start is called before the first frame update
    void Start()
    {
        //加载本地的version
        //检查MD5
        //下载dll
        //替换dll
        //进入samplescene
        testss += testeee;

        var version = Resources.Load<TextAsset>("version");

        if (version == null)
        {
            Debug.LogError("version 读取不了，请检查！");
            return;
        }

        var localMD5 = version.text;

        StartCoroutine(GetRemoteVersion(localMD5));
    }

    private void testeee(float progess, float kb)
    {
        progress = progess;

        Debug.Log("进度:" + (progess * 100f).ToString("f2") + "%");

        Debug.Log("网速:" + kb.ToString("f2") + "MB/s");

    }

    UnityWebRequest unityWebRequest;//version
    UnityWebRequest unityWebRequest_dll;//dll
    Callback<float, float> testss;

    IEnumerator GetRemoteVersion(string localMD5)
    {
        unityWebRequest = UnityWebRequest.Get(hotfixAddress + routing[Routing.URL_VERSION]);


        if (unityWebRequest != null)
        {
             unityWebRequest.SendWebRequest();

            while (!unityWebRequest.isDone)
            {
                Debug.Log(unityWebRequest.downloadProgress);
                yield return 1;
            }

            if (unityWebRequest.isHttpError || unityWebRequest.isNetworkError)
                Debug.Log(unityWebRequest.error);
            else
            {
                Debug.Log(unityWebRequest.downloadHandler.text);
            }

            var remoteMD5 = unityWebRequest.downloadHandler.text;

            Debug.Log("远程MD5码:" + remoteMD5.ToString() + "\t 本地MD5码:" + localMD5);

            if (localMD5.Equals(remoteMD5))
            {
                //跳转场景；
                Debug.Log("版本一致。");
            }
            else
            {
                Debug.Log("版本不一致，进入更新下载。");

                //var local_dll_path = Path.Combine(Application.persistentDataPath, "hotfix.dll");

                var local_dll_path = Application.persistentDataPath;

                DownLoadFile.Instance.DownLoadRes(hotfixAddress + routing[Routing.URL_DLL], local_dll_path, testss);

                while(progress < 1)
                {
                    yield return null;
                }

                File.Delete(Path.Combine(Application.streamingAssetsPath, "hotfix_dll", "hotfix.dll"));

   
                File.Move(Path.Combine(Application.persistentDataPath, "hotfix.dll"), Path.Combine(Application.streamingAssetsPath, "hotfix_dll", "hotfix.dll"));

                File.WriteAllText(Path.Combine(Application.dataPath, "Resources", "version.txt"), remoteMD5);


                Debug.Log("更新完毕。");

               
            }

            unityWebRequest.Dispose();

            Debug.Log("进入游戏.");

            SceneManager.LoadScene("SampleScene");
        }

    }

    private void Update()
    {
        
    }



}
