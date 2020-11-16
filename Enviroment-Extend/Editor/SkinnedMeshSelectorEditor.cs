using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class SkinnedMeshSelector : SceneViewExtendsEditor
{
  public bool isShowContextMenu = false;
  public Vector2 lastMousePosition = Vector2.zero;
  private SkinnedMeshRenderer SkinnedMeshRenderer = null;

  public SkinnedMeshSelector()
  {
    OnMouseDown += SkinnedMeshSelector_OnMouseClick;
    OnMouseUp += SkinnedMeshSelector_OnMouseUp;
  }

  //離した場所によってアクション設定
  private void SkinnedMeshSelector_OnMouseUp(int button)
  {
    if (!Selection.activeObject) return;
    if (!Selection.activeGameObject.GetComponent<SkinnedMeshRenderer>()) return;
    if (button != 1) return;

    isShowContextMenu = false;
  }

  private void SkinnedMeshSelector_OnMouseClick(int button)
  {
    if (!Selection.activeObject) return;
    if (!Selection.activeGameObject.GetComponent<SkinnedMeshRenderer>()) return;
    if (button != 1) return;
    isShowContextMenu = true;
    lastMousePosition = Event.current.mousePosition;
    this.SkinnedMeshRenderer = Selection.activeGameObject.GetComponent<SkinnedMeshRenderer>();
  }

  private void ShowContextMenu()
  {

    Vector2 size = new Vector2(50, 30);

    Area(new Rect(lastMousePosition+Vector2.left*100, size), () =>
    {
      Button("←", () =>
      {
        Debug.Log("OK");
      });
    });

    Area(new Rect(lastMousePosition + Vector2.right * 100, size), () =>
    {
      Button("→", () =>
      {
        Debug.Log("OK");
      });
    });
    Area(new Rect(lastMousePosition + Vector2.up * 100, size), () =>
    {
      Button("↑", () =>
      {
        Debug.Log("OK");
      });
    });
    Area(new Rect(lastMousePosition + Vector2.down * 100, size), () =>
    {
      Button("↓", () =>
      {
        Debug.Log("Pose Reset!");
        //ポーズリセット
        PoseReset(this.SkinnedMeshRenderer);    
      });
    });

  }

  private void PoseReset(SkinnedMeshRenderer skinned)
  {
    skinned.rootBone.transform.rotation = Quaternion.Euler(Vector3.zero);
    foreach (Transform t in skinned.rootBone.GetComponentsInChildren<Transform>())
    {
      Debug.Log($"{t.gameObject.name}:reset!");
      t.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
  }


  protected override void DoGUI(SceneView scene)
  {
    if (isShowContextMenu)
    {
      ShowContextMenu();
    }

  }





}

