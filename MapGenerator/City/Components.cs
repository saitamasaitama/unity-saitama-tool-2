using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
namespace MapGen.City
{

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

    public override string ToString()
    {
      return $"[{x},{y}]";
    }
  }


  //線分
  [Serializable]
  public struct Line
  {
    [SerializeField]
    public Point2F from, to;
    public static Line From(Point2F from, Point2F to) => new Line()
    {
      from = from,
      to = to
    };


    public static Line Horizontal(Point2F start, float length) => Line.From(
      start,
      Point2F.From(start.x + length, start.y)
    );
    public static Line Vertical(Point2F start, float length) => Line.From(
      start,
      Point2F.From(start.x , start.y+length)
    );

    public bool isCross(Line B)=>Line.isCross(this,B);
    public static bool isCross(Line A,Line B)
    {
      var ta = (B.from.x - B.to.x) * (A.from.y - B.from.y) + (B.from.y - B.to.y) * (B.from.x - A.from.x);
      var tb = (B.from.x - B.to.x) * (A.to.y - B.from.y) + (B.from.y - B.to.y) * (B.from.x - A.to.x);
      var tc = (A.from.x - A.to.x) * (B.from.y - A.from.y) + (A.from.y - A.to.y) * (A.from.x - B.from.x);
      var td = (A.from.x - A.to.x) * (B.to.y - A.from.y) + (A.from.y - A.to.y) * (A.from.x - B.to.x);
      return tc * td < 0 && ta * tb < 0;
    }

    public static Point2F CrossPoint(Line X,Line Y)
    {
      var det = (X.from.x - X.to.x) * (Y.to.y - Y.from.y) - (Y.to.x - Y.from.x) * (X.from.y - X.to.y);
      var t = ((Y.to.y - Y.from.y) * (Y.to.x - X.to.x) + (Y.from.x - Y.to.x) * (Y.to.y - X.to.y)) / det;
      var x = t * X.from.x + (1.0 - t) * X.to.x;
      var y = t * X.from.y + (1.0 - t) * X.to.y;

      return Point2F.From((float)x,(float)y);
    }

    public override string ToString()
    {
      return $"FROM({from.x},{from.y})=>TO({to.x},{to.y})";
    }

  }

  /*
   *大きい道路に挟まれた四角形の空間ビルが複数ある
   *
  */
  public struct Block
  {
    public CrossPoint TopLeft, TopRight, BottomLeft, BottomRight;

  }
  public class Blocks : List<Block>
  {

  }

  public struct RoadInfo
  {
    public int CarLines;//

  }

  //道路の縦
  public class Avenue 
  {
    public Line line;
    private void OnDrawGizmos()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawLine(
        new Vector3(line.from.x, 0, line.from.y),
        new Vector3(line.to.x, 0, line.to.y)
      );
    }
  }

  public class Street 
  {
    public Line line;

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawLine(
        new Vector3(line.from.x, 0, line.from.y),
        new Vector3(line.to.x, 0, line.to.y)
      );
    }
  }

  public struct CrossPoint
  {
    public Point2F Point;
    public Line X;
    public Line Y;

    public static CrossPoint? From(Line X,Line Y)
    {
      if (!Line.isCross(X, Y))
      {
        return null;
      }

      //交点を出力

      return new CrossPoint() { 
        X=X,
        Y=Y,
        Point=Line.CrossPoint(X,Y)
      };
    }
  }
  public class CrossPoints : List<CrossPoint>
  {
    public CrossPoints FindManHattenLength(CrossPoint A,int length,ref CrossPoints carry)
    {
      if (carry == null)
      {
        carry = new CrossPoints();
      }
      if (length <= 0)
      {
        return carry;
      }



      //UP (+Y)
      CrossPoint up=this.Where(B =>  A.Point.x.Equals(B.Point.x) && A.Point.y < B.Point.y)
        .OrderBy(B =>Mathf.Abs(B.Point.y - A.Point.y))
        .First();
      CrossPoint down = this.Where(B => A.Point.x.Equals(B.Point.x) && B.Point.y< A.Point.y )
        .OrderBy(B => Mathf.Abs(B.Point.y - A.Point.y))
        .First();

      CrossPoint right = this.Where(B => 
        A.Point.y.Equals(B.Point.y) 
        && A.Point.x < B.Point.x)
        .OrderBy(B => Mathf.Abs(B.Point.x - A.Point.x))
        .First();
      CrossPoint left = this.Where(B =>
        A.Point.y.Equals(B.Point.y)
        &&  B.Point.x< A.Point.x)
        .OrderBy(B => Mathf.Abs(B.Point.x - A.Point.x))
        .First();

      if (!carry.Contains(up)) carry.Add(down);
      if (!carry.Contains(down)) carry.Add(down);
      if (!carry.Contains(right)) carry.Add(down);
      if (!carry.Contains(left)) carry.Add(down);




      return carry;
    }
  }

  public class MapData : MonoBehaviour
  {
    public List<Line> Avenues;
    public List<Line> Streets;
    public CrossPoints CrossPoints=new CrossPoints();
    public List<Block> Blocks = new List<Block>();

    public void Reculculate()
    {
      CrossPoints =
        Avenues.Aggregate(new CrossPoints(),
          (carry, Y) => {
            carry.AddRange(
              Streets.Select(X => CrossPoint.From(X, Y))
                .Where(c => c.HasValue)
                .Select(c => c.Value)
              );
            return carry;
          });
      Blocks = 
        CrossPoints.Aggregate(new List<Block>(),
          (carry, P) =>
          {
            //U,UR,Rを集めて

            return carry;
          }
        );
      
    }


    //四面を取得
    public List<Block> Squares{

      get { 
        //原点、上、上右、右でポイントをとっていく
      
      return new List<Block>(); 
      }
    }


    private void OnDrawGizmos()
    {
      Gizmos.color = Color.blue;
      foreach(Line l in Streets)
      {
        Gizmos.DrawLine(
          new Vector3(l.from.x, 0, l.from.y),
          new Vector3(l.to.x, 0, l.to.y)
        );
      }
      Gizmos.color = Color.red;
      foreach (Line l in Avenues)
      {
        Gizmos.DrawLine(
          new Vector3(l.from.x, 0, l.from.y),
          new Vector3(l.to.x, 0, l.to.y)
        );
      }

      Gizmos.color = Color.red;
      foreach(CrossPoint p in CrossPoints)
      {
        Gizmos.DrawSphere(new Vector3(p.Point.x, 0, p.Point.y), 1);
      }
    }

  }
}
