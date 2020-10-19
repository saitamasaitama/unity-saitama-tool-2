using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshCreator))]
public class MeshCreatorEditor:Editor
{


  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    MeshCreator target = this.target as MeshCreator;
    if (GUILayout.Button("Create"))
    {
      target.Create(new MeshUtil.CubeCreator());
    }


    if (GUILayout.Button("CreateBonedPipe"))
    {
      target.Create(
        new PipeCreator(
          1.2f,
          0.5f,
          0.4f,
          0,6));
    }

    if (GUILayout.Button("CreateBonedBow"))
    {
      target.Create(
        new BowCreator(
          1.2f,
          0.5f,
          4, 10));
    }


    if (GUILayout.Button("Information"))
    {
      target.DumpInformation();
    }

  }


}
