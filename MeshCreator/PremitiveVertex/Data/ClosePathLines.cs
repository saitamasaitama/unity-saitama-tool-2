using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Saitama3D.Common;

public struct ClosePathLines :
  ICrossingDetection<Lines>
{
  public List<Vector2> vertexes;

  public bool isCross(Lines o)
  {
    return o.isCross(this);
  }
  public bool isCross(Line lB)
  {
    foreach (Line lA in this.toLines())
    {
      if (Line.isCross(lA, lB))
      {
        return true;
      }
    }
    return false;
  }

  public ClosePathLines InterSection(ClosePathLines B)
  {
    //交差している領域を取得する

    //　A -> B  -> A

    return B;
  }
  public bool isInPoint(Point2F p)
  {

    Vector2 p1, p2;
    bool inside = false;
    Vector2 oldPoint = this.vertexes[this.vertexes.Count - 1];
    for (int i = 0; i < this.vertexes.Count; i++)
    {
      Vector2 newPoint = this.vertexes[i];
      if (newPoint.x > oldPoint.x)
      {
        p1 = oldPoint; p2 = newPoint;
      }
      else
      {
        p1 = newPoint; p2 = oldPoint;
      }
      if ((p1.x < p.x) == (p.x <= p2.x)
      && (p.y - p1.y) * (p2.x - p1.x) < (p2.y - p1.y) * (p.x - p1.x))
      {
        inside = !inside;
      }
      oldPoint = newPoint;
    }
    return inside;
  }

  public Lines toLines()
  {
    Lines result = new Lines();
    for (int i = 0; i < vertexes.Count; i++)
    {
      int begin = i;
      int end = (i + 1) % vertexes.Count;

      result.Add(
        Line.From(
          Point2F.From(vertexes[begin]),
          Point2F.From(vertexes[end])
      ));
    }
    return result;
  }

  public static ClosePathLines From(Vector3[] d)
  {
    return new ClosePathLines()
    {
      vertexes = d.Select(v => new Vector2(v.x, v.z)).ToList()
    };
  }
  public static implicit operator Lines(ClosePathLines d)
  {
    return d.toLines();
  }

  public static bool isCross(Lines A, Lines B)
  {
    return A.isCross(B);
  }

}
