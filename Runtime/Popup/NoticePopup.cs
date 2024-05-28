using System;
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
                confirmAction?.Invoke();
                Destroy(gameObject);
            });
            this.btnClose.onClick.RemoveAllListeners();
            this.btnClose.onClick.AddListener(() =>
            {
                closeAction?.Invoke();
                Destroy(gameObject);
            });

            this.title.text = title;
            this.content.text = content;
            gameObject.SetActive(true);
            if (panel)
            {
                Sequence.Create()
                    .Chain(Tween.Scale(panel, 1, 1.35f, 0.3f))
                    .Chain(Tween.Scale(panel, 1.35f, 0.85f, 0.3f))
                    .Chain(Tween.Scale(panel, 0.85f, 1, 0.4f));
            }
        }
    }
}