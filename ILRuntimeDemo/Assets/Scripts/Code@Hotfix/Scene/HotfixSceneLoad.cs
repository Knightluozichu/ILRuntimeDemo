using Hotfix.Manager;
using Hotfix.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hotfix
{
    [SceneLoad(GlobalDefine.HOTFIX_SCENE_NAME)]
    public class HotfixSceneLoad : SceneLoad
    {
        protected HotfixSceneLoad(string sceneName) : base(sceneName)
        {
        }

        protected override void OnLoadFinish()
        {
            base.OnLoadFinish();
        }


        protected override void RegisterAllLoadTask()
        {
            base.RegisterAllLoadTask();
        }

        
    }
}

