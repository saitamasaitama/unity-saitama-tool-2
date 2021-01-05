using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point2D
{
  int x, y;
  public Point2D From(int x,int y)
  {
    return new Point2D() {
      x = x,
      y = y
    };
  }
}

public struct Size2D
{
  int width, height;
  public Size2D From(int width, int height)
  {
    return new Size2D()
    {
      width = width,
      height = height
    };
  }
}

public struct Bound2D
{
  public Point2D point;
  public Size2D size;
  public Bound2D From(Point2D point,Size2D size)
  {
    return new Bound2D()
    {
      point = point,
      size=size
    };
  }
}


public delegate Texture2D Paint(Texture2D tex, Bound2D bound);

public class BitmapPainter
{
  public Paint Fill =(Texture2D tex, Bound2D bound) =>
  {

    return tex;
  };


}


