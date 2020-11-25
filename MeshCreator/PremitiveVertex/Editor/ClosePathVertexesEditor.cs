using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Saitama3D.Common;

[CustomEditor(typeof(ClosePathVertexes))]
public class ClosePathVertexesEditor : Editor
{

  public ClosePathVertexes testing;
  public override void OnInspectorGUI()
  {

    ClosePathVertexes target = this.target as ClosePathVertexes;
    base.OnInspectorGUI();
    if (GUILayout.Button("Create"))
    {
      target.Create();
    }
    if (GUILayout.Button("RandomCreate"))
    {
      target.CreateRandom();
    }

    if (GUILayout.Button("Test"))
    {
      var isin = target.ClosePathLines.isInPoint(Point2F.From(200, 0));


      Debug.Log($"isin[{isin}]");
    }

  }
}
