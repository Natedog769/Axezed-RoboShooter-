using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class ObjectFormationMaker : EditorWindow
{

    float distanceFromEachOther = 1;
    int numberOfLines = 1;

    GameObject[] selectedObjects { get { return Selection.gameObjects; } }

    [MenuItem("Extentions/Formation Maker")]
    public static void ShowWindow()
    {
        GetWindow<ObjectFormationMaker>("Formation Maker");
    }//makes the window for the editor


    private void OnGUI()
    {//this draws evertything on the editor

        //window header
        GUILayout.Label(" Select objects to put into a formation \n Formations are based off the first selected object", EditorStyles.boldLabel);
        distanceFromEachOther = EditorGUILayout.FloatField(distanceFromEachOther);
        numberOfLines = EditorGUILayout.IntField(numberOfLines);

        EditorGUILayout.LabelField("Currently Selected : ", EditorStyles.boldLabel);
        foreach (GameObject gameObj in selectedObjects)
        {
            EditorGUILayout.LabelField(gameObj.name);
            this.Repaint();
        }

        


        if (GUILayout.Button("Vertical Stack"))
        {

            for (int i = 1; i < selectedObjects.Length; i++)
            {//so formations are based off the Y positions in the world
                selectedObjects[i].transform.position = new Vector3(selectedObjects[i - 1].transform.position.x, selectedObjects[i - 1].transform.position.y + distanceFromEachOther, selectedObjects[i - 1].transform.position.z);
            }
            EditorSceneManager.MarkAllScenesDirty();
        }


        if (GUILayout.Button("Horizontal Line on X Axis"))
        {
            GameObject[,] formation = new GameObject[numberOfLines, selectedObjects.Length/numberOfLines];


            for (int i = 1; i < selectedObjects.Length; i++)
            {//so formations are based off the Y positions in the world
                selectedObjects[i].transform.position = new Vector3(selectedObjects[i - 1].transform.position.x+distanceFromEachOther, selectedObjects[i - 1].transform.position.y, selectedObjects[i - 1].transform.position.z);
           
            }


            //for (int )

            EditorSceneManager.MarkAllScenesDirty();
        }

        if (GUILayout.Button("Horizontal Line on Z Axis"))
        {

            for (int i = 1; i < selectedObjects.Length; i++)
            {//so formations are based off the Y positions in the world
                selectedObjects[i].transform.position = new Vector3(selectedObjects[i - 1].transform.position.x, selectedObjects[i - 1].transform.position.y, selectedObjects[i - 1].transform.position.z + distanceFromEachOther);
            }

            EditorSceneManager.MarkAllScenesDirty();
        }

        if (GUILayout.Button("Horizontal Digaonal Line"))
        {

            for (int i = 1; i < selectedObjects.Length; i++)
            {//so formations are based off the Y positions in the world
                selectedObjects[i].transform.position = new Vector3(selectedObjects[i - 1].transform.position.x + distanceFromEachOther, selectedObjects[i - 1].transform.position.y, selectedObjects[i - 1].transform.position.z + distanceFromEachOther);
            }

            EditorSceneManager.MarkAllScenesDirty();
        }

    }

}
