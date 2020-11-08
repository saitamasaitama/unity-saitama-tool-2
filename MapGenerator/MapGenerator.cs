using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using System;
using Random = UnityEngine.Random;


public interface IMapGenerator
{
  void Generate(GameObject o);
}

[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour
{
  public CityMapGeneratorParam param;

  public GameObject Generate()
  {
    GameObject o = new GameObject("Map");
    IMapGenerator generator = new CityMapGenerator(param);
    generator.Generate(o);
    return o;
  }
}


public class GraphMapGenerator
{
//  public static Graph

}