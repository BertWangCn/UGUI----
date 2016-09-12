using UnityEngine;
using System.Collections;
using System;


[Serializable]
public class UIPanelInfo:ISerializationCallbackReceiver {

[NonSerialized]
    public UIPanelType panelType;
    public string panelTypeString;
    public string path;

    /// <summary>
    /// 反序列化   从文本信息 到对象
    /// </summary>
    public void OnAfterDeserialize()
    {
        UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        panelType = type;
    }

    /// <summary>
    /// 序列化
    /// </summary>
    public void OnBeforeSerialize()
    {
        
    }
}
