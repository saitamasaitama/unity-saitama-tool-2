using System;
using System.Collections;
using System.Collections.Generic;

public class GN<T>
{
  public T Value { get; private set; }
  public List<GN<T>> NeighBour = new List<GN<T>>();

  public GN(T obj) {
    Value = obj;
  }

  public static implicit operator T(GN<T> hoge)
  {
    return hoge.Value;
  }

}

public class Graph<G>:List<GN<G>>{

}

