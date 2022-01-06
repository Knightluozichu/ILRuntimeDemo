using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace DownLoad
{
    /// <summary>
    /// 下载文件之断点下载
    /// </summary>
    public class DownLoadFile : MonoBehaviour
    {
        /// <summary>
        /// 单例
        /// </summary>
        static DownLoadFile instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static DownLoadFile Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject game = new GameObject("DownLoadResource");
                    DontDestroyOnLoad(game);
                    instance = game.AddComponent<DownLoadFile>();
                }
                return instance;
            }
        }

        /// <summary>
        /// 下载链接字典
        /// </summary>
        Dictionary<string, UnityWebRequest> downReqMap = new Dictionary<string, UnityWebRequest>();
        /// <summary>
        /// 携程列表
        /// </summary>
        List<Coroutine> coroutines = new List<Coroutine>();

        /// <summary>
        /// 下载资源
        /// </summary>
        /// <param name="downPath">下载地址</param>
        /// <param name="savePath">保存地址（不包含文件名）</param>
        /// <param name="callback">下载进度回调 返回 0-1之间的小数</param>
        public void DownLoadRes(string downPath, string savePath, Callback<float, float> callback)
        {
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);   //创建保存地址文件夹

            if (!downReqMap.ContainsKey(downPath))   //判断当前连接是否有下载
            {
                coroutines.Add(StartCoroutine(IDownLoadRes(downPath, savePath, callback)));
            }
        }

        /// <summary>
        ///  下载资源携程
        /// </summary>
        /// <param name="downPath">下载地址</param>
        /// <param name="savePath">保存地址（不包含文件名）</param>
        /// <param name="callback">下载进度回调 返回 0-1之间的小数</param>
        /// <returns></returns>
        IEnumerator IDownLoadRes(string downPath, string savePath, Callback<float, float> callback)
        {
            //获取文件名
            string fileName = downPath.Split('/')[downPath.Split('/').Length - 1];

            //创建网络请求
            UnityWebRequest unityWeb = UnityWebRequest.Get(downPath);
            //新建下载文件句柄
            DownloadFileHandler downloadFile = new DownloadFileHandler(savePath, fileName);

            unityWeb.downloadHandler = downloadFile;
            long length = downloadFile.NowLength;

            //设置开始下载文件从什么位置开始
            unityWeb.SetRequestHeader("Range", "bytes=" + length + "-");//这句很重要

            unityWeb.SendWebRequest();

            downReqMap.Add(downPath, unityWeb);

            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (callback != null)
                {
                    if (downloadFile.DownloadProgress < 1)
                    {
                        callback.Invoke(downloadFile.DownloadProgress,downloadFile.kb);
                    }
                }
                if (downloadFile.IsDone)
                {
                    Debug.Log("下载完成！");
                    downReqMap.Remove(downPath);
                    callback.Invoke(1, downloadFile.kb);
                    break;
                }
            }
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="UrlFilePath">网络文件地址</param>
        /// <param name="callback">返回  文件字节数</param> 
        public void GetFileSize(string UrlFilePath, Callback<float> callback)
        {
            StartCoroutine(IGetFileSize(UrlFilePath, callback));
        }
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="UrlFilePath">网络文件地址</param>
        /// <param name="callback">返回  文件字节数</param>
        IEnumerator IGetFileSize(string UrlFilePath, Callback<float> callback)
        {
            UnityWebRequest unityWeb = UnityWebRequest.Get(UrlFilePath);
            DownloadFileHandler downloadFile = new DownloadFileHandler();
            unityWeb.downloadHandler = downloadFile;

            yield return unityWeb.SendWebRequest();
            if (callback != null)
            {
                callback.Invoke(downloadFile.SumLength);
            }
        }

        /// <summary>
        /// 停止下载
        /// </summary>
        public void StopDownLoad()
        {
            for (int i = 0; i < coroutines.Count; i++)
            {
                StopCoroutine(coroutines[i]);
            }
            coroutines.Clear();

            foreach (var item in downReqMap.Values)
            {
                item.Abort();//中止下载  
                item.Dispose();  //释放
            }
            downReqMap.Clear();
        }


        private void OnDestroy()
        {
            StopDownLoad();
        }
    }
}