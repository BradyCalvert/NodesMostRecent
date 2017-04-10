using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBaseClass : MonoBehaviour {
    public int id;
    public Rect rect;
    public string title="";

    public List<int> linkedNodes = new List<int>();

    public delegate void voidFunction(int id);

    public voidFunction CloseFunction;

    public NodeBaseClass(Rect r, int idNum)
    {
        id = idNum;
        rect = r;
    }


    public virtual void DrawGUI(int winID)
    {
        GUILayout.Label(winID.ToString());
        BaseDraw(winID);
    }

    public void BaseDraw(int winID)
    {
        Color temp=GUI.backgroundColor;
        GUI.backgroundColor = Color.green;
        if (GUI.Button(new Rect(rect.width - 18, 1, 18, 18), "X")) 
        {
            CloseFunction(winID);
        }
        GUI.backgroundColor = temp;
        GUI.DragWindow();
    }

    public void AttatchNode(int nodeID)
    {
        linkedNodes.Add(nodeID);
    }
}
