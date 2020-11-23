using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Saitama3D.Common;
using UnityEditor;

[ExecuteInEditMode]
public class ClosePathVertexes : Vertexes
{
  public ClosePathLines localClosePathLines =>
  ClosePathLines.From(vertexes.ToArray());
  public ClosePathLines worldClosePathLines => ClosePathLines;
  public ClosePathLines ClosePathLines => ClosePathLines.From(
      vertexes
      .Select(v => transform.rotation * v + this.transform.position)
      .ToArray()
      );


  public float Radius = 1.0f;
  [Range(3, 100)]
  public int Points = 10;
  [Range(3, 6)]
  public int PointsMin = 6;
  [Range(6, 20)]
  public int PointsMax = 10;
  public Vector2 BiasWide = new Vector2(1, 1);
  public float RandomRangeMin = 0.8f;
  public float RandomRangeMax = 1.2f;


  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    int fromIndex = 0;


    Gizmos.DrawSphere(this.transform.position, 0.03f);
    //回転させてから移動

    List<Vector3> WorldVertexes = this.vertexes
      .Select(v => transform.rotation * v + this.transform.position).ToList();

    Handles.BeginGUI();
    foreach (Vector3 v in WorldVertexes)
    {
      Handles.Label(v, $"v{fromIndex}");

      int nextIndex = fromIndex + 1;
      Gizmos.DrawLine(
          WorldVertexes[fromIndex % WorldVertexes.Count],
          WorldVertexes[nextIndex % WorldVertexes.Count]);
      Gizmos.DrawSphere(v, 0.01f);
      fromIndex++;
    }
    Handles.EndGUI();
  }


  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    foreach (Vector3 v in vertexes)
    {
      Gizmos.DrawSphere(this.transform.position + v, 0.01f);
    }
  }

  public void Create()
  {
    this.vertexes = new List<Vector3>();
    float step = 360 / Points;
    for (int i = 0; i < Points; i++)
    {
      float round = step * i;
      Vector3 pos = Quaternion.Euler(0, round, 0) * this.transform.forward * Radius;
      this.vertexes.Add(pos);
    }
  }

  public void CreateRandom()
  {
    this.vertexes = new List<Vector3>();
    //まずpointsを設定
    int points = Random.Range(PointsMin, PointsMax);

    float[] steps = new float[points];
    float[] ranges = new float[points];

    steps = steps.Select((float s) => (float)(360f / (float)points)).ToArray();
    ranges = ranges.Select((float s) => Random.Range(Radius * RandomRangeMin, Radius * RandomRangeMax)).ToArray();

    int shuffleCount = points * 10;
    for (int i = 0; i < shuffleCount; i++)
    {
      //ランダム
      int A = Random.Range(0, points);
      int B = Random.Range(0, points);

      float diff = Math.Abs(Random.Range(
          0,
          Math.Min(steps[A], steps[B]) * 0.25f
          ));
      steps[A] -= diff;
      steps[B] += diff;

    }

    for (int i = 0; i < points; i++)
    {
      float round = steps.Where((v, index) => index <= i).Sum();
      float range = ranges[i];
      Vector3 pos = Quaternion.Euler(0, round, 0) * this.transform.forward * range;
      this.vertexes.Add(pos);
    }

  }

  public void Combine(ClosePathLines B)
  {

  }

}