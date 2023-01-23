using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ScriptStruct
{
    public List<ConversationStruct> Script;
}
[Serializable]
public class ConversationStruct
{
    public List<DialogueStruct> Lines;
}
[Serializable]
public class DialogueStruct
    {
    public string Speaker;
public int LineID;
public string LeftSide;
public string RightSide;
public string LeftExpression;
public string RightExpression;
public string Sound;

}
