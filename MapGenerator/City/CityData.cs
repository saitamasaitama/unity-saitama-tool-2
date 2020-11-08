using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MapGen.City;


public class CityData : MonoBehaviour
{
  public List<Line> Avenues;
  public List<Line> Streets;
  [SerializeField]
  public List<CrossPoint> CrossPoints = new List<CrossPoint>();
  [SerializeField]
  public List<Block> Blocks = new Blocks();

  public void Reculculate()
  {

    CrossPoints crossPoints = culculateCrossPoints();
    CrossPoints = crossPoints;

    Blocks = culcurateBlocks(crossPoints);

    //ビルを立てていく
  }

  private CrossPoints culculateCrossPoints()
  {
    return Avenues.Aggregate(new CrossPoints(),
      (carry, Y) =>
      {
        carry.AddRange(
          Streets.Select(X => CrossPoint.From(X, Y))
            .Where(c => c.HasValue)
            .Select(c => c.Value)
          );
        return carry;
      });

  }

  private Blocks culcurateBlocks(CrossPoints crossPoints)
  {
    return CrossPoints.Aggregate(new Blocks(),
            (carry, P) =>
            {
              CrossPoint bottomLeft = P;
              DirectionCursor<CrossPoint> cursor = crossPoints.Cursor(P);

              if (cursor.E.HasValue && cursor.N.HasValue)
              {
                CrossPoint bottomRight = cursor.E.Value;
                CrossPoint topLeft = cursor.N.Value;
                DirectionCursor<CrossPoint> nextCursor =
                  crossPoints.Cursor(topLeft);

                if (nextCursor.E.HasValue)
                {
                  CrossPoint topRight = nextCursor.E.Value;
                  //Debug.Log($"{topLeft},{bottomLeft},{bottomLeft},{bottomRight}");
                  //揃った
                  carry.Add(
                  Block.From(
                  topLeft, nextCursor.E.Value,
                  bottomLeft, bottomRight)
                  );
                }
              }
              return carry;
            }
          );
  }


  //デバッグ用表示
  private void OnDrawGizmos()
  {
    Gizmos.color = Color.blue;
    foreach (Line l in Streets)
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
    foreach (CrossPoint p in CrossPoints)
    {
      Gizmos.DrawSphere(new Vector3(p.Point.x, 0, p.Point.y), 1);
    }

    Gizmos.color = Color.cyan * 0.5f;
    foreach (Block b in Blocks)
    {
      Gizmos.DrawCube(
        b.Center.toVector3()
        + Vector3.up * 5f,
        new Vector3(
          b.Width - 0.5f,
          10,
          b.Height - 0.5f)
          );
    }

  }

}