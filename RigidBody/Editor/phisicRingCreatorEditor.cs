using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(phisicRingCreator))]
public class phisicRingCreatorEditor:Editor
{

  public override void OnInspectorGUI()
  {

    phisicRingCreator target = this.target as phisicRingCreator;
    base.OnInspectorGUI();

    if (GUILayout.Button("Create"))
    {
      //code
      target.Create();
    }
  }

  

  
}
