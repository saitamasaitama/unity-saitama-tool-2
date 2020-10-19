using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BowJoint))]
public class BowJointEditor:Editor
{

  public bool mirrorY = false;
  public bool mirrorX = false;
  public bool createChain = true;

  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();
    GUILayout.Label($"CreateBowJoint Settings");
    BowJoint joint = this.target as BowJoint;

    mirrorX = EditorGUILayout.Toggle("Xミラー", mirrorX);
    mirrorY = EditorGUILayout.Toggle("Yミラー", mirrorY);
    GUILayout.Label($"LENGTH={joint.LengthFromRoot:0.000}");


    if (GUILayout.Button("Create"))
    {
      var j= joint.CreateJoint(joint.nextyaw,joint.nextpitch,joint.nextlength);
      Selection.activeObject = j.gameObject;
      if (createChain)
      {
        j.AddChainJoint();
      }

    }

  }


  private void OnSceneGUI()
  {
    BowJoint joint = this.target as BowJoint;
    Quaternion q = Quaternion.Euler(joint.nextpitch,joint.nextyaw, 0);
    Vector3 direction = q * Vector3.forward * joint.nextlength;
    Handles.color = Color.red;   
    Handles.DrawLine(
      joint.transform.position,
      joint.transform.position + direction

      );

  }

}
