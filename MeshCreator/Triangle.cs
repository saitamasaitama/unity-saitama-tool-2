using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MeshTool
{

  public struct TriangleA
  {
    public Vector3 A, B, C;

    public void Draw()
    {
      Debug.DrawLine(A, B);
      Debug.DrawLine(A, C);
      Debug.DrawLine(B, C);
    }
  }
}

