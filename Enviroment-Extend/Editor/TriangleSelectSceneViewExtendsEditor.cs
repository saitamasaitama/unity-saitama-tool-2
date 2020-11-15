using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using System.Linq;



/*
 レイを飛ばすやつ
 */
public class TriangleSelectSceneViewExtends : SceneViewExtendsEditor
{
  private bool isHit = false;
  public SkinnedMeshRenderer selectSkinnedMesh = null;



  protected override void DoGUI(SceneView scene)
  {
    WriteSelected();
    if (this.SelectMesh(scene))
    {



    }
    //meshを

  }

  private bool SelectMesh(SceneView scene)
  {
    Event e = Event.current;
    if (!Selection.activeObject) return false;

    MeshCollider collider = Selection.activeGameObject.GetComponent<MeshCollider>();

    if (!collider) return false;
    if (!(e.type == EventType.MouseDown && e.button == 0)) return false;

    Debug.Log("Ray");

    Vector3 mousePos = e.mousePosition;
    float ppp = EditorGUIUtility.pixelsPerPoint;
    mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
    mousePos.x *= ppp;
    Ray ray = scene.camera.ScreenPointToRay(mousePos);

    if (!collider.Raycast(ray, out RaycastHit hit, float.MaxValue)) return false;

    
    var selector = hit.collider.gameObject.GetComponent<MeshPolygonSelector>();
    int index = hit.triangleIndex;
    //根元のmeshのindexは？
    var skinmesh = hit.collider.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;

    var i= skinmesh.triangles[index];
    Vector3 p0 = skinmesh.vertices[skinmesh.triangles[hit.triangleIndex * 3 + 0]];
    Vector3 p1 = skinmesh.vertices[skinmesh.triangles[hit.triangleIndex * 3 + 1]];
    Vector3 p2 = skinmesh.vertices[skinmesh.triangles[hit.triangleIndex * 3 + 2]];


    var o= GameObject.CreatePrimitive(PrimitiveType.Sphere);
    o.transform.position = hit.point;
    o.transform.localScale = Vector3.one * 0.024f;

    o = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    o.transform.position = p0;
    o.transform.localScale = Vector3.one * 0.05f;
    o = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    o.transform.position = p1;
    o.transform.localScale = Vector3.one * 0.05f;
    o = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    o.transform.position = p2;
    o.transform.localScale = Vector3.one * 0.05f;


    e.Use();
    return true;
  }

  private void WriteSelected()
  {
    if (!isHit) return;
    //Debug.Log("mesh");

  }
}

