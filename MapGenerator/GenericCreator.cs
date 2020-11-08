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

//クラス
public class GenericParameters
{

}

[ExecuteInEditMode]
public abstract class GenericCreator<PARAM,DATA> : MonoBehaviour
  where DATA:MonoBehaviour
  where PARAM:GenericParameters
{


  public DATA data=null;
  public PARAM param;

  public abstract IMapGenerator<DATA> getGenerator(PARAM param);

  public GameObject Generate()
  {
    GameObject o = this.gameObject;
    IMapGenerator<DATA> generator = getGenerator(param);
    //古いデータは削除
    if (data != null)
    {
      for(int i = data.transform.childCount; 0 < i; i--)
      {
        DestroyImmediate(data.transform.GetChild(i - 1).gameObject);
      }
      DestroyImmediate(data);
    }
    data = generator.Generate(o);
    //都市を作ったので順次ブロックを生成
    return o;
  }
}

