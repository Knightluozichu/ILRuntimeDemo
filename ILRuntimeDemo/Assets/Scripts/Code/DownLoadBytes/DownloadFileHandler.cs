using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace DownLoad
{
    /// <summary>
    /// 文件下载
    /// </summary>
    public class DownloadFileHandler : DownloadHandlerScript
    {

        private string saveFilePath; //保存路径
        private string fileName; //文件名
        private string pathName; //路径+名
        private string postfix = ".temp"; //临时文件后缀名

        /// <summary>
        /// 文件总长度
        /// </summary>
        int sumLength;
        /// <summary>
        /// 文件总长度
        /// </summary> 
        public int SumLength
        {
            get { return sumLength; }
        }
        /// <summary>
        /// 已下载长度
        /// </summary>
        private int nowLength;
        /// <summary>
        /// 已下载长度
        /// </summary>
        public int NowLength { get { return nowLength; } }

        /// <summary>
        /// 下载进度
        /// </summary>
        public float DownloadProgress
        {
            get
            {
                if (SumLength == 0)
                    return 0f;
                else
                    return (float)NowLength / SumLength;
            }
        }

        /// <summary>
        /// 下载完成
        /// </summary>
        new bool isDone = false;
        /// <summary>
        /// 下载完成
        /// </summary>
        public bool IsDone { get { return isDone; } }

        /// <summary>
        /// 每次下载的数据
        /// </summary>
        public byte[] DownloadDatas { get { return Datas.ToArray(); } }
        /// <summary>
        /// 每次下载的数据
        /// </summary>
        private List<byte> Datas = new List<byte>();
        /// <summary>
        /// 下载时间
        /// </summary>
        private float totalScends;
        private bool isdown;
        /// <summary>
        /// 实例方法
        /// </summary>
        /// <param name="saveFilePath">文件路径</param>
        /// <param name="fileName">文件名</param>
        public DownloadFileHandler(string saveFilePath, string fileName) : base(new byte[1024 * 200])  //限制下载的长度
        {
            this.saveFilePath = saveFilePath;
            this.fileName = fileName;

            pathName = saveFilePath + "/" + ErasePostfix(fileName) + postfix;
            isdown = true;
            nowLength = (int)GetFileLength(pathName);
        }

        /// <summary>
        /// 实例方法 该实例方法只限获取下载文件长度
        /// </summary>
        public DownloadFileHandler()
        {
            isdown = false;
            nowLength = 0;
        }

        /// <summary>
        /// 重写基类 获取到下载文件的长度
        /// </summary>
        /// <param name="contentLength"></param>
        [System.Obsolete]
        protected override void ReceiveContentLength(int contentLength)
        {
            //这里真坑  断点下载 下次获取的是未下载的长度 需要加上本地已经下载的长度 才是整个文件的总长度
            sumLength = contentLength + NowLength;
        }
        public float kb = 0;
        float starttime;
        /// <summary>
        ///  重写基类 正在下载 
        /// </summary>
        /// <param name="data">下载的数据 （不止有现在下载的还有前面下载的数据）</param>
        /// <param name="dataLength">当前下载的数据长度</param>
        /// <returns></returns>
        protected override bool ReceiveData(byte[] data, int dataLength)
        {
            if (starttime == 0)
            {
                starttime = Time.time;
            }

            if (!isdown)
                return false;

            for (int i = 0; i < dataLength; i++)
            {
                Datas.Add(data[i]);
            }
            nowLength += dataLength;
            totalScends = Time.time - starttime;
            WriteFile(pathName, data, dataLength);
            kb = 0;
            if (totalScends != 0)
            {
                kb = nowLength / totalScends / 1024f / 1024f;
            }
            else
            {
                kb = nowLength / 1024f / 1024f;
            }
            Debug.Log("   下载的长度--- " + NowLength + "   总时间====" + totalScends);
            Debug.Log("   下载的长度" + NowLength + "   总长度" + SumLength + "进度：" + DownloadDatas.Length);
            Debug.Log("   下载的长度---" + NowLength + "   总长度====" + SumLength);
            Debug.Log(kb.ToString("f1") + "mb/s");
            return true;
        }

        protected override void CompleteContent()
        {
            ChangeName();
            isDone = true;
            Debug.Log("11下载完成！");
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="Path_Name"></param>
        /// <param name="dates"></param>
        /// <param name="insertPosition"></param>
        private void WriteFile(string Path_Name, byte[] dates, int length)
        {

            //using (var sw = File.CreateText(Path_Name))
            //{
            //    sw.Write(dates);
            //}



            FileStream fs;

            if (!File.Exists(Path_Name))
                fs = File.Create(Path_Name, length, FileOptions.Asynchronous);
            else
                fs = File.OpenWrite(Path_Name);
            long ength = fs.Length;

            fs.Seek(ength, SeekOrigin.Current);
            fs.Write(dates, 0, length);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        /// <summary>
        /// 改名
        /// </summary> 
        private void ChangeName()
        {
            string filepathName = saveFilePath + "/" + fileName;
            if (File.Exists(filepathName))
                File.Delete(filepathName);
            File.Move(pathName, filepathName);
        }

        /// <summary>
        /// 获取文件长度
        /// </summary>
        /// <param name="Path_Name"></param>
        /// <returns></returns> 
        public static long GetFileLength(string Path_Name)
        {
            if (!File.Exists(Path_Name))
                return 0;
            FileStream fs = File.OpenWrite(Path_Name);
            long ength = fs.Length;
            fs.Close();
            fs.Dispose();
            return ength;
        }

        /// <summary>
        /// 去掉‘.’后面的字符
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string ErasePostfix(string filePath)
        {
            return filePath.Substring(0, filePath.LastIndexOf('.'));
        }
    }
}