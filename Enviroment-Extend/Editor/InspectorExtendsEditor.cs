using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InspectorExtends))]
public class InspectorExtendsEditor:Editor
{

  public override void OnInspectorGUI()
  {

    InspectorExtends target = this.target as InspectorExtends;
    base.OnInspectorGUI();

    
  }

  
}
