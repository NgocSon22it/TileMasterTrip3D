using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class TestEditor : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Adjust this value to control the rotation speed.

    void Update()
    {
        // Rotate the GameObject around its Y-axis (upward) by the rotationSpeed.
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void SpawnTile()
    {

    }


    /* string fileName = "MP4.txt";

     string status = "Idle";
     string recordButton = "Record";
     bool recording = false;

     [MenuItem("Window/UI Toolkit/Game Setting")]
     public static void ShowExample()
     {
         GetWindow<TestEditor>("Game Setting");
     }
     public void CreateGUI()
     {


     }

     private void OnGUI()
     {
         fileName = EditorGUILayout.TextField("File Name:", fileName);

         GUILayout.BeginHorizontal();
         GUILayout.Button("1");
         GUILayout.Button("2");
         GUILayout.Button("3");
         GUILayout.Button("4");
         GUILayout.EndHorizontal();

         if (GUILayout.Button(recordButton))
         {
             if (recording)  //recording
             {
                 status = "Pause...";
                 recordButton = "Start";
                 recording = false;
             }
             else     // idle
             {
                 status = "Loading...";
                 recordButton = "Stop";
                 recording = true;
             }
         }

         EditorGUILayout.LabelField("Status: ", status);

     }

     public void Choose()
     {
         Debug.Log("Click");
     }*/
}
