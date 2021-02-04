using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLocalizerManager : MonoBehaviour {
    public DialogueGraph DialogueGraph;
    private DialogueNode TargetDialogueNode;
    public Text DialogueDisplay;
    public Button[] ButtonList;

	// Use this for initialization
	void Start () {
        TargetDialogueNode = DialogueGraph.GetFirstNode();
        for (int i = 0; i < ButtonList.Length; i++)
        {
            int LambdaClosureIssue = i;
            ButtonList[i].onClick.AddListener(() =>
            {
                GetReply(LambdaClosureIssue);
            });
        }

	}

    public void Reset()
    {
        TargetDialogueNode = DialogueGraph.GetFirstNode();
        DialogueDisplay.text = DialogueGraph.LocalizationList.targetStringList.Dialogue[TargetDialogueNode.TextKey];
    }

    void GetReply(int i)
    {
        TargetDialogueNode.DialogueReply(i);
        TargetDialogueNode = DialogueGraph.TargetDialogue;
    }
	
	// Update is called once per frame
	void Update () {
        DialogueDisplay.text = DialogueGraph.LocalizationList.targetStringList.Dialogue[DialogueGraph.TargetDialogue.TextKey];
        //ButtonSetUp();
        for (int i = 0; i < ButtonList.Length; i++)
        {
            if (i < TargetDialogueNode.getAnsNum() && ButtonList[i] != null)
            {
                ButtonList[i].gameObject.SetActive(true);
                ButtonList[i].GetComponentInChildren<Text>().text =
                    DialogueGraph
                    .LocalizationList
                    .targetStringList
                    .Dialogue[TargetDialogueNode
                    .answers[i]
                    .TextKey];
            }
            else if (ButtonList[i] != null)
            {
                ButtonList[i].GetComponentInChildren<Text>().text = ""; 
                ButtonList[i].gameObject.SetActive(false);
            }

        }
    }


    void ButtonSetUp()
    {

    }
}
