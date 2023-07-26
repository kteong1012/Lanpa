using Lanpa;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDesigner
{
    public enum TestEnum
    {
        E_A,
        E_B,
        E_C,
        E_D,
    }
    [Flags]
    public enum TestEnum2
    {
        E_A = 1,
        E_B = 2,
        E_C = 4,
        E_D = 8,
    }

    [LText(label = "记录", inputText = false, order = 10)]
    private string _record = "aaabb";

    [LDropDown(label = "测试枚举", order = 13)]
    public TestEnum testEnum = TestEnum.E_A;

    [LMultiDropDown(label = "测试枚举2", order = 14)]
    public TestEnum2 testEnum2 = TestEnum2.E_C;

    [LText(label = "测试", inputText = true, order = 12)]
    private string test = "aaabb";

    [LCheckBox(label = "显示", order = 11)]
    private bool isShow = false;

    [LDictionary(label = "字典", order = 15)]
    private Dictionary<int, Dictionary<string, bool>> dic = new Dictionary<int, Dictionary<string, bool>>
    {
        { 1, new Dictionary<string, bool> { { "a", true }, { "b", false },{"x",false },{ "y",false} } },
        { 2, new Dictionary<string, bool> { { "c", true }, { "d", false } } },
    };

    [LButton("测试")]
    private void Test()
    {
        Debug.Log($"{_record}   {isShow}   {test}  {testEnum}");
        //打印dic
        foreach (var item in dic)
        {
            Debug.Log(item.Key);
            foreach (var item2 in item.Value)
            {
                Debug.Log($"{item2.Key}  {item2.Value}");
            }
        }
    }
}
