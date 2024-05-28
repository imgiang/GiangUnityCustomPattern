using System;
using System.Collections;
using _ScriptBase;
using PrimeTween;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Task = System.Threading.Tasks.Task;

namespace GiangCustom.Runtime.LoadingScene
{
    public class SplashScene : MonoBehaviour
    {
        public ProgressBar slideBar;
        private Coroutine loadSceneCo;

        void Start()
        {
            Application.targetFrameRate = 60;
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
            StartCoroutine(LoadAsyncScene());
        }

        IEnumerator LoadAsyncScene()
        {
            var asyncOperation = SceneManager.LoadSceneAsync("MainScene");
            yield return Task.Delay(1000);
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
