using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshPolygonSelector))]
public class MeshPolygonSelectorEditor:Editor
{

  public override void OnInspectorGUI()
  {

    MeshPolygonSelector target = this.target as MeshPolygonSelector;
    base.OnInspectorGUI();
    if (GUILayout.Button("Button"))
    {
      //code
    }
    
  }  
}
