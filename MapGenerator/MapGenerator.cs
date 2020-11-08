using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MapGen.City;

using System;
using Random = UnityEngine.Random;


public interface IMapGenerator<T>
{
  T Generate(GameObject o);
}

[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour
{
  public MapData GeneratedCity=null;
  public CityMapGeneratorParam param;

  public GameObject Generate()
  {
    GameObject o = GeneratedCity==null
      ?new GameObject("Map")
      :GeneratedCity.gameObject;


    IMapGenerator<MapData> generator = new CityMapGenerator(param);
    //古いデータは削除
    if (GeneratedCity != null)
    {
      DestroyImmediate(GeneratedCity);
    }
    GeneratedCity = generator.Generate(o);
    return o;
  }
}


public class GraphMapGenerator
{
//  public static Graph

}