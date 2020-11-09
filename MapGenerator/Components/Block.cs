using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MapGen.City
{
  /*
   *道路に挟まれた四角形の空間
   *
  */
  [Serializable]
  public struct Block
  {
    public CrossPoint TopLeft, TopRight, BottomLeft, BottomRight;

    public float Width;
    public float Height;



    public Point2F Center => Line.CrossPoint(
        Line.From(TopLeft.Point, BottomRight.Point),
        Line.From(TopRight.Point, BottomLeft.Point)
      );


    public static Block From(
     CrossPoint TopLeft,
     CrossPoint TopRight,
     CrossPoint BottomLeft,
     CrossPoint BottomRight)
    {
      return new Block()
      {
        TopLeft = TopLeft,
        TopRight = TopRight,
        BottomLeft = BottomLeft,
        BottomRight = BottomRight,
        Width = Mathf.Abs(TopLeft.Point.x - TopRight.Point.x),
        Height = Mathf.Abs(TopLeft.Point.y - BottomLeft.Point.y)
      };
    }
  }
  public class Blocks : List<Block>
  {


  }
}
