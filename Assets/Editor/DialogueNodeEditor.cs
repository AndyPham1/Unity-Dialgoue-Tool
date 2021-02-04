using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;


[CustomNodeEditor(typeof(DialogueNode))]
public class DialogueNodeEditor : NodeEditor    
{

    SerializedProperty isFirstNode; 
    public override void OnBodyGUI()
    {
        //isFirstNode = new SerializedObject((target as DialogueNode)).FindProperty("isFirstNode");
        GUILayout.BeginHorizontal();
        DialogueNode DialogueNode = (DialogueNode)target;
        LocalizationList LocalizationList  = (DialogueNode.graph as DialogueGraph).LocalizationList;
        NodeEditorGUILayout.PortField(target.GetInputPort("Input"), GUILayout.Width(100));
        EditorGUILayout.Space();
        if (DialogueNode.answers.Count == 0) NodeEditorGUILayout.PortField(target.GetOutputPort("Output"), GUILayout.Width(100));
        GUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("First Node", EditorStyles.boldLabel);
        bool tmp = EditorGUILayout.Toggle(DialogueNode.isFirstNode);
        bool flag = false;
        foreach (DialogueNode node in DialogueNode.graph.nodes)
            if (tmp==true && node.isFirstNode == true)
                flag = true;
        if (flag == false)
            DialogueNode.isFirstNode = tmp;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("Dialogue", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Dialogue Key");
        DialogueNode.TextKey = EditorGUILayout.Popup(DialogueNode.TextKey, LocalizationList.targetStringList.Keys.ToArray()) ;
        EditorGUILayout.LabelField("Dialogue Content");
        LocalizationList.targetStringList.Dialogue[DialogueNode.TextKey] = EditorGUILayout.TextArea(LocalizationList.targetStringList.Dialogue[DialogueNode.TextKey]);
        EditorGUILayout.LabelField("Dialogue Replies",EditorStyles.boldLabel);
        for (int i = 0; i < DialogueNode.answers.Count; i++)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Key");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("-", GUILayout.Width(30)))
            {
                DialogueNode.RemoveInstancePort(DialogueNode.answers[i].portName);
                DialogueNode.answers.RemoveAt(i);
                i--;
            }
             
            //DialogueNode.answers[i].TextKey = EditorGUILayout.TextField(DialogueNode.answers[i].TextKey);
            DialogueNode.answers[i].TextKey = EditorGUILayout.Popup(DialogueNode.answers[i].TextKey, LocalizationList.targetStringList.Keys.ToArray());
            NodeEditorGUILayout.PortField(new GUIContent(), target.GetOutputPort(DialogueNode.answers[i].portName), GUILayout.Width(-4));
            GUILayout.EndHorizontal();
            EditorGUILayout.LabelField("Content");
            LocalizationList.targetStringList.Dialogue[DialogueNode.answers[i].TextKey] = EditorGUILayout.TextArea(LocalizationList.targetStringList.Dialogue[DialogueNode.answers[i].TextKey]);
            EditorGUILayout.EndVertical();
        }
        if (GUILayout.Button("+", GUILayout.Width(30)))
        {
            NodePort newport = DialogueNode.AddInstanceOutput(typeof(DialogueNode));
            DialogueNode.answers.Add(new DialogueNode.Answer() { portName = newport.fieldName });
        }
    }

    public override int GetWidth()
    {
        return 400;
    }
}
