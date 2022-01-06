using System;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Animations.Rigging;
using UnityEditor;

using System.Collections;

using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Object = UnityEngine.Object;
using Boo.Lang;

/// <summary>
/// 自动查找物体
/// </summary>
public class AutoFindEditor
{
    //找资源
    public class FindReferences
    {

        [MenuItem("Assets/Find References", false, 10)]
        static private void Find()
        {
            EditorSettings.serializationMode = SerializationMode.ForceText;
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (!string.IsNullOrEmpty(path))
            {
                string guid = AssetDatabase.AssetPathToGUID(path);
                List withoutExtensions = new List() { ".mat" , ".controller" };//".prefab", ".unity", ".mat", ".asset"
                string[] files = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories)
                    .Where(s => withoutExtensions.Contains(Path.GetExtension(s).ToLower())).ToArray();
                int startIndex = 0;

                EditorApplication.update = delegate ()
                {
                    string file = files[startIndex];

                    bool isCancel = EditorUtility.DisplayCancelableProgressBar("匹配资源中", file, (float)startIndex / (float)files.Length);

                    if (Regex.IsMatch(File.ReadAllText(file), guid))
                    {
                        Debug.Log(file, AssetDatabase.LoadAssetAtPath<Object>(GetRelativeAssetsPath(file)));
                    }

                    startIndex++;
                    if (isCancel || startIndex >= files.Length)
                    {
                        EditorUtility.ClearProgressBar();
                        EditorApplication.update = null;
                        startIndex = 0;
                        Debug.Log("匹配结束");
                    }

                };
            }
        }

