using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 对dictionnary的扩展
/// </summary>
public static class DictionaryExtension  {

    /// <summary>
    /// 尝试根据Key得到value,得到了话直接返回value，没有得到直接返回null
    /// </summary>
	public static TValue TryGet<TKey,TValue>(this Dictionary<TKey,TValue> dict,TKey key)
    {
        TValue value;
        dict.TryGetValue(key, out value);
        return value;
    }
}
