using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ClosePathCombiner))]
public class ClosePathCombinerEditor : Editor
{

  public override void OnInspectorGUI()
  {

    ClosePathCombiner target = this.target as ClosePathCombiner;
    base.OnInspectorGUI();
    if (GUILayout.Button("Button"))
    {
      //code
      if (target.A.worldClosePathLines
        .isCross(target.B.worldClosePathLines))
      {
        //A and Bを出力する        
        target.Combine();


      }
      else
      {
        Debug.Log($"Not Cross...");
      }

    }

  }
}
