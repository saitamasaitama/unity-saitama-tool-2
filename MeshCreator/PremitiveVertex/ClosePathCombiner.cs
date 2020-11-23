using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Saitama3D.Common;



public class ClosePathCombiner : MonoBehaviour
{
  public ClosePathVertexes A;
  public ClosePathVertexes B;


  [SerializeField]
  private List<Vector3> InnerPointBA = new List<Vector3>();
  [SerializeField]
  private List<Vector3> InnerPointAB = new List<Vector3>();

  [SerializeField]
  private List<(Line, Line)> LineCross = new List<(Line, Line)>();

  private List<Vector3> LineCrossPoint = new List<Vector3>();

  public void Combine()
  {
    InnerPointBA.Clear();
    InnerPointAB.Clear();
    LineCrossPoint.Clear();

    InnerPointBA.AddRange(
      B.worldClosePathLines
        .vertexes
        .Where(p => A.worldClosePathLines.isInPoint(Point2F.From(p)))
        .Select(v2 => new Vector3(v2.x, 0, v2.y))
        .ToList()
    );
    InnerPointAB.AddRange(
      A.worldClosePathLines
        .vertexes
        .Where(p => B.worldClosePathLines.isInPoint(Point2F.From(p)))
        .Select(v2 => new Vector3(v2.x, 0, v2.y))
        .ToList()
    );

    //AとBの交点取得
    var crossesBA =
    B.worldClosePathLines
      .toLines()
      .Where(
        (Line la) => A.worldClosePathLines.isCross(la)
      )
      .Select(
        (Line la) => A.worldClosePathLines
        .toLines()
        .Where((Line lb) => lb.isCross(la))
        .Select((Line lb) => (la, lb))
        .ToList()
      ).ToList();

    foreach (List<(Line, Line)> lc in crossesBA)
    {
      foreach ((Line, Line) item in lc)
      {
        LineCross.Add(item);
        LineCrossPoint.Add(item.Item1.CrossPoint(item.Item2).toVector3());
      }
    }

    //交点を取得したのでそれ以外をつなぐ
    GameObject o = new GameObject("Combined");
    ClosePathVertexes result = o.AddComponent<ClosePathVertexes>();
    ClosePathCombiner combiner = o.AddComponent<ClosePathCombiner>();

    result.vertexes = RecursiveJoinLine(
      A.ClosePathLines,
      B.ClosePathLines
    );
    combiner.A = result;

  }

  private List<Vector3> RecursiveJoinLine(
    ClosePathLines self,
    ClosePathLines rival,
    int startIndex = 0,
    List<Vector3> carry = null
  )
  {
    if (carry == null)
    {
      carry = new List<Vector3>();
    }
    //ゴールは 全部の頂点数 -  内側の点 + 交点の数 
    int total = A.vertexes.Count + B.vertexes.Count;
    int innerPoints = InnerPointBA.Count + InnerPointAB.Count;
    int crossPoints = LineCrossPoint.Count;
    int goal = total + crossPoints - innerPoints;

    if (carry.Count == goal)
    {
      return carry;
    }
    Debug.Log($@"開始:{startIndex}
    終了:{goal}
    内部点:{innerPoints}
    交差点:{crossPoints}
    ");


    foreach ((Line la, int lAindex) in self.toLines().Select((Line l, int index) => (l, index)))
    {

      //開始位置までスキップ
      if (lAindex < startIndex)
      {
        continue;
      }
      //そもそも内側ならcontinue
      if (rival.isInPoint(la.from))
      {
        continue;
      }

      carry.Add(la.from.toVector3());
      foreach ((Line lb, int lBIndex) in rival.toLines().Select((Line l, int index) => (l, index)))
      {


        if (la.isCross(lb))
        {
          //クロスしたので交点を取得
          carry.Add(la.CrossPoint(lb).toVector3());
          int nextIndex = lBIndex + 1;
          //交点の外側を取得
          if (!self.isInPoint(lb.from))
          {

            //順方向
            //ここで反転
            rival.vertexes.Reverse();
            nextIndex = rival.vertexes.Count - lBIndex;
            Debug.Log($"反対方向:{lBIndex}->{nextIndex}");
          }
          else
          {
            Debug.Log($"順方向:{nextIndex}");
            return RecursiveJoinLine(
              rival,
              self,
              nextIndex,//反対方向の場合のインデックスは違うのでは
              carry
            );

          }

        }
      }
    }
    //ここに来た時は0からやり直し
    return RecursiveJoinLine(
      self,
      rival,
      0,
      carry
    );

    //return carry;
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.blue;
    foreach (Vector3 v in InnerPointBA)
    {
      Gizmos.DrawSphere(v, 1f);
    }
    Gizmos.color = Color.green;
    foreach (Vector3 v in InnerPointAB)
    {
      Gizmos.DrawSphere(v, 1f);
    }

    Gizmos.color = Color.yellow;
    foreach (Vector3 v in LineCrossPoint)
    {
      Gizmos.DrawSphere(v, 1f);
    }
  }


  private void OnDrawGizmosSelected()
  {
  }

}