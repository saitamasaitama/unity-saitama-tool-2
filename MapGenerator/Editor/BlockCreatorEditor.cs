using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

[CustomEditor(typeof(BlockGenerator))]
public class BlockCreatorEditor:Editor
{

  public override void OnInspectorGUI()
  {

    CityCreator target = this.target as CityCreator;
    base.OnInspectorGUI();
    if (GUILayout.Button("Generate"))
    {
      //code
      var o= target.Generate();
      GameObjectUtility.SetStaticEditorFlags(o, StaticEditorFlags.NavigationStatic);

      //NavMeshBuilder.BuildNavMeshData(NavMeshBuilder.)
    }


  }  
}
