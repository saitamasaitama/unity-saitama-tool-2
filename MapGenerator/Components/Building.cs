using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;


namespace MapGen.City
{
  public struct Building
  {
    public Point2F Center;
    public float Width;
    public float Height;
    public float GroundHeight;//地上高さ
    public float GroundDepth;//地下
    //親
    public Block owner;
    //public int FloorCount;

  }
}

