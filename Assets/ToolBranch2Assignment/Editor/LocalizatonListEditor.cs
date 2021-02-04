using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

[CustomEditor(typeof(LocalizationList))]
public class LocalizatonListEditor : Editor {
    private LocalizationList LocalizationList;
    private SerializedObject SOStringListEditor;
    private SerializedProperty StringListList;
    private SerializedProperty Count;
    private StringList[] tmp;
    private string newLang;
    private string tmpString;
    private string tmpString2;
    private Vector2 scrollpos;
    private int viewIndex = 0;
    private static string ListCount = "StringListList.Array.size";
    private static string ListItem = "StringListList.Array.data[{0}]";
    public void OnEnable()
    {
        LocalizationList = (LocalizationList) target;
        SOStringListEditor = new SerializedObject(LocalizationList);
        StringListList = SOStringListEditor.FindProperty("StringListList");
        StringListList = SOStringListEditor.FindProperty("StringListList");
        Count = SOStringListEditor.FindProperty(ListCount);
    }

    public override void OnInspectorGUI()
    {
        SOStringListEditor.Update();
        GUILayout.Label("Localization Manager", EditorStyles.boldLabel);
        tmp = GetList();

        GUILayout.Label("Create Language", EditorStyles.boldLabel);
        newLang = EditorGUILayout.TextField("", newLang);
        EditorGUILayout.HelpBox("Create Language makes the asset with the inputted name (If asset file name already exist in the list or input is empty, editor will not create asset)", UnityEditor.MessageType.Info);
        if (GUILayout.Button("Create New Language"))
        {

            if(!String.IsNullOrEmpty(newLang.Trim()))
            {
                StringList asset = ScriptableObject.CreateInstance<StringList>(); 
                asset.LanguageName = newLang;


                StringList res = LocalizationList.StringListList.Find((x) =>
                {
                    if (x == null) return false;
                    return x.LanguageName.Equals(newLang);
                        
                }
                        );
                if (res == null)
                {
                    AssetDatabase.CreateAsset(asset, "Assets/ScriptableObject/" + newLang + ".asset");
                    AssetDatabase.SaveAssets();
                    LocalizationList.StringListList.Add(asset);
                }
                else
                {
                    asset = null;
                }
            }

            //Count.intValue++;
            //SetList(Count.intValue - 1, null);
        }

        if (Count.intValue > 0)
        {  
            GUILayout.Label("StringListList Click and Drop&Drag Reference",EditorStyles.boldLabel);
            GUILayout.Label("Basic Load Asset", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(StringListList,true);
        }
        else
        {
            EditorGUILayout.LabelField("No Entries Found!");
        }

        GUILayout.Label("Basic Unload Asset ", EditorStyles.boldLabel);

        if (Count.intValue > 0)
        {
            for (int i = 0; i < tmp.Length; i++)
            {
                if(tmp[i] != null)
                {

                    EditorGUILayout.BeginHorizontal();
                    bool test = String.IsNullOrEmpty(tmp[i].LanguageName.Trim());
                    if(test)
                    {
                        EditorGUILayout.LabelField("No language Name");
                    }
                    else
                    {
                        EditorGUILayout.LabelField(tmp[i].LanguageName);
                    }
                    
                    if (GUILayout.Button("-", GUILayout.Width(20f)))
                    {
                        RemoveItemFromArray(i);
                    }
                    if (GUILayout.Button("Select", GUILayout.ExpandWidth(false)))
                    {
                        SelectAsTargetLanguage(i);

                    }
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Empty Entry");
                    if (GUILayout.Button("-", GUILayout.Width(20f)))
                    {
                        RemoveItemFromArray(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }

            }
        }
        else
        {
            EditorGUILayout.LabelField("No Entries Found!",EditorStyles.helpBox);
        }



        GUILayout.Label("Target Language Selector", EditorStyles.boldLabel);
        if(LocalizationList.targetStringList != null)
        {

        bool test2 = String.IsNullOrEmpty(LocalizationList.targetStringList.LanguageName);

        if(String.IsNullOrEmpty(LocalizationList.targetStringList.LanguageName.Trim()))
        {
            GUILayout.Label("Language Selected: No language name");
        }
        else{
            GUILayout.Label("Language Selected: " + LocalizationList.targetStringList.LanguageName);

        }
        }
        else
        {
            GUILayout.Label("Language Selected:");
        }
        
        SOStringListEditor.ApplyModifiedProperties();
    }

    private void SelectAsTargetLanguage(int i)
    {
        LocalizationList.targetStringList = ListIndex(i);
    }

    public StringList CreateAsset(string path)
    {

        StringList asset = ScriptableObject.CreateInstance<StringList>();
        AssetDatabase.CreateAsset(asset, "assets/" + path);
        AssetDatabase.SaveAssets();
        return asset;
    }

    private void RemoveItemFromArray(int i)
    {
        for (int h = i; h < Count.intValue - 1; h++)
        {
            SetList(h, ListIndex(h+1));
        }
        Count.intValue--;
    }

    public StringList[] GetList()
    {
        StringList[] tmpList = new StringList[Count.intValue];
        for (int i = 0; i < Count.intValue; i++)
        {
            tmpList[i] = SOStringListEditor.FindProperty(string.Format(ListItem, i)).objectReferenceValue as StringList;
        }
        return tmpList;
    }
    public void SetList(int index, StringList list)
    {
           SOStringListEditor.FindProperty(string.Format(ListItem, index)).objectReferenceValue = list;
    }

    public StringList ListIndex(int index)
    {
        return SOStringListEditor.FindProperty(string.Format(ListItem, index)).objectReferenceValue as StringList;
    }
}


