using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor:Editor
{

  public override void OnInspectorGUI()
  {

    MapGenerator target = this.target as MapGenerator;
    base.OnInspectorGUI();
    if (GUILayout.Button("Button"))
    {
      //code
      var o= target.Generate();

      GameObjectUtility.SetStaticEditorFlags(o, StaticEditorFlags.NavigationStatic);
      //NavMeshBuilder.BuildNavMeshData(NavMeshBuilder.)
    }

  }  
}
