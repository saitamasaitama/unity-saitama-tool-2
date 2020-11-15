using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(SkinnedMeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class MeshPolygonSelector
  : MonoBehaviour
{
  public SkinnedMeshRenderer SkinnedMeshRenderer => GetComponent<SkinnedMeshRenderer>();
  public MeshCollider MeshCollider => GetComponent<MeshCollider>();

  public Mesh tempMesh;

 // private
  private void Awake()
  {
    Debug.Log("Awake");
    tempMesh = new Mesh();
    tempMesh.name = "tempMesh";
    SkinnedMeshRenderer.BakeMesh(tempMesh);
    MeshCollider.sharedMesh = tempMesh;


  }




}
