using System.Collections.Generic;
using UnityEngine;

namespace GiangCustom.Runtime.LoadingScene
{
    public static class SceneDictionary
    {
        public static Dictionary<SceneEnum, string> sceneDic = new Dictionary<SceneEnum, string>()
        {
            {SceneEnum.Gameplay, "Gameplay"},
            {SceneEnum.Loading, "Loading"},
            {SceneEnum.MainScene, "MainScene"}
        };
    }

    public enum SceneEnum
    {
        Gameplay,
        Loading,
        MainScene
    }
}
