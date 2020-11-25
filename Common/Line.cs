using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Saitama3D.Common
{


  [Serializable]
  public class Lines : List<Line>,
  ICrossingDetection<Lines>,
  ICrossingDetection<Line>
  {
    public bool isCross(Lines lines)
    {
      return this.Where(l => lines.isCross(l)).Any();
    }

    public bool isCross(Line o)
    {
      return this.Where(l => l.isCross(o)).Any();
    }
  }

  public interface ICrossingDetection<T>
  {
    bool isCross(T o);
  }

  //頂点
  [Serializable]
  public struct Point2F
  {
    [SerializeField]
    public float x, y;
    public static Point2F From(float x, float y) => new Point2F()
    {
      x = x,
      y = y
    };
    public static Point2F From(Vector2 v) => new Point2F()
    {
      x = v.x,
      y = v.y
    };

    public static Point2F From(Vector3 v) => new Point2F()
    {
      x = v.x,
      y = v.z
    };


    public override string ToString()
    {
      return $"[{x},{y}]";
    }

    public Vector3 toVector3()
    {
      return new Vector3(x, 0, y);
    }
  }

  [Serializable]
  public struct Line : ICrossingDetection<Line>
  {
    [SerializeField]
    public Point2F from, to;
    public static Line From(Point2F from, Point2F to) => new Line()
    {
      from = from,
      to = to
    };

    public Point2F CrossPoint(Line Y)
    {
      return Line.CrossPoint(this, Y);
    }


    public static Line Horizontal(Point2F start, float length) => Line.From(
      start,
      Point2F.From(start.x + length, start.y)
    );
    public static Line Vertical(Point2F start, float length) => Line.From(
      start,
      Point2F.From(start.x, start.y + length)
    );

    public bool isCross(Line B) => Line.isCross(this, B);
    public static bool isCross(Line A, Line B)
    {
      var ta = (B.from.x - B.to.x) * (A.from.y - B.from.y) + (B.from.y - B.to.y) * (B.from.x - A.from.x);
      var tb = (B.from.x - B.to.x) * (A.to.y - B.from.y) + (B.from.y - B.to.y) * (B.from.x - A.to.x);
      var tc = (A.from.x - A.to.x) * (B.from.y - A.from.y) + (A.from.y - A.to.y) * (A.from.x - B.from.x);
      var td = (A.from.x - A.to.x) * (B.to.y - A.from.y) + (A.from.y - A.to.y) * (A.from.x - B.to.x);
      return tc * td < 0 && ta * tb < 0;
    }

    public static Point2F CrossPoint(Line X, Line Y)
    {
      var det = (X.from.x - X.to.x) * (Y.to.y - Y.from.y) - (Y.to.x - Y.from.x) * (X.from.y - X.to.y);
      var t = ((Y.to.y - Y.from.y) * (Y.to.x - X.to.x) + (Y.from.x - Y.to.x) * (Y.to.y - X.to.y)) / det;
      var x = t * X.from.x + (1.0 - t) * X.to.x;
      var y = t * X.from.y + (1.0 - t) * X.to.y;

      return Point2F.From((float)x, (float)y);
    }

    public override string ToString()
    {
      return $"FROM({from.x},{from.y})=>TO({to.x},{to.y})";
    }
  }


}
