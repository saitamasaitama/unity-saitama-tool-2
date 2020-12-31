using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct Triangle
{
  public Vector3 A, B, C;

  public void Draw()
  {
    Debug.DrawLine(A, B);
    Debug.DrawLine(A, C);
    Debug.DrawLine(B, C);
  }
}