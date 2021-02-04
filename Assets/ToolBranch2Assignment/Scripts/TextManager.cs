using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {
    public LocalizationList LocalizationList;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        text1.text = LocalizationList.GetString("SYSTEM_TEXT_1");
        text2.text = LocalizationList.GetString("SYSTEM_TEXT_2");
        text3.text = LocalizationList.GetString("SYSTEM_TEXT_3");
        text4.text = LocalizationList.GetString("SYSTEM_TEXT_4");
    }
}
