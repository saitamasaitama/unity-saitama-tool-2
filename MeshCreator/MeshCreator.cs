using MeshUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class MeshCreator : MonoBehaviour
{
  private MeshFilter MeshFilter => this.GetComponent<MeshFilter>();
  private MeshRenderer MeshRenderer => this.GetComponent<MeshRenderer>();

  public MeshTopology topology = MeshTopology.Quads;

  public bool isDebugDraw = true;


  public void Update()
  {



  }

  private void OnDrawGizmos()
  {
    if (!isDebugDraw) return;
    var mesh = MeshFilter.sharedMesh;

    Vector3[] vertices = mesh.vertices;
    Vector3[] normals = mesh.normals;

    for (var i = 0; i < normals.Length; i++)
    {
      Vector3 pos = vertices[i];
      pos.x *= transform.localScale.x;
      pos.y *= transform.localScale.y;
      pos.z *= transform.localScale.z;
      pos += transform.position;

      Debug.DrawLine
      (
          pos,
          pos + normals[i] * 0.33f, Color.red
      );
      UnityEditor.Handles.Label(pos, $"v{i}");
    }
  }


  public void Create(IMeshCreator creator)
  {
    Mesh mesh = new Mesh();
    mesh.name = "Mesh";

    //とりあえず立方体を作成する


    mesh.SetVertices(creator.Vertics());
    mesh.SetIndices(creator.Indices(), MeshTopology.Quads, 0);
    mesh.SetNormals(creator.Normals());
    mesh.SetUVs(0, creator.UV());


    mesh.RecalculateBounds();
    mesh.RecalculateNormals();
    mesh.RecalculateTangents();
    

    Debug.Log(mesh.vertices
      .Select(v => $"[{v.x:0.00},{v.y:0.00},{v.z:0.00}]")
      .Aggregate((carry, item) => $"{carry}\n{item}")
    );


    MeshFilter.sharedMesh = mesh;
    MeshFilter.mesh = mesh;
  }



  public void DumpInformation()
  {
    var mesh = MeshFilter.sharedMesh;

    Debug.Log($@"
      VERTICS={mesh.vertices.Length}
      NORMALS={mesh.normals.Length}
    ");
  }

  private int[] quad2TriIndex(int A, int B, int C, int D)
  {
    return new int[] {
      A,B,D,C
    };
  }

  private Vector3[] faceVertics() => new Vector3[]{
    new Vector3(-1f,1,1),//左上
    new Vector3(1f,1,1),//右上
    new Vector3(-1f,1,-1),//左下
    new Vector3(1f,1,-1),//左上

  };

}

