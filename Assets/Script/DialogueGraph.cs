using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class DialogueGraph : NodeGraph { 
	public LocalizationList LocalizationList;
    public DialogueNode TargetDialogue;

    public DialogueNode GetFirstNode()
    {
       TargetDialogue = nodes.Find(x => { return x is DialogueNode && (x as DialogueNode).isFirstNode == true; }) as DialogueNode;
       return TargetDialogue;
    }

    public int GetTotalDialogueNode()
    {
        return nodes.FindAll(x => { return x is DialogueNode; }).Count;
    }

    public DialogueNode DialogueReply(int i)
    {
        TargetDialogue.DialogueReply(i);
        return TargetDialogue;
    }

    public LocalizationList getLocalizationList()
    {
        return LocalizationList;
    }

    
}