using System;
using GiangCustom.Runtime.Sounds;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GiangCustom.Runtime.Popup
{
    public class NoticePopup : MonoBehaviour
    {
        [SerializeField] private RectTransform panel;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI content;
        [SerializeField] private Button btnClose;
        [SerializeField] private Button btnConfirm;

        public void Show(string title, string content, Action confirmAction = null, Action closeAction = null)
        {
            this.btnConfirm.onClick.RemoveAllListeners();
            this.btnConfirm.onClick.AddListener(() =>
            {
                SoundManagerCustom.Instance.PlaySound(SoundName.Tap);
                AdsController.Instance.ShowInters2(null, InterstitialPositionType.Close.ToString());
                confirmAction?.Invoke();
                Destroy(gameObject);
            });
            this.btnClose.onClick.RemoveAllListeners();
            this.btnClose.onClick.AddListener(() =>
            {
                closeAction?.Invoke();
                SoundManagerCustom.Instance.PlaySound(SoundName.Tap);
                AdsController.Instance.ShowInters2(null, InterstitialPositionType.Close.ToString());
                Destroy(gameObject);
            });

            this.title.text = title;
            this.content.text = content;
            gameObject.SetActive(true);
            if (panel)
            {
                Sequence.Create()
                    .Chain(Tween.Scale(panel, 1, 1.15f, 0.3f))
                    .Chain(Tween.Scale(panel, 1.15f, 0.95f, 0.3f))
                    .Chain(Tween.Scale(panel, 0.95f, 1, 0.4f));
            }
        }
    }
}