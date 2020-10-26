using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(phisicSphereCreator))]
public class phisicSphereCreatorEditor:Editor
{

  public override void OnInspectorGUI()
  {

    phisicSphereCreator target = this.target as phisicSphereCreator;
    base.OnInspectorGUI();
    if (GUILayout.Button("Create"))
    {
            target.Create();
    }
    
  }  
}
