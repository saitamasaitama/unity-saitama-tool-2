using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlendShapeGroup))]
public class BlendShapeGroupEditor:Editor
{



  public override void OnInspectorGUI()
  {


    base.OnInspectorGUI();
    BlendShapeGroup target = this.target as BlendShapeGroup;

    var skins = target.GetComponentsInChildren<SkinnedMeshRenderer>();
    var mesh = skins[0].sharedMesh;
    var blendShapes = new Dictionary<string, int>();

    for (int i = 0; i < mesh.blendShapeCount; i++)
    {
      string name = mesh.GetBlendShapeName(i);
      float weight = target.skinmesh.GetBlendShapeWeight(i);
      string[] s = Regex.Split(name, "__");
      string key = s[1];

      if (!target.BlendShape.ContainsKey(key))
      {
        target.BlendShape.Add(key, weight);
      }
      

      target.BlendShape[key] =EditorGUILayout.Slider(key, target.BlendShape[key], 0, 100);
    }


  }


}
