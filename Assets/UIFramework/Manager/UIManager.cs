﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class UIManager {

    /// <summary>
    /// 单例模式的核心
    /// 1 定义一个静态的对象，在外界访问 在内部构造
    /// 2 构造方法私有化
    /// </summary>
    private static UIManager _instance;
    public static UIManager Instance
    {
        get {
            if (_instance == null)
            {
                _instance = new UIManager();
                //  Debug.Log("构造成功");
            }
            return _instance;
        }
    }

    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get {
            if(canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
             }
             return canvasTransform;
        }
    }

    private Dictionary<UIPanelType, string> panelPathDict;//存储所有面板的路径
    private Dictionary<UIPanelType, BasePanel> panelDict;//保存所有实例化面板游戏物体身上的basePanel组件
    private Stack<BasePanel> panelStack;

    private UIManager()
    {
        ParseUIPanelTypeJson();
    }

    /// <summary>
    /// 把某个页面入栈，把某个页面显示在界面上
    /// </summary>
    public void PushPanel(UIPanelType panelType)
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();
        
            //判断一下栈里面是否有页面
        if(panelStack.Count>0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = GetPanel(panelType);
        panel.OnEnter();
        panelStack.Push(panel);
    }

    /// <summary>
    /// 出栈，把页面从界面上移出
    /// </summary>
    public void PopPanel()
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        if (panelStack.Count <= 0)
            return;

        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();
    }



    private BasePanel GetPanel(UIPanelType panelType)
    {
        if(panelDict ==null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }

       // BasePanel panel;
        //panelDict.TryGetValue(panelType, out panel);//TODO

        BasePanel panel = panelDict.TryGet(panelType);

        if(panel ==null)
        {
            //如果找不到，那么就找这个面板的prefab的路径，然后去根据prefab去实例化面板
            //  string path;
            //panelPathDict.TryGetValue(panelType, out path);

            string path = panelPathDict.TryGet(panelType);
            GameObject instanPanel =  GameObject.Instantiate( Resources.Load(path)) as GameObject;
            instanPanel.transform.SetParent(CanvasTransform,false);

            panelDict.Add(panelType,instanPanel.GetComponent<BasePanel>());
            return instanPanel.GetComponent<BasePanel>();

         }else {
            return panel;
         }

    }

    [Serializable]
    class UIPanelTypeJson
    {
        public List<UIPanelInfo> infoList;
    }

    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();

        TextAsset ta= Resources.Load<TextAsset>("UIPanelType");

        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);


       // List<UIPanelInfo> panelInfoList= JsonUtility.FromJson<List<UIPanelInfo>>(ta.text);

        foreach(UIPanelInfo info in jsonObject.infoList)
        {
            Debug.Log(info.panelType);
            panelPathDict.Add(info.panelType,info.path);
        }

    }


   


}
