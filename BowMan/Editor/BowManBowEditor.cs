using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BowManBow))]
public class BowManBowEditor:Editor
{
  public float yaw = 0;
  public float pitch = 0;

  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    if (GUILayout.Button("OK"))
    {

    }

    yaw = EditorGUILayout.Slider("yaw",yaw, 0, 360);
    pitch = EditorGUILayout.Slider("pitch", pitch, -90, 90);

  }

}
