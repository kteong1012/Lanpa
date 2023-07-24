using Lanpa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDesigner
{
    [LText(label = "记录", inputText = false, order = 10)]
    private string _record = "aaabb";


    [LText(label = "测试", inputText = true, order = 12)]
    private string test = "aaabb";

    [LCheckBox(label = "显示", order = 11)]
    private bool isShow = false;

    [LButton("测试")]
    private void Test()
    {
        Debug.Log($"{_record}   {isShow}   {test}");
    }
}
