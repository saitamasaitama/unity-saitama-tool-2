using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BowMan))]
public class BowManEditor:Editor
{



  //まず最初にボールを作成
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();
    BowMan bowman = this.target as BowMan;

    if (GUILayout.Button("ResetRoot"))
    {
      bowman.ResetBow();


    }

    
   



  }



}
