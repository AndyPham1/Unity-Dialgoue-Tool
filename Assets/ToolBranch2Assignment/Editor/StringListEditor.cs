using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(StringList))]
public class StringListEditor : Editor
{
    private StringList StringList;
    private SerializedObject SOStringListEditor;
    private SerializedProperty LanguageName;
    private SerializedProperty Keys;
    private SerializedProperty Dialogues;
    private SerializedProperty CountA;
    private SerializedProperty CountB;
    private string newDialgoue="";
    private string newKey="";
    private string StringUpdate;
    private static string KeySize = "Keys.Array.size";
    private static string DialogueSize = "Dialogue.Array.size";
    private static string KeyItem = "Keys.Array.data[{0}]";
    private static string DialogueItem = "Dialogue.Array.data[{0}]";

    public void OnEnable()
    {
        StringList = (StringList)target;
        SOStringListEditor = new SerializedObject(target);
        LanguageName = SOStringListEditor.FindProperty("LanguageName");
        CountA = SOStringListEditor.FindProperty(KeySize);
        CountB = SOStringListEditor.FindProperty(DialogueSize);
    }

    public override void OnInspectorGUI()
    {
        SOStringListEditor.Update();
        EditorGUILayout.LabelField("Dialogue Inspector", EditorStyles.boldLabel);
        LanguageName.stringValue = EditorGUILayout.TextField("Dialogue Language ", LanguageName.stringValue);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Key",EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Dialogue", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal(); 
        string[] StringList = GetDialogueList();
        string[] KeyList = GetKeyList();
        for (int i = 0; i < StringList.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            string newString = EditorGUILayout.TextField(KeyList[i].ToString(), StringList[i]);
            if (GUI.changed)
                SetDialogueList(i, newString);
            if(GUILayout.Button("-",GUILayout.Width(20f)))
            {
                RemoveItemFromArray(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        if(CountA.intValue < 1)
        {
            EditorGUILayout.LabelField("No entries are found!",EditorStyles.helpBox);
        }
        EditorGUILayout.LabelField("Add Key And Diagloue", EditorStyles.boldLabel);
        newKey = EditorGUILayout.TextField("Key: ", newKey);
        newDialgoue = EditorGUILayout.TextField("Dialogue: ", newDialgoue);
        if (GUILayout.Button("Add Key and Dialogue"))
        {
            if (string.IsNullOrEmpty(newKey.Trim()))
            {
                EditorGUILayout.HelpBox("Key Can't be Empty String",MessageType.Info);
            }
            else
            {
                CountA.intValue++;
                CountB.intValue++;
                SetKeyList(CountA.intValue - 1, newKey);
                SetDialogueList(CountB.intValue - 1, newDialgoue);
            }
        }
        
        SOStringListEditor.ApplyModifiedProperties();

    }

    private void RemoveItemFromArray(int i)
    {
        for (int h = i; h < CountA.intValue - 1; h++)
        {
            SetDialogueList(h, DialogueIndex(h + 1));
            SetKeyList(h, KeyIndex(h+1));
        }
        CountA.intValue--;
        CountB.intValue--;
    }

    public string[] GetKeyList()
    {
        string[] tmpList = new string[CountA.intValue];
        for (int i = 0; i < CountA.intValue; i++)
            tmpList[i] = SOStringListEditor.FindProperty(string.Format(KeyItem, i)).stringValue;
        return tmpList;
    }
    public string[] GetDialogueList()
    {
        string[] tmpList = new string[CountB.intValue];
        for(int i = 0; i < CountB.intValue; i++)
        {
            tmpList[i] = SOStringListEditor.FindProperty(string.Format(DialogueItem, i)).stringValue;
            //SOStringListEditor.FindProperty(string.Format(DialogueItem, i)).stringValue = EditorGUILayout.TextField(SOStringListEditor.FindProperty(string.Format(KeyItem, i)).stringValue,SOStringListEditor.FindProperty(string.Format(DialogueItem, i)).stringValue);
        }
        return tmpList;
    }

    public void SetDialogueList(int index, string dialogue)
    {
       if(SOStringListEditor.FindProperty(string.Format(DialogueItem, index)) != null)
            SOStringListEditor.FindProperty(string.Format(DialogueItem, index)).stringValue = dialogue;
    }
    public void SetKeyList(int index, string key)
    {
        if(SOStringListEditor.FindProperty(string.Format(KeyItem, index)) != null)
            SOStringListEditor.FindProperty(string.Format(KeyItem, index)).stringValue = key;
    }

    public string KeyIndex(int index)
    {
        return SOStringListEditor.FindProperty(string.Format(KeyItem, index)).stringValue;
    }

    public string DialogueIndex(int index)
    {
        return SOStringListEditor.FindProperty(string.Format(DialogueItem, index)).stringValue;
    }
}

