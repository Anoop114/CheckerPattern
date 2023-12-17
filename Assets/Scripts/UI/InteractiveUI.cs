using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace UI
{
    public class InteractiveUI : MonoBehaviour
    {
        #region UI ButtonCall canvas

        public void Show(RectTransform quitPanel)
        {
            Show(quitPanel,null);
        }
        #endregion

        #region Logic Functions
        
        public void Show(RectTransform quitPanel,Action callbackMethod = null)
        {
            var quitPanelGroup = quitPanel.GetComponent<CanvasGroup>();
            quitPanelGroup.alpha = 0f;
            quitPanel.GetChild(0).GetComponent<RectTransform>().localScale = Vector3.zero;
            quitPanel.gameObject.SetActive(true);
            quitPanelGroup.DOFade(1f, 0.2f).OnComplete(() => {
                quitPanel.GetChild(0).GetComponent<RectTransform>().DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
                callbackMethod?.Invoke();
            });
        }

        // disable the quit panel
        public void CancelQuit(RectTransform quitPanel)
        {
            var quitPanelGroup = quitPanel.GetComponent<CanvasGroup>();
            quitPanel.GetChild(0).GetComponent<RectTransform>().DOScale(Vector3.zero, 0.2f).OnComplete(() =>
            {
                quitPanelGroup.DOFade(0f, 0.2f).OnComplete(() =>
                {
                    quitPanel.gameObject.SetActive(false);
                });
            });
        }

        public void Quitcall()
        {
            Application.Quit();
    #if UNITY_EDITOR
            EditorApplication. ExitPlaymode();
    #endif
        }
        #endregion
    }
}