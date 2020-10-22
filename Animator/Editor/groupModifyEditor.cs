using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(groupModify))]
public class groupModifyEditor:Editor
{

  public override void OnInspectorGUI()
  {

    groupModify target = this.target as groupModify;
    base.OnInspectorGUI();

    
  }

  
}