        [MenuItem("Assets/Find References", true)]
        static private bool VFind()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            return (!string.IsNullOrEmpty(path));
        }

        static private string GetRelativeAssetsPath(string path)
        {
            return "Assets" + Path.GetFullPath(path).Replace(Path.GetFullPath(Application.dataPath), "").Replace('\\', '/');
        }
    }

    //[MenuItem("Control/自动/人物设置交互")]

    //public static void 人物设置()
    //{
    //    if (Selection.objects.Length < 1)
    //    {
    //        Debug.LogWarning("没有选择");
    //        return;
    //    }
    //    GameObject obj = (GameObject)Selection.objects[0];
    //    var rootbone = obj.transform.Find("BoneRoot");
    //    if (obj == null || rootbone ==null )
    //    {
    //        Debug.LogWarning("选择错误");
    //        return;
    //    }
    //    RigBuilder builder = obj.GetComponent<RigBuilder>();
    //    if (builder == null) {
    //        builder = obj.AddComponent<RigBuilder>();
    //    }

    //    builder.layers = new System.Collections.Generic.List<RigBuilder.RigLayer>();
    //    腰部层.Clear();
    //    位置层.Clear();
    //    手部层.Clear();

    //    GameObject handjiahuobj = obj.transform.Find("动画覆盖/右手接递交互").gameObject;
    //    if (handjiahuobj == null) {
    //        return;
    //    }
    //    右手接递交互(handjiahuobj, rootbone);

    //    builder.layers.AddRange(腰部层);
    //    builder.layers.AddRange(位置层);
    //    builder.layers.AddRange(手部层);

    //    foreach ( var t in builder.layers) {
    //        t.rig.weight = 0;
    //    }
    //}

    //static System.Collections.Generic.List<RigBuilder.RigLayer> 腰部层 = new System.Collections.Generic.List<RigBuilder.RigLayer>();
    //static System.Collections.Generic.List<RigBuilder.RigLayer> 位置层 = new System.Collections.Generic.List<RigBuilder.RigLayer>();
    //static System.Collections.Generic.List<RigBuilder.RigLayer> 手部层 = new System.Collections.Generic.List<RigBuilder.RigLayer>();





    //public static void 右手接递交互(GameObject obj,Transform rootbone)
    //{

    //    //obj.transform.parent.Find("BoneRoot");

    //    if (rootbone == null)
    //    {
    //        return;
    //    }

     

    //    //找到所有位置

    //    TwoBoneIKConstraint[] tbiArray = obj.GetComponentsInChildren<TwoBoneIKConstraint>();

    //    for (int i = 0; i < tbiArray.Length; i++)
    //    {
    //        AutoFindBoneHandPos(SideOfBody.R, rootbone, tbiArray[i].gameObject);

    //        //Transform temp = obj.transform.GetChild(i);

    //        // FindObjHandpos(temp, rootbone);
    //    }

    //    MultiParentConstraint[] mpcArray = obj.GetComponentsInChildren<MultiParentConstraint>();

    //    for (int i = 0; i < mpcArray.Length; i++)
    //    {
    //        //  AutoFindBoneHandPos(SideOfBody.R, rootbone, tbiArray[i].gameObject);

    //        //Transform temp = obj.transform.GetChild(i);

    //        // FindObjHandpos(temp, rootbone);


    //        bool isEnglish = CheckeHaveChildName(rootbone, "R_Mid1");
    //        bool isNumber = CheckeHaveChildName(rootbone, "L_Finger00");


    //        if (isNumber)
    //        {
    //            SetBoone("autofindman", rightstr, mpcArray[i].gameObject, rootbone);
    //            Debug.Log("找到右手数字 绑定成功");
    //        }

    //        if (isEnglish)
    //        {
    //            SetBoone("autofindwoman", rightstr, mpcArray[i].gameObject, rootbone);
    //            Debug.Log("找到右手英文 绑定成功");
    //        }

    //    }

    //    MultiRotationConstraint[] mrcArray = obj.GetComponentsInChildren<MultiRotationConstraint>();

    //    for (int i = 0; i < mrcArray.Length; i++)
    //    {
    //        if (mrcArray[i].gameObject.name.Contains("腰"))
    //        {
    //            Rig rig = mrcArray[i].GetComponent<Rig>();
    //            if (rig == null)
    //            {
    //                rig = mrcArray[i].gameObject.AddComponent<Rig>();
    //            }

    //            腰部层.Add(new RigBuilder.RigLayer(rig));
    //            //var temptran = FindName(boneroot, Fingerstemp[item.gameObject.name]);
    //            //if (temptran == null)
    //            //{
    //            //    continue;
    //            //}
    //            var temptran = FindName(rootbone, "Spine01");
    //            if (temptran == null)
    //            {
    //                Debug.Log("没有找到Spine01");
    //                continue;
    //            }
    //            mrcArray[i].GetComponent<BoneRenderer>().transforms = new Transform[] { mrcArray[i].transform };
    //            mrcArray[i].data.constrainedObject = temptran;
    //            mrcArray[i].gameObject.transform.position = temptran.position;
    //            mrcArray[i].gameObject.transform.rotation = temptran.rotation;

    //            var tempar = new WeightedTransformArray(1);
    //            tempar.SetTransform(0, temptran.transform);
    //            tempar.SetWeight(0, 1);
    //            mrcArray[i].data.sourceObjects = tempar;


    //        }
    //    }
    //}




    //[MenuItem("Control/自动/右手接递交互")]
    //public static void 右手接递交互() {

    //    if (Selection.objects.Length < 1)
    //    {
    //        Debug.LogWarning("没有选择骨骼");
    //        return;
    //    }
    //    GameObject obj = (GameObject)Selection.objects[0];
    //    if (obj == null || !obj.name.EndsWith("右手接递交互"))
    //    {
    //        Debug.LogWarning("选择错误");
    //        return;
    //    }

    //    var rootbone = _FindCloseParent(obj.transform , "BoneRoot");

    //    //obj.transform.parent.Find("BoneRoot");

    //    if (rootbone == null) {
    //        return ;
    //    }

    //    //rootbone
    //    //for (int i = 0; i < obj.transform.childCount; i++)
    //    //{
    //    //    Transform temp = obj.transform.GetChild(i);

    //    //    FindObjHandpos(temp, rootbone);
    //    //}

    //    //找到所有位置

    //    TwoBoneIKConstraint[] tbiArray = obj.GetComponentsInChildren<TwoBoneIKConstraint>();

    //    for (int i = 0; i < tbiArray.Length; i++)
    //    {
    //        AutoFindBoneHandPos(SideOfBody.R, rootbone, tbiArray[i].gameObject);
          
    //        //Transform temp = obj.transform.GetChild(i);

    //        // FindObjHandpos(temp, rootbone);
    //    }

    //   MultiParentConstraint[] mpcArray = obj.GetComponentsInChildren<MultiParentConstraint>();

    //    for (int i = 0; i < mpcArray.Length; i++)
    //    {
    //        //  AutoFindBoneHandPos(SideOfBody.R, rootbone, tbiArray[i].gameObject);

    //        //Transform temp = obj.transform.GetChild(i);

    //        // FindObjHandpos(temp, rootbone);


    //        bool isEnglish = CheckeHaveChildName(rootbone, "R_Mid1");
    //        bool isNumber = CheckeHaveChildName(rootbone, "L_Finger00");

           
    //            if (isNumber)
    //            {
    //                SetBoone("autofindman", rightstr, mpcArray[i].gameObject,rootbone);
    //                Debug.Log("找到右手数字 绑定成功");
    //            }

    //            if (isEnglish)
    //            {
    //                SetBoone("autofindwoman", rightstr, mpcArray[i].gameObject, rootbone);
    //                Debug.Log("找到右手英文 绑定成功");
    //            }
                
    //    }

    //    MultiRotationConstraint[] mrcArray = obj.GetComponentsInChildren<MultiRotationConstraint>();

    //    for (int i = 0; i < mrcArray.Length; i++)
    //    {
    //        if (mrcArray[i].gameObject.name.Contains("腰")) {

    //            Rig rig = mrcArray[i].GetComponent<Rig>();
    //            if (rig == null)
    //            {
    //                rig = mrcArray[i].gameObject.AddComponent<Rig>();
    //            }

    //            腰部层.Add(new RigBuilder.RigLayer(rig));
    //            //var temptran = FindName(boneroot, Fingerstemp[item.gameObject.name]);
    //            //if (temptran == null)
    //            //{
    //            //    continue;
    //            //}
    //            var temptran = FindName(rootbone, "Spine01");
    //            if (temptran == null)
    //            {
    //                Debug.Log("没有找到Spine01");
    //                continue;
    //            }
    //            mrcArray[i].GetComponent<BoneRenderer>().transforms = new Transform[] { mrcArray[i].transform };
    //            mrcArray[i].data.constrainedObject = temptran;
    //            mrcArray[i].gameObject.transform.position = temptran.position;
    //            mrcArray[i].gameObject.transform.rotation = temptran.rotation;

    //            var tempar = new WeightedTransformArray(1);
    //            tempar.SetTransform(0, temptran.transform);
    //            tempar.SetWeight(0, 1);
    //            mrcArray[i].data.sourceObjects = tempar;


    //        }
    //    }
    // }


    


    private static Transform _FindCloseParent(Transform target, String name)
    {


        Transform temp = target.Find(name);

        if (temp == null) {
            if(target.parent==null)
            {
                return null;
            }
            return _FindCloseParent(target.parent, name);
        }
        else {
            return temp;
        }


    }




    //[MenuItem("Control/自动/手部轨迹位置设定 道具动画")]
    //public static void AutohandObjParent() {
    //    if (Selection.objects.Length < 1)
    //    {
    //        Debug.LogWarning("没有选择骨骼");
    //        return;
    //    }
    //    GameObject obj = (GameObject)Selection.objects[0];
    //    if (obj == null || !obj.name.EndsWith("道具动画"))
    //    {
    //        Debug.LogWarning("选择错误");
    //        return;
    //    }
    //    var rootbone = obj.transform.parent.Find("BoneRoot");
    //    for (int i =0;i< obj.transform.childCount;i++) {
    //       Transform temp =  obj.transform.GetChild(i);

    //        FindObjHandpos(temp, rootbone);
    //    }

    //}


    //[MenuItem("Control/自动/手部轨迹位置设定")]
    //public static void AutoHandPos() {

    //    if (Selection.objects.Length < 1)
    //    {
    //        Debug.LogWarning("没有选择骨骼");
    //        return;
    //    }

    //    GameObject obj = (GameObject)Selection.objects[0];
    //    if (obj == null || obj.GetComponent<BoneRenderer>() == null)
    //    {
    //        Debug.LogWarning("选择错误");
    //        return;
    //    }
    //    var rootbone = obj.transform.parent.parent.Find("BoneRoot");
    //    if (obj.name.StartsWith("右"))
    //    {

    //        AutoFindBoneHandPos(SideOfBody.R, rootbone, obj);
    //    }
    //    if (obj.name.StartsWith("左"))
    //    {

    //        AutoFindBoneHandPos(SideOfBody.L, rootbone, obj);
    //    }
    //}

    //public static void FindObjHandpos(Transform findobj,Transform rootbone) {

    //    if (findobj == null || !findobj.name.EndsWith("轨迹"))
    //    {
    //        Debug.LogWarning(findobj.name+ ":选择错误");
    //        return;
    //    }
      

    //    if (findobj.name.Contains("右"))
    //    {
    //        Debug.Log("找到右"+ findobj.name);
    //        AutoFindBoneHandPos(SideOfBody.R, rootbone, findobj.gameObject);
    //    }
    //    if (findobj.name.Contains("左"))
    //    {
    //        Debug.Log("找到左"+ findobj.name);
    //        AutoFindBoneHandPos(SideOfBody.L, rootbone, findobj.gameObject);
    //    }

    //}





  

    //public enum SideOfBody {

    //    R,
    //    L,
    //    M
    //}


    //public static void AutoFindBoneHandPos(SideOfBody leftorright,Transform rootBone,GameObject findobj)
    //{



      



      



    //    if (findobj == null)
    //        return;




    //    var root = FindName(rootBone, leftorright.ToString() + "_Upperarm");
    //    var mid = FindName(rootBone, leftorright.ToString() + "_Forearm");
    //    var tip = FindName(rootBone, leftorright.ToString() + "_Hand");

    //    //找到组件
    //    TwoBoneIKConstraint[] all = findobj.GetComponentsInChildren<TwoBoneIKConstraint>();
      
    //    foreach (var item in all)
    //    {
    //        Rig rig = item.GetComponent<Rig>();
    //        if (rig == null) {
    //            rig = item.gameObject.AddComponent<Rig>();
    //        }
    //        BoneRenderer br = item.GetComponent<BoneRenderer>();
    //        if (br == null)
    //        {
    //            br = item.gameObject.AddComponent<BoneRenderer>();
    //        }

    //        位置层.Add(new RigBuilder.RigLayer(rig));
    //        item.data.root = root;
    //        item.data.mid = mid;
    //        item.data.tip = tip;
    //    Transform tr=    item.transform.parent.Find("elbow");
    //        if (tr!=null) {
    //            item.data.hint = tr;
    //        }
            

    //    }




    //}


    //public static void SetBoone(string MobanName, string[] mapping,GameObject obj,Transform rootbone)
    //{

    //    Dictionary<string, string> Fingerstemp = new Dictionary<string, string>();
    //    string jsonStr = Resources.Load<TextAsset>(MobanName).ToString();
    //    Rolestr rolestr = JsonUtility.FromJson<Rolestr>(jsonStr);
    //    for (int i = 0; i < rolestr.Fingers.Length; i++)
    //    {
    //        if (!Fingerstemp.ContainsKey(mapping[i]))
    //        {
    //            Fingerstemp.Add(mapping[i], rolestr.Fingers[i].finger);
    //        }
    //    }
    //    FuZhi(obj, Fingerstemp, rootbone);
    //}

    //public static void SetBoone(string MobanName, string[] mapping) {

    //    Fingers = new Dictionary<string, string>();
    //    string jsonStr = Resources.Load<TextAsset>(MobanName).ToString();
    //    Rolestr rolestr = JsonUtility.FromJson<Rolestr>(jsonStr);
    //    for (int i = 0; i < rolestr.Fingers.Length; i++)
    //    {
    //        if (!Fingers.ContainsKey(mapping[i]))
    //        {
    //            Fingers.Add(mapping[i], rolestr.Fingers[i].finger);
    //        }
    //    }
    //    FuZhi(Selection.objects[0]);
    //}

    //[MenuItem("Control/自动/手指骨骼绑定")]
    //public static void AutoFindBoneMR()
    //{

    //    if (Selection.objects.Length < 1) {
    //        Debug.LogWarning("没有选择骨骼");
    //        return;
    //    }

    //    GameObject obj = (GameObject)Selection.objects[0];
    //    if (obj == null  || obj.GetComponent<MultiParentConstraint>()==null)
    //    {
    //        Debug.LogWarning("选择错误");
    //        return;
    //    }

    //    //检测模式
    //    var boneroot = obj.transform.parent.parent.Find("BoneRoot");

    //    bool isEnglish = CheckeHaveChildName(boneroot, "R_Mid1");
    //    bool isNumber = CheckeHaveChildName(boneroot, "L_Finger00");

    //    //检测左右
    //    if (obj.name.StartsWith("右")) {
    //        if (isNumber) {
    //            SetBoone("autofindman", rightstr);
    //            Debug.Log("找到右手数字 绑定成功");
    //        }

    //        if (isEnglish) { 
    //        SetBoone("autofindwoman", rightstr);
    //            Debug.Log("找到右手英文 绑定成功");
    //        }
    //        return;
    //    }

    //    if (obj.name.StartsWith("左"))
    //    {
    //        if (isNumber)
    //        {
    //            SetBoone("L_autofindmanL", leftstr);
    //            Debug.Log("找到左手数字 绑定成功");
    //        }
    //        if (isEnglish) { 
    //            SetBoone("L_autofindwomanL", leftstr);
    //            Debug.Log("找到左手数字 绑定成功");
    //         }    
    //        return;
    //    }

    //    Debug.LogWarning("没有找到对应");



    //}

   // /// <summary>
   // /// 检测所有子物体是否有对应的名字
   // /// </summary>
   // /// <param name="boneRoot"></param>
   // /// <param name="name"></param>
   // /// <returns></returns>
   // static bool CheckeHaveChildName(Transform boneRoot, string name) {
   //     Transform[] allbool = boneRoot.GetComponentsInChildren<Transform>();
   //     foreach (Transform obj in allbool) {

           

   //         if (obj.name == name) {

   //             return true;
   //         }

   //     }
   //     return false;
   // }

   // //[MenuItem("Control/自动/手指骨骼名为数字 左手")]
   // public static void AutoFindBoneML()
   // {
   //     SetBoone("L_autofindman", leftstr);
   // }

   // //[MenuItem("Control/自动/手指骨骼名为英文 左手")]
   // public static void AutoFindBoneWML()
   // {
   //     SetBoone("L_autofindwoman", leftstr);
   // }

   //// [MenuItem("Control/自动/手指骨骼名为英文 右手")]
   // public static void AutoFindBoneWMR()
   // {
   //     SetBoone("autofindwoman", rightstr);
   // }



    public static string[] rightstr = new string[16] { "右手骨骼", "大拇指下", "大拇指中", "大拇指上", "食指下", "食指中", "食指上", "中指下", "中指中",
            "中指上", "无名指下", "无名指中", "无名指上", "小指下", "小指中", "小指上" };


    public static string[] leftstr = new string[16] { "左手骨骼", "大拇指下", "大拇指中", "大拇指上", "食指下", "食指中", "食指上", "中指下", "中指中",
            "中指上", "无名指下", "无名指中", "无名指上", "小指下", "小指中", "小指上" };

   
    public static void AutoFindBoneW()
    {
        //Fingers = new Dictionary<string, string>();
        //string jsonStr = Resources.Load<TextAsset>("autofindwoman").ToString();
        //Rolestr rolestr = JsonUtility.FromJson<Rolestr>(jsonStr);
        //for (int i = 0; i < rolestr.Fingers.Length; i++)
        //{
        //    if (!Fingers.ContainsKey(str[i]))
        //    {
        //        Fingers.Add(str[i], rolestr.Fingers[i].finger);
        //    }
        //}
        //FuZhi(Selection.objects[0]);
    }


    /// <summary>
    /// 找到物体
    /// </summary>
    /// <param name="root"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Transform FindName(Transform root, string name)
    {
        var trans = root.GetComponentsInChildren<Transform>();

        foreach (var obj in trans)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }


    ///// <summary>
    ///// 赋值
    ///// </summary>
    ///// <param name="trm"></param>
    //public static void FuZhi(Object trm,Dictionary<string, string> Fingerstemp , Transform boneroot)
    //{
    //    if (trm == null)
    //        return;
    //    GameObject obj = (GameObject)trm;
    //    if (!obj.GetComponent<MultiParentConstraint>())
    //    {
    //        Debug.Log("不存在");
    //        return;
    //    }
    //    //找到父物体
    //   // var boneroot = obj.transform.parent.parent.Find("BoneRoot");

    //    MultiParentConstraint mpc = obj.GetComponent<MultiParentConstraint>();

    //    Rig rig = mpc.GetComponent<Rig>();
    //    if (rig == null) {
    //        rig = mpc.gameObject.AddComponent<Rig>();
    //    }
    //    手部层.Add(new RigBuilder.RigLayer(rig));

    //    mpc.data.constrainedObject = obj.transform;
    //    var temp = new WeightedTransformArray(1);
    //    temp.SetTransform(0, FindName(boneroot, Fingerstemp[obj.name]));
    //    temp.SetWeight(0, 1);
    //    mpc.data.sourceObjects = temp;
    //    mpc.gameObject.transform.position = temp.GetTransform(0).position;
    //    mpc.gameObject.transform.rotation = temp.GetTransform(0).rotation;
    //    //找到组件
    //    MultiRotationConstraint[] all = obj.GetComponentsInChildren<MultiRotationConstraint>();
    //    foreach (var item in all)
    //    {
    //        if (Fingerstemp.ContainsKey(item.gameObject.name))
    //        {
    //            var temptran = FindName(boneroot, Fingerstemp[item.gameObject.name]);
    //            if (temptran == null)
    //            {
    //                continue;
    //            }
    //            item.data.constrainedObject = temptran;
    //            item.gameObject.transform.position = temptran.position;
    //            item.gameObject.transform.rotation = temptran.rotation;

    //            var tempar = new WeightedTransformArray(1);
    //            tempar.SetTransform(0, item.transform);
    //            tempar.SetWeight(0, 1);
    //            item.data.sourceObjects = tempar;
    //        }
    //    }
    //}


    ///// <summary>
    ///// 赋值
    ///// </summary>
    ///// <param name="trm"></param>
    //public static void FuZhi(Object trm)
    //{
    //    if (trm == null)
    //        return;
    //    GameObject obj = (GameObject)trm;
    //    if (!obj.GetComponent<MultiParentConstraint>())
    //    {
    //        Debug.Log("不存在");
    //        return;
    //    }
    //    //找到父物体
    //    var boneroot = obj.transform.parent.parent.Find("BoneRoot");       

    //    MultiParentConstraint mpc = obj.GetComponent<MultiParentConstraint>();
    //    mpc.data.constrainedObject = obj.transform;
    //    var temp = new WeightedTransformArray(1);
    //    temp.SetTransform(0, FindName(boneroot, Fingers[obj.name]));
    //    temp.SetWeight(0, 1);
    //    mpc.data.sourceObjects = temp;
    //    mpc.gameObject.transform.position = temp.GetTransform(0).position;
    //    mpc.gameObject.transform.rotation = temp.GetTransform(0).rotation;
    //    //找到组件
    //    MultiRotationConstraint[] all = obj.GetComponentsInChildren<MultiRotationConstraint>();
    //    foreach (var item in all)
    //    {
    //        if (Fingers.ContainsKey(item.gameObject.name))
    //        {
    //            var temptran = FindName(boneroot, Fingers[item.gameObject.name]);
    //            if (temptran == null)
    //            {
    //                continue;
    //            }
    //            item.data.constrainedObject = temptran;
    //            item.gameObject.transform.position = temptran.position;
    //            item.gameObject.transform.rotation = temptran.rotation;
               
    //            var tempar = new WeightedTransformArray(1);
    //            tempar.SetTransform(0, item.transform);
    //            tempar.SetWeight(0, 1);
    //            item.data.sourceObjects = tempar;
    //        }
    //    }
    //}

    /// <summary>
    /// 这是右手骨骼
    /// </summary>
        //这是key
    public static Dictionary<string, string> Fingers;
}
[Serializable]
public class Rolestr
{
    public Fingers[] Fingers;

}
[Serializable]
public class Fingers
{
    public string finger;
}
