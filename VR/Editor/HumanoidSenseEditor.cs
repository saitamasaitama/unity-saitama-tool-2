using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HumanoidSense))]
public class HumanoidSenseEditor:Editor
{

  public override void OnInspectorGUI()
  {

    HumanoidSense target = this.target as HumanoidSense;
    base.OnInspectorGUI();

    
  }

  
}
