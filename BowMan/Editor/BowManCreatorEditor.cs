using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BowManCreator))]
public class BowManCreatorEditor:Editor
{


  public BowManCreator bowmanCreator => this.target as BowManCreator;
  

  public override void OnInspectorGUI()
  {
      base.OnInspectorGUI();
    if (GUILayout.Button("Create"))
    {

      GameObject o= new GameObject("BowMan");
       
      o.gameObject.AddComponent<BowMan>();
      var joint= o.gameObject.AddComponent<BowJoint>();

      joint.length = 0;
      var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      sphere.transform.SetParent(o.transform);
      sphere.transform.localPosition = Vector3.zero;
      sphere.transform.localScale = Vector3.one * 0.1f;


    }


  }



}
