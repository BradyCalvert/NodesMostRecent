using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodesEditor : EditorWindow {
    public List<NodeBaseClass> myWindows = new List<NodeBaseClass>();
    [MenuItem("Window/Node editor")]

 
static void ShowWindow()
    {
        NodesEditor editor = EditorWindow.GetWindow<NodesEditor>();
    }
    private void OnGUI()
    {
            for(int i=0;i<myWindows.Count;i++)
        {
            for(int j =0; j < myWindows[i].linkedNodes.Count;j++)
            {
                DrawNoxeCurve(myWindows[i].rect, myWindows[myWindows[i].linkedNodes[j]].rect);
            }
        }
        if(GUILayout.Button("Add Node"))
            {
            myWindows.Add(new NodeBaseClass(new Rect(20,20,100,100),myWindows.Count));
            myWindows[myWindows.Count - 1].CloseFunction += RemoveNode;
            if(myWindows.Count>1)
            {
                myWindows[myWindows.Count - 2].AttatchNode(myWindows.Count - 1);
            }
        }
        BeginWindows();
        for (int i = 0; i< myWindows.Count; i++)
        {
            myWindows[i].rect = GUI.Window(i, myWindows[i].rect, myWindows[i].DrawGUI, myWindows[i].title);
        }

        EndWindows();
    }

    void DrawNoxeCurve(Rect start, Rect end)
    {
        Vector3 starPos = new Vector3(start.x + start.width, start.y + (start.height / 2),0);
        Vector3 endPos = new Vector3(end.x, end.y + (end.height / 2), 0);
        Vector3 startTan = starPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Handles.DrawBezier(starPos, endPos, startTan, endTan, Color.black,null,1);
    }

    public void RemoveNode(int winID)
    {
        RemoveAttatchments(winID);
        myWindows.RemoveAt(winID);

        ReassignIDs();
    }

    public void ReassignIDs()
    {
        for(int i=0;i<myWindows.Count;i++)
        {
            myWindows[i].id-=1;
        }
    }
    public void RemoveAttatchments(int winID)
    {
        for (int i = 0; i < myWindows.Count; i++)
        {
            myWindows[i].linkedNodes.Remove(winID);
        }
    }
}
