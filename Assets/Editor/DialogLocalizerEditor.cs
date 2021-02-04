using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogLocalizer))]
public class DialogLocalizerEditor : Editor {
    private DialogLocalizer DialogLocalizer;
    private SerializedObject SODialogLocalizer;
    private SerializedProperty DialogueGraph;
    private SerializedProperty Type;
    private SerializedProperty replyNum;
    private SerializedProperty UIText;
    private SerializedProperty button;

    private void OnEnable()
    {
        DialogLocalizer = (DialogLocalizer)target;
        SODialogLocalizer = new SerializedObject(DialogLocalizer);
        DialogueGraph = SODialogLocalizer.FindProperty("DialogueGraph");
        Type = SODialogLocalizer.FindProperty("Type");
        replyNum = SODialogLocalizer.FindProperty("replyNum");
        UIText = SODialogLocalizer.FindProperty("UIText");
        button = SODialogLocalizer.FindProperty("button");

    }

    public override void OnInspectorGUI()
    {
        SODialogLocalizer.Update();
        EditorGUILayout.PropertyField(DialogueGraph);
        EditorGUILayout.PropertyField(Type);
        if(DialogLocalizer.Type == DialogLocalizer.DialogueOrReply.Dialogue)
        {
            EditorGUILayout.PropertyField(UIText);
        }
        else if (DialogLocalizer.Type == DialogLocalizer.DialogueOrReply.Reply)
        {
            EditorGUILayout.PropertyField(replyNum);
            EditorGUILayout.PropertyField(button);
        }

        SODialogLocalizer.ApplyModifiedProperties();

    }
}
