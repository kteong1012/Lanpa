using Lanpa;
using System;
using System.Collections.Generic;
using UnityEngine;
//Object并非C#基础中的Object，而是 UnityEngine.Object
using Object = UnityEngine.Object;

//使其能在Inspector面板显示，并且可以被赋予相应值
[Serializable]
public class ReferenceCollectorData
{
	public string key;
    //Object并非C#基础中的Object，而是 UnityEngine.Object
    public Object gameObject;
}
//继承IComparer对比器，Ordinal会使用序号排序规则比较字符串，因为是byte级别的比较，所以准确性和性能都不错
public class ReferenceCollectorDataComparer: IComparer<ReferenceCollectorData>
{
	public int Compare(ReferenceCollectorData x, ReferenceCollectorData y)
	{
		return string.Compare(x.key, y.key, StringComparison.Ordinal);
	}
}

public class ReferenceCollector: MonoBehaviour, ISerializationCallbackReceiver
{
	//Object并非C#基础中的Object，而是 UnityEngine.Object
	[LDictionary]
    public readonly Dictionary<string, ReferenceCollectorData> Dic = new Dictionary<string, ReferenceCollectorData>();
	//public XList<ReferenceCollectorData> List = new XList<ReferenceCollectorData>();
    //使用泛型返回对应key的gameobject
	public T Get<T>(string key) where T : class
	{
		ReferenceCollectorData dictGo;
		if (!Dic.TryGetValue(key, out dictGo))
		{
			return null;
		}
		return dictGo.gameObject as T;
	}

	public Object GetObject(string key)
	{
		ReferenceCollectorData dictGo;
		if (!Dic.TryGetValue(key, out dictGo))
		{
			return null;
		}
		return dictGo.gameObject;
	}

	public void OnBeforeSerialize()
	{
	}
    //在反序列化后运行
	public void OnAfterDeserialize()
	{
	}
}
