using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HumanouidSensingGenerate))]
public class HumanouidSensingGenerateEditor:Editor
{

  public override void OnInspectorGUI()
  {

    HumanouidSensingGenerate target = this.target as HumanouidSensingGenerate;
    base.OnInspectorGUI();

    
  }

  
}
