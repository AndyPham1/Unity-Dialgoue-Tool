using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogLocalizer : MonoBehaviour{
    public enum DialogueOrReply  { Dialogue,Reply };
    public DialogueGraph DialogueGraph;
    public DialogueOrReply Type;
    public int replyNum;
    public Text UIText;
    public Button button;
    ColorBlock color;
    Color TextColor;
    
	// Use this for initialization
	void Start () {
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        replyNum = replyNum - 1;
        if (Type == DialogueOrReply.Dialogue)
        {
            UIText = GetComponent<Text>();
            UIText.text = DialogueGraph.LocalizationList.targetStringList.Dialogue[DialogueGraph.TargetDialogue.TextKey];
        }
        else if(Type == DialogueOrReply.Reply)
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                GetReply();
            });
            if(replyNum < DialogueGraph.TargetDialogue.getAnsNum())
            {
                ButtonEnabled();
            }
            else {
                ButtonDisable();
            }
        }
	}

    void Update()
    {
        if (Type == DialogueOrReply.Dialogue)
        {
            UIText.text = DialogueGraph.LocalizationList.targetStringList.Dialogue[DialogueGraph.TargetDialogue.TextKey];
        }
        else if (Type == DialogueOrReply.Reply)
        {
            if (replyNum < DialogueGraph.TargetDialogue.getAnsNum())
            {
                ButtonEnabled();
            }
            else
            {
                ButtonDisable();
            }
        }
    }
    void ButtonEnabled()
    {
        button.enabled = true;
        button.GetComponentInChildren<Text>().text = DialogueGraph.LocalizationList.targetStringList.Dialogue[DialogueGraph.TargetDialogue.answers[replyNum].TextKey];

    }

    void ButtonDisable()
    {
        button.GetComponentInChildren<Text>().text = "";
        button.enabled = false;
    }

    void GetReply()
    {
        DialogueGraph.TargetDialogue.DialogueReply(replyNum);
    }

    public void ResetDialogue()
    {
        DialogueGraph.GetFirstNode();
    }


}
