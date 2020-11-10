using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public abstract class SceneViewExtendsEditor
{
  //対象のエディタを指定
  static readonly List<SceneViewExtendsEditor> SceneViewEditors = new List<SceneViewExtendsEditor>()
  {
    new QVSceneViewExtendsEditor()
  };



  static SceneViewExtendsEditor()
  {
    Handles.BeginGUI();
    foreach(var e in SceneViewEditors)
    {
      SceneView.duringSceneGui += e.OnGUI;
    }
    Handles.EndGUI();
  }


  protected abstract void OnGUI(SceneView scene);


  public static void Area(Rect rect, Action A)
  {
    GUILayout.BeginArea(rect);

    A();

    GUILayout.EndArea();
  }


  public static void Button(String label,Action A)
  {
    if (GUILayout.Button(label))
    {
      A();
    }
  }

}

