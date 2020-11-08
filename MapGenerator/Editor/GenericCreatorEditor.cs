using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

//[CustomEditor(typeof(CityCreator))]
public abstract class GenericCreatorEditor<PARAM,DATA,CREATOR>:Editor
  where DATA : MonoBehaviour
  where PARAM : GenericParameters
  where CREATOR :GenericCreator<PARAM,DATA>
{

  public override void OnInspectorGUI()
  {

    CREATOR target = this.target as CREATOR;
    base.OnInspectorGUI();
    if (GUILayout.Button($"Generate {typeof(CREATOR)}"))
    {
      //code
      var o= target.Generate();
      //GameObjectUtility.SetStaticEditorFlags(o, StaticEditorFlags.NavigationStatic);
      //NavMeshBuilder.BuildNavMeshData(NavMeshBuilder.)
    }
  }  
}
