
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Linq;
using System.Reflection;
//using ZenFulcrum.EmbeddedBrowser;
using System.Text;

public class Test : Editor
{
    [MenuItem("添加组件和记录pos位置/移除丢失的脚本")]
    public static void RemoveMissingScript()
    {
        var gos = GameObject.FindObjectsOfType<GameObject>();
        foreach (var item in gos)
        {
            Debug.Log(item.name);
            SerializedObject so = new SerializedObject(item);
            var soProperties = so.FindProperty("m_Component");
            var components = item.GetComponents<Component>();
            int propertyIndex = 0;
            foreach (var c in components)
            {
                if (c == null)
                {
                    soProperties.DeleteArrayElementAtIndex(propertyIndex);
                }
                ++propertyIndex;
            }
            so.ApplyModifiedProperties();
        }

        AssetDatabase.Refresh();
        Debug.Log("清理完成!");

    }

    [MenuItem("添加组件和记录pos位置/添加碰撞体和发光脚本(编辑场景所有物体(中文名字))")]
    static void 添加组件()
    {
        //记录中文名字
        StringBuilder content = new StringBuilder();
        //创建文件
        WriteFile("indexChinese.txt", "");

        // List<string> TempChineselist = new List<string>();
        //查找场景中所有的物体
        foreach (GameObject objj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {   //检测到---不可交互
            if (objj.tag != "可交互")
            {
                //检测到中文
                if (checkString(objj.transform.name))
                {
                    if (objj.transform.name != "弹窗位置" && objj.transform.name != "箭头"
                   && !objj.transform.name.StartsWith("前") && !objj.transform.name.StartsWith("后"))
                    {
                        //改变tag
                        objj.tag = "可交互";
                        //添加boxcollider
                        if (!objj.GetComponent<BoxCollider>())
                        {
                            content.Append("," + objj.transform.name);

                            GameObject tcobj = new GameObject("弹窗位置");
                            tcobj.transform.parent = objj.transform;
                            tcobj.transform.localPosition = Vector3.zero + new Vector3(0, 1.2f, 0);
                            Debug.Log(objj.name);
                            objj.AddComponent<BoxCollider>();
                        }
                        //highlighting
                        //if (!objj.GetComponent<HighlightableObject>())
                        //{
                        //    objj.AddComponent<HighlightableObject>();
                        //}
                        #region MyRegion
                        //else if (objj.GetComponents<BoxCollider>().Length > 1)
                        //{
                        //    Debug.Log("有多个，box");
                        //    DestroyImmediate(objj.GetComponents<BoxCollider>()[0]);
                        //}
                        //else if (objj.GetComponents<HighlightableObject>().Length > 1) //
                        //{
                        //    Debug.Log("有多个，hight");
                        //    DestroyImmediate(objj.GetComponents<HighlightableObject>()[0]);
                        //}
                        #endregion

                    }
                }
            }
            else
            {
                Debug.Log("这是可交互物体:----" + objj.transform.name);
            }
        }
        if (!string.IsNullOrEmpty(content.ToString()))
        {
            WriteFile("indexChinese.txt", content.ToString());
        }

    }

    /// <summary>
    ///     // 判断 当前字符是否为中文
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static bool isChinese(char c)
    {
        return c >= 0x4E00 && c <= 0x9FA5;
    }
    public static bool checkString(string str)
    {
        char[] ch = str.ToCharArray();
        if (str != null)
        {
            for (int i = 0; i < ch.Length; i++)
            {
                if (isChinese(ch[i]))
                {
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// 字典 pos
    /// </summary>
    // static Dictionary<int, positem> PosArryDic = new Dictionary<int, positem>();
    /// <summary>
    /// 记录为位置点
    /// </summary>
    //[MenuItem("添加组件和记录pos位置/记录位置（jsonunity 必须pos0 前0 后0）")]
    static void RecordPos()
    {
        posIndex = 0;
        //第一次创建文件
        WriteFile("pos.json", "");

        GameObject[] PosObjArry = GameObject.FindGameObjectsWithTag("pos");
        List<GameObject> objList = PosObjArry.ToList();

        posarry posList = new posarry();
        posList.poslist = new List<positem>();

        PosCun(objList, posList);

        string jsoninfo = JsonUtility.ToJson(posList);
        WriteFile("pos.json", jsoninfo);

    }
    static int posIndex = 0;
    /// <summary>
    /// 存一个集合
    /// </summary>
    /// <param name="posarry"></param>
    /// <param name="posList"></param>
    static void PosCun(List<GameObject> objList, posarry posList)
    {
        Debug.Log("posIndex:" + posIndex);

        if (objList.Count > 0)
        {
            //Debug.Log("数组数量: " + objList.Count);
            List<GameObject> templist = new List<GameObject>();
            for (int i = 0; i < objList.Count; i++)
            {
                string str = objList[i].name.Substring(objList[i].name.Length - 1);
                int index = int.Parse(str);
                // Debug.Log("位置:" + str);
                if (index == posIndex)
                {
                    templist.Add(objList[i]);
                    if (templist.Count > 2)   // 证明已经 存3个 元素了 --- 打断循环
                    {
                        bianlilist(templist, posList);
                        PosCun(objList, posList);
                    }

                }
            }
        }
    }
    //static void PosCun(List<GameObject> objList, JSONNode jsonList)
    //{
    //    Debug.Log("posIndex:" + posIndex);
    //    if (objList.Count > 0)
    //    {
    //        //Debug.Log("数组数量: " + objList.Count);
    //        List<GameObject> templist = new List<GameObject>();
    //        for (int i = 0; i < objList.Count; i++)
    //        {
    //            string str = objList[i].name.Substring(objList[i].name.Length - 1);
    //            int index = int.Parse(str);
    //            // Debug.Log("位置:" + str);
    //            if (index == posIndex)
    //            {
    //                templist.Add(objList[i]);
    //                if (templist.Count > 2)   // 证明已经 存3个 元素了 --- 打断循环
    //                {
    //                    bianlilist(templist, jsonList);
    //                    PosCun(objList, jsonList);
    //                }

    //            }
    //        }
    //    }
    //}
    //static void bianlilist(List<GameObject> templist, JSONNode posList)
    //{
    //    JSONNode positem = new JSONNode();
    //    JSONNode renwupos = new JSONNode();
    //    JSONNode jianjinpos = new JSONNode();
    //    JSONNode houtuipos = new JSONNode();

    //    for (int j = 0; j < templist.Count; j++)
    //    {
    //        //人物
    //        if (templist[j].name.StartsWith("p"))
    //        {
    //            renwupos["px"] = templist[j].transform.position.x;
    //            renwupos["py"] = templist[j].transform.position.y;
    //            renwupos["pz"] = templist[j].transform.position.z;
    //            //Debug.Log("renwupx:" + renwupos.px + "renwupy:" + renwupos.py + "renwupz:" + renwupos.pz);
    //        }
    //        //后退
    //        if (templist[j].name.StartsWith("后"))
    //        {
    //            houtuipos["px"] = templist[j].transform.GetComponent<RectTransform>().anchoredPosition3D.x;
    //            houtuipos["py"] = templist[j].transform.GetComponent<RectTransform>().anchoredPosition3D.y;
    //            houtuipos["pz"] = templist[j].transform.GetComponent<RectTransform>().anchoredPosition3D.z;
    //            Vector3 huituipos = GetInspectorRotationValueMethod(templist[j].transform);
    //            houtuipos["rx"] = huituipos.x;
    //            houtuipos["ry"] = huituipos.y;
    //            houtuipos["rz"] = huituipos.z;
    //        }
    //        //前进
    //        if (templist[j].name.StartsWith("前"))
    //        {
    //            jianjinpos["px"] = templist[j].transform.GetComponent<RectTransform>().anchoredPosition3D.x;
    //            jianjinpos["py"] = templist[j].transform.GetComponent<RectTransform>().anchoredPosition3D.y;
    //            jianjinpos["pz"] = templist[j].transform.GetComponent<RectTransform>().anchoredPosition3D.z;
    //            Vector3 qianjinpos = GetInspectorRotationValueMethod(templist[j].transform);
    //            jianjinpos["rx"] = qianjinpos.x;
    //            jianjinpos["ry"] = qianjinpos.y;
    //            jianjinpos["rz"] = qianjinpos.z;
    //            // Debug.Log("前进PX" + jianjinpos.px + "前进PY" + jianjinpos.py + "前进PZ" + jianjinpos.pz);
    //            // Debug.Log("前进RX" + jianjinpos + "前进Ry" + jianjinpos.ry + "前进Rz" + jianjinpos.rz);
    //        }
    //    }
    //    positem["人物位置"] = renwupos;
    //    positem["前进箭头"] = jianjinpos;
    //    positem["后退箭头"] = houtuipos;
    //    posList.Add(positem);
    //    posIndex += 1;
    //}
    /// <summary>
    /// 赋值--数组
    /// </summary>
    /// <param name="templist"></param>
    /// <param name="posList"></param>  
    static void bianlilist(List<GameObject> templist, posarry posList)
    {
        positem positem = new positem();
        renwupos renwupos = new renwupos();
        jianjinpos jianjinpos = new jianjinpos();
        houtuipos houtuipos = new houtuipos();

        for (int j = 0; j < templist.Count; j++)
        {
            //人物
            if (templist[j].name.StartsWith("p"))
            {
                renwupos.px = templist[j].transform.position.x;
                renwupos.py = templist[j].transform.position.y;
                renwupos.pz = templist[j].transform.position.z;
                //Debug.Log("renwupx:" + renwupos.px + "renwupy:" + renwupos.py + "renwupz:" + renwupos.pz);
            }
            //后退
            if (templist[j].name.StartsWith("后"))
            {
                houtuipos.px = templist[j].transform.GetComponent<RectTransform>().anchoredPosition3D.x;
                houtuipos.py = templist[j].transform.GetComponent<RectTransform>().anchoredPosition3D.y;
                houtuipos.pz = templist[j].transform.GetComponent<RectTransform>().anchoredPosition3D.z;
                Vector3 huituipos = GetInspectorRotationValueMethod(templist[j].transform);
                houtuipos.rx = huituipos.x;
                houtuipos.ry = huituipos.y;
                houtuipos.rz = huituipos.z;
            }
            //前进
            if (templist[j].name.StartsWith("前"))
            {
                jianjinpos.px = templist[j].transform.GetComponent<RectTransform>().anchoredPosition3D.x;
                jianjinpos.py = templist[j].transform.GetComponent<RectTransform>().anchoredPosition3D.y;
                jianjinpos.pz = templist[j].transform.GetComponent<RectTransform>().anchoredPosition3D.z;
                Vector3 qianjinpos = GetInspectorRotationValueMethod(templist[j].transform);
                jianjinpos.rx = qianjinpos.x;
                jianjinpos.ry = qianjinpos.y;
                jianjinpos.rz = qianjinpos.z;
                // Debug.Log("前进PX" + jianjinpos.px + "前进PY" + jianjinpos.py + "前进PZ" + jianjinpos.pz);
                Debug.Log("前进RX" + jianjinpos.rx + "前进Ry" + jianjinpos.ry + "前进Rz" + jianjinpos.rz);
            }
        }
        positem.人物位置 = renwupos;
        positem.前进箭头 = jianjinpos;
        positem.后退箭头 = houtuipos;
        posList.poslist.Add(positem);
        posIndex += 1;

    }
    /// <summary>
    ///     //获取到旋转的正确数值
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static Vector3 GetInspectorRotationValueMethod(Transform transform)
    {
        // 获取原生值
        System.Type transformType = transform.GetType();
        PropertyInfo m_propertyInfo_rotationOrder = transformType.GetProperty("rotationOrder", BindingFlags.Instance | BindingFlags.NonPublic);
        object m_OldRotationOrder = m_propertyInfo_rotationOrder.GetValue(transform, null);
        MethodInfo m_methodInfo_GetLocalEulerAngles = transformType.GetMethod("GetLocalEulerAngles", BindingFlags.Instance | BindingFlags.NonPublic);
        object value = m_methodInfo_GetLocalEulerAngles.Invoke(transform, new object[] { m_OldRotationOrder });
        string temp = value.ToString();
        //将字符串第一个和最后一个去掉
        temp = temp.Remove(0, 1);
        temp = temp.Remove(temp.Length - 1, 1);
        //用‘，’号分割
        string[] tempVector3;
        tempVector3 = temp.Split(',');
        //将分割好的数据传给Vector3
        Vector3 vector3 = new Vector3(float.Parse(tempVector3[0]), float.Parse(tempVector3[1]), float.Parse(tempVector3[2]));
        return vector3;
    }
    /// <summary>
    /// 写json
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="fileInfo"></param>
    private static void WriteFile(string fileName, string fileInfo)
    {
        //文件路径
        string filePath = @"E:/小学场景配置文件/" + fileName;

        //检测文件夹是否存在，不存在则创建    
        if (File.Exists(filePath))
        {
            //System.Diagnostics.Process.Start(filePath);

            if (!string.IsNullOrEmpty(fileInfo))
            {
                string filePaths = @"E:\小学场景配置文件";
                System.Diagnostics.Process.Start("explorer.exe", filePaths);

                File.WriteAllText(filePath, fileInfo);//写入新文件
                Debug.Log("写入 File 完成");
            }
            else
            {
                Debug.Log("json 为空");
            }

            #region MyRegion
            //定义编码方式，text1.Text为文本框控件中的内容
            // byte[] mybyte = Encoding.UTF8.GetBytes(fileInfo);
            //写入文件
            //File.WriteAllBytes(filePath, mybyte);//写入新文件
            //string mystr1 = Encoding.UTF8.GetString(mybyte);
            //File.AppendAllText(filePath, mystr1);//添加至文件
            #endregion

        }
        else
        {
            Debug.Log("创建 File");
            //创建文件夹
            if (!Directory.Exists("E:/小学场景配置文件"))
            {
                Directory.CreateDirectory("E:/小学场景配置文件");
            }
            File.Create(filePath);
        }

    }
    /// <summary>
    /// 读j'son
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>  
    private static string ReadFile(string filepath)
    {
        string str;
        string filepaths = @"E:/小学场景配置文件/";
        //检测文件夹是否存在，不存在则创建    
        if (File.Exists(filepaths + filepath))
        {
            return str = File.ReadAllText(filepaths + filepath);
        }
        else
        {
            Debug.Log("读取失败 File");
            File.Create(filepaths + filepath);
        }
        return str = "";
    }

    ////static JSONNode JsonList = new JSONNode();
    //[MenuItem("添加组件和记录pos位置/记录位置(jsonNode 必须对应 pos0 前0 后0)")]
    //static void JsonNodeRcordpos()
    //{
    //    posIndex = 0;
    //    //第一次创建文件
    //    WriteFile("pos.json", "");

    //    GameObject[] PosObjArry = GameObject.FindGameObjectsWithTag("pos");
    //    List<GameObject> objList = PosObjArry.ToList();
    //    PosCun(objList, JsonList);

    //    Debug.Log("json数据:" + JsonList);
    //    WriteFile("pos.json", JsonList);
    //}
}
[Serializable]
public struct posarry
{
    public List<positem> poslist;
}
[Serializable]
public struct positem
{
    public renwupos 人物位置;
    public jianjinpos 前进箭头;
    public houtuipos 后退箭头;
}
[Serializable]
public struct renwupos
{
    public float px;
    public float py;
    public float pz;
}
[Serializable]
public struct jianjinpos
{
    public float px;
    public float py;
    public float pz;
    public float rx;
    public float ry;
    public float rz;
}
[Serializable]
public struct houtuipos
{
    public float px;
    public float py;
    public float pz;
    public float rx;
    public float ry;
    public float rz;
}

