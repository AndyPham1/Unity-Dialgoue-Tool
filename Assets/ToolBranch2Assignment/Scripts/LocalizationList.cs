using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LocalizationList", menuName = "LocalizationList")]
public class LocalizationList : ScriptableObject {
    public StringList targetStringList;
    public List<StringList> StringListList = new List<StringList>();


    public string GetIndex(int index)
    {
        if (targetStringList == null)
            return "";
        return targetStringList.GetIndex(index);
    }
    public string GetString(string name)
    {
        if(targetStringList == null)
            return "";
        return targetStringList.GetString(name);
    }

}
