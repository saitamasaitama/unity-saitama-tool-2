using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(boxcelCreator))]
public class boxcelCreatorEditor:Editor
{

  public override void OnInspectorGUI()
  {

    boxcelCreator target = this.target as boxcelCreator;
    base.OnInspectorGUI();
    if (GUILayout.Button("Button"))
    {
      target.Create();
    }
    
  }  
}
