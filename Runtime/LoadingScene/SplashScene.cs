using System;
using System.Collections;
using _ScriptBase;
using PrimeTween;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace GiangCustom.Runtime.LoadingScene
{
    public class SplashScene : MonoBehaviour
    {
        public ProgressBar slideBar;
        private Coroutine loadSceneCo;

        void Start()
        {
            LoadScene();
        }

        public IEnumerator CheckInternetConnection(Action<bool> action)
        {
            UnityWebRequest request = new UnityWebRequest("https://google.com");
            request.timeout = 2;
            yield return request.SendWebRequest();

            action(request.result == UnityWebRequest.Result.Success && request.responseCode == 200);

        }

        public void LoadScene()
        {
            StartCoroutine(Helper.StartAction(() =>
                {
                    LoadSceneCallback();
                },
                1f));
        }

        private void LoadSceneCallback()
        {
            if(PlayerPrefs.GetInt("check_first_privacy", 0) == 0)
            {
                PlayerPrefs.SetInt("check_first_privacy", 1);
            }
            else
            {
                if (loadSceneCo != null) { StopCoroutine(loadSceneCo); }
                loadSceneCo = StartCoroutine(LoadAsyncScene());
            }
        }

        IEnumerator LoadAsyncScene()
        {
            var asyncOperation = SceneManager.LoadSceneAsync(SceneDictionary.sceneDic[SceneEnum.MainScene]);
            if (asyncOperation != null)
            {
                asyncOperation.allowSceneActivation = false;
            }
        
            slideBar.SetProgress(1f, 4f);
            yield return Tween.Delay(4f).ToYieldInstruction();
        
            if (asyncOperation != null)
            {
                while (asyncOperation.progress < 0.9f)
                {
                    yield return null;
                }

                asyncOperation.allowSceneActivation = true;
            }
        }
    }
}
