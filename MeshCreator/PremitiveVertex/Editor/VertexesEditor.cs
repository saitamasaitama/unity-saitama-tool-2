using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Vertexes))]
public class VertexesEditor:Editor
{

  public override void OnInspectorGUI()
  {

    Vertexes target = this.target as Vertexes;
    base.OnInspectorGUI();
    if (GUILayout.Button("Button"))
    {
      //code
    }
  }  
}
