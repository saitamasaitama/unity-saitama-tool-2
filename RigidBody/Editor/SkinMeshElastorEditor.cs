using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SkinMeshElastor))]
public class SkinMeshElastorEditor:Editor
{

  public override void OnInspectorGUI()
  {

    SkinMeshElastor target = this.target as SkinMeshElastor;
    base.OnInspectorGUI();
    if (GUILayout.Button("Button"))
    {
      //code
    }
    
  }  
}
