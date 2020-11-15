using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class QVSceneViewExtendsEditor:SceneViewExtendsEditor
{
  protected override void DoGUI(SceneView scene)
  {
    NavigationIconSets(scene);
  }

  private void NavigationIconSets(SceneView scene)
  {
    Vector2 left = new Vector2(0, 0);
    Vector2 center = new Vector2(30, 0);
    Vector2 right = new Vector2(60, 0);
    Vector2 size = new Vector2(30, 20);
    //scene.camera.transform.rotation *= Quaternion.Euler(Vector3.up * -30);

    Area(new Rect(left, size), () =>
    {
      Button("<<", () =>
      {
        scene.rotation = Quaternion.Euler(Vector3.up * 45)* scene.rotation;
      });
    });
    Area(new Rect(center, size), () =>
    {
      Button("C", () =>
      {
        scene.orthographic = true;
        scene.LookAtDirect(Vector3.zero, Quaternion.Euler(45, 45, 0));
      });
    });


    Area(new Rect(right, size), () =>
    {
      Button(">>", () =>
      {
        scene.rotation = Quaternion.Euler(Vector3.up * -45) * scene.rotation;
      });
    });

  }
}

