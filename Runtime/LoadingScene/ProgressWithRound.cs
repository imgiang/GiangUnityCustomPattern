using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace GiangCustom.Runtime.LoadingScene
{
    public class ProgressWithRound : MonoBehaviour
    {
        [SerializeField] private RectTransform round;
        [SerializeField] private Image progress;

        private Tween tween;

        public void ResetProgress()
        {
            progress.fillAmount = 0;
            round.pivot = new Vector2(round.pivot.x, 0);
        }

        public void SetProgress(float value)
        {
            if (tween.isAlive)
            {
                tween.Stop();
            }

            tween = Tween.Custom(startValue: progress.fillAmount,
                endValue: value, 0.5f,
                (v) =>
                {
                    progress.fillAmount = v;
                    round.pivot = new Vector2(round.pivot.x, v);
                });
        
        }
    }
}
