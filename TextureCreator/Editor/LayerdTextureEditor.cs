using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LayerdTexture))]
public class LayerdTextureEditor:Editor
{

  public override void OnInspectorGUI()
  {

    LayerdTexture target = this.target as LayerdTexture;
    base.OnInspectorGUI();
    if (GUILayout.Button("Button"))
    {
      //code
    }
    
  }  
}
