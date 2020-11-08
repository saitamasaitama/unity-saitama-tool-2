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
  [MenuItem("Assets/Create/SaitamaCreateGenerator", priority = 42, validate = false)]
  public static void CreateGenerator()
  {
    var gameObject = Selection.activeGameObject;
    //Creatorを作る


  }


  public override void OnInspectorGUI()
  {
    CREATOR target = this.target as CREATOR;
    base.OnInspectorGUI();
    if (GUILayout.Button($"Gen From {typeof(CREATOR)}"))
    {
      var o= target.Generate();
    }
  }  
}
