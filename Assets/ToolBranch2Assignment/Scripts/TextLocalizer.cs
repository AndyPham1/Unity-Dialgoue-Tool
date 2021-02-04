using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextLocalizer : MonoBehaviour {
    public LocalizationList localizationList;
    public int key = 0;
    [System.NonSerialized] public Text UIText;
    [System.NonSerialized] public string text;

    private void Start()
    {
        UIText = GetComponent<Text>();   
    }
    public void SetText(int newKey)
    {
        this.key = newKey;
    }

    private void Update()
    {
        if (localizationList.targetStringList != null)
            UIText.text = localizationList.targetStringList.GetIndex(key); 
        else
            UIText.text = ""; 
    }
}
