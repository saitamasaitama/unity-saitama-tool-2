using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LiteStringCreator))]
public class LiteStringCreatorEditor:Editor
{

  public override void OnInspectorGUI()
  {

    LiteStringCreator target = this.target as LiteStringCreator;
    base.OnInspectorGUI();
    if (GUILayout.Button("Button"))
    {
      target.Create();
    }
    
  }  
}
