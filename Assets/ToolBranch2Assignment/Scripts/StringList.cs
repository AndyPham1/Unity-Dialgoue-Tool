using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StringList", menuName = "StringList")]
[System.Serializable]
public class StringList : ScriptableObject {
    public string LanguageName = ""; 
    public List<string> Keys = new List<string>();
    public List<string> Dialogue = new List<string>();

    public string GetIndex(int index)
    {
        if (index > Dialogue.Count - 1)
            return "";
        if (Dialogue[index] == null)
            return "";
        return Dialogue[index];
    }
    public string GetString(string key)
    {
        int index = Keys.IndexOf(key);
        if (index != -1)
        {
            return Dialogue[index];
        }
        else
        {
            return "";
        }
    }

}
