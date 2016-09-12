using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TaskPanel : BasePanel {


    private CanvasGroup canvasGroup;

    void Start()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
    }

/// <summary>
/// 处理页面的关闭
/// </summary>
    public override void OnExit()
    {
       // canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0,0.5f);


    }

    /// <summary>
    /// 显示
    /// </summary>
    public override void OnEnter()
    {
        //base.OnEnter();
        if(canvasGroup ==null)
            canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = true;

        canvasGroup.DOFade(1,0.5f);

    }




    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();   
    }
}
