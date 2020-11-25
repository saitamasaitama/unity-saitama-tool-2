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
    /*
    new SkinnedMeshSelector(),
    new QVSceneViewExtendsEditor(),
    new TriangleSelectSceneViewExtends()
    */ 
  };


  protected event Action<int> OnMouseDown;
  protected event Action<int> OnMouseUp;




  static SceneViewExtendsEditor()
  {
    
    foreach(var e in SceneViewEditors)
    {
      SceneView.duringSceneGui += e.OnGUI;      
    }
    
    
  }
  /*
  private void OnGUI(SceneView scene)
  {

  }
  */

  protected void OnGUI(SceneView scene)
  {
    Handles.BeginGUI();
    Event e = Event.current;


    
    DoGUI(scene);

    if (e.type == EventType.MouseDown)
    {
      OnMouseDown?.Invoke(e.button);
    }
    else if (e.type == EventType.MouseUp)
    {
      OnMouseUp?.Invoke(e.button);
    }

    Handles.EndGUI();
  }

  protected abstract void DoGUI(SceneView scene);

  


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

