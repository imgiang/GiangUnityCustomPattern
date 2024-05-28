using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Tween = PrimeTween.Tween;

namespace _ScriptBase
{
    public class ProgressBar : MonoBehaviour
    {
        public Image image;
        public TextMeshProUGUI text;

        void Start()
        {
            if (image)
            {
                image.fillAmount = 0;
            }
        }

        public void SetProgress(float progress, float duration = 0)
        {
            if (!image)
            {
                return;
            }
            
            if (duration == 0)
            {
                image.fillAmount = progress;
            }
            else
            {
                Tween.Custom(image.fillAmount, progress, duration, (v) =>
                {
                    image.fillAmount = v;
                    text.text = (v * 100).ToString("N0") + "%";

                });
            }
        }
    }
}
