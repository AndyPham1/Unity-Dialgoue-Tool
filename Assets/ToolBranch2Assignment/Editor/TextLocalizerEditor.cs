using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TextLocalizer))]
public class TextLocalizerEditor : Editor {
    private TextLocalizer TextLocalizer;
    private SerializedObject SOTextLocalizer;
    private SerializedProperty LocalizationList;
    private SerializedProperty UIText;
    private SerializedProperty text;
    private SerializedProperty key;
    private SerializedObject targetStringList;
    private void OnEnable()
    {
        TextLocalizer = (TextLocalizer)target;
        SOTextLocalizer = new SerializedObject(TextLocalizer);
        LocalizationList = SOTextLocalizer.FindProperty("localizationList");
        UIText = SOTextLocalizer.FindProperty("UIText");
        text = SOTextLocalizer.FindProperty("text");
        key = SOTextLocalizer.FindProperty("key");



    } 

    public override void OnInspectorGUI()
    {
        SOTextLocalizer.Update();
        EditorGUILayout.LabelField("Localized Text Selector");
        EditorGUILayout.PropertyField(LocalizationList);
        if (TextLocalizer.localizationList != null)
        {
            if (TextLocalizer.localizationList.targetStringList != null)
            {
                if (TextLocalizer.localizationList.targetStringList.Keys.Count > 0)
                {
                    EditorGUILayout.LabelField("Text Selector", EditorStyles.boldLabel);
                    TextLocalizer.key =
                        EditorGUILayout.Popup("Select Text Key",
                        TextLocalizer.key,
                        TextLocalizer.localizationList.targetStringList.Keys.ToArray()

                        );

                    EditorGUILayout.LabelField("Dialogue Quick Editor", EditorStyles.boldLabel);
                    if (TextLocalizer.key > TextLocalizer.localizationList.targetStringList.Dialogue.Count - 1)
                    {
                        EditorGUILayout.LabelField("Dialogue not found");
                    }
                    else
                    {
                    TextLocalizer.localizationList.targetStringList.Dialogue[TextLocalizer.key] = EditorGUILayout.TextArea(TextLocalizer.localizationList.targetStringList.Dialogue[TextLocalizer.key]);
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("No keys found.");
                }
            }
            else
            {
                EditorGUILayout.LabelField("No target language selected in asset.");
                //EditorGUILayout.LabelField("(target language needed prior to key detection)");
            }
        }
        else
        {
            EditorGUILayout.LabelField("No LocalizationList selected.");
        }
        SOTextLocalizer.ApplyModifiedProperties();


    }
}
