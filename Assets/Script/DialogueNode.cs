using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : Node {
    [Input] public Connection Input;
    [Output] public Connection Output;
    [SerializeField] public bool isFirstNode;
    public int TextKey;
    public List<Answer> answers = new List<Answer>();

    public void DialogueReply(int i)
    {
        NodePort port = null;
        if (answers.Count == 0)
        {
            port = GetOutputPort("output");
        }
        else
        {
            if (answers.Count <= i) return;
            port = GetOutputPort(answers[i].portName);
        }

        if (port == null) return;
        for (int h = 0; h < port.ConnectionCount; h++)
        {
            NodePort connection = port.GetConnection(h);
            (connection.node as DialogueNode).setCurrentNode();
        }
    }

    public int getAnsNum()
    {
        return answers.Count;
    }


    // Use this for initialization
    protected override void Init() {
		base.Init();
	}

    private void setCurrentNode()
    {
        DialogueGraph g = (DialogueGraph) graph;
        g.TargetDialogue = this;
    }
    
    [System.Serializable]
    public class Answer
    {
        [TextArea] public int TextKey;
        public string portName;
    }

    [System.Serializable]
    public class Connection
    {
    }
}

