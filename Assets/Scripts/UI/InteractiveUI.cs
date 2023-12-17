using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace UI
{
    public class InteractiveUI : MonoBehaviour
    {
        // The menu that is going to hide.
    [SerializeField,Space] private RectTransform startMenu;
    
    //list of all panel that are added and deleted form list
    [SerializeField] private List<RectTransform> allPanels = new();

    // duration of the animation of the slide
    [SerializeField] private float duration = .5f;

    // mode of ease in and out.
    private readonly Ease ease = Ease.InSine;
    
    // can the UI is Hide or not;
    bool ishidecall;

    
    
    #region UI ButtonCall canvas
    // move center btn call
    public void MoveCenterCall(RectTransform t)
    {
        Vector2 right = Vector2.zero;
        right.x = t.rect.width + 10;
        t.localPosition = right;
        t.gameObject.SetActive(true);
        MoveCenter(t);
    }
        
    // move left btn call
    public void MoveLeftCall(RectTransform t)
    {
        MoveLeft(t);
    }
        
    // move right btn call
    public void MoveRightCall(RectTransform t)
    {
        MoveRight(t);
    }
    
    // back state btn call
    public void Back()
    {
        var panel = allPanels[allPanels.Count - 2];
        MoveRight(allPanels[allPanels.Count - 1]);
        MoveCenter(panel, null, true);
    }

    public void Show(RectTransform quitPanel)
    {
        Show(quitPanel,null);
    }
    // Hide UI
    public void HideAndShow(bool isHide)
    {
        if(!isHide && !ishidecall)
        {
            MoveLeft(startMenu, null);
            ishidecall = true;
        }
        else if(ishidecall)
        {
            MoveCenter(startMenu, null, true);
            ishidecall= false;
        }
    }
    #endregion

    #region Logic Functions

    // moving the canvas to the center
    private void MoveCenter(RectTransform panel, Action callbackMethod = null, bool callFromBack = false)
    {
        panel.gameObject.SetActive(true);

        if (callFromBack == false)
        {
            allPanels ??= new List<RectTransform>();
            allPanels.Add(panel);
        }

        panel.DOLocalMove(Vector3.zero, duration, true).SetEase(ease).OnComplete(() =>
        {
            callbackMethod?.Invoke();
        });
    }

    // moving the canvas to the Left
    private void MoveLeft(RectTransform panel, Action callbackMethod = null)
    {
        var left = Vector2.zero;
        left.x = (panel.rect.width + 10) * (-1f);

        panel.DOLocalMove(left, duration, true).SetEase(ease).OnComplete(() =>
        {
            panel.gameObject.SetActive(false);

            callbackMethod?.Invoke();
        });
    }

    // moving the canvas to the right
    private void MoveRight(RectTransform panel, Action callbackMethod = null)
    {
        var right = Vector2.zero;
        right.x = panel.rect.width + 10;

        allPanels.RemoveAt(allPanels.Count - 1);

        panel.DOLocalMove(right, duration, true).SetEase(ease).OnComplete(() =>
        {
            panel.gameObject.SetActive(false);

            callbackMethod?.Invoke();
        });
    }

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