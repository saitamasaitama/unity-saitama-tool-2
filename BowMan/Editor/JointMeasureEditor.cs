using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JointMeasure))]
public class JointMeasureEditor:Editor
{

  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    if (GUILayout.Button("Set"))
    {

    }
  }

  
}
