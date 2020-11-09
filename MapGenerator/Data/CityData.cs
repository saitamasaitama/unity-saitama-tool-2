using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MapGen.City;

[Serializable]
public class CityData : GenericData
{
  [SerializeField]
  public List<Line> Avenues;
  [SerializeField]
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
  }

  /*
    リセット処理
  */
  public override void Reset()
  {
    throw new NotImplementedException();
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



}