using Lanpa;
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

    [LText(label = "记录", inputText = false, order = 10)]
    private string _record = "aaabb";

    [LDropDown(label = "测试枚举", order = 13)]
    public TestEnum testEnum = TestEnum.E_A;

    [LText(label = "测试", inputText = true, order = 12)]
    private string test = "aaabb";

    [LCheckBox(label = "显示", order = 11)]
    private bool isShow = false;

    [LButton("测试")]
    private void Test()
    {
        Debug.Log($"{_record}   {isShow}   {test}  {testEnum}");
    }
}
