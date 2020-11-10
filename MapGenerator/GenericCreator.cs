using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MapGen.City;

using System;
using Random = UnityEngine.Random;


public interface IMapGenerator<T>
{
  T Generate(GameObject o,T oldData);
}

//クラス
public class GenericParameters
{

}

[ExecuteInEditMode]
public abstract class GenericCreator<PARAM,DATA> : MonoBehaviour
  where DATA:GenericData
  where PARAM:GenericParameters
{
  [SerializeField]
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
      for(int i = o.transform.childCount; 0 < i; i--)
      {
        DestroyImmediate(o.transform.GetChild(i - 1).gameObject);
      }
    }
    data = generator.Generate(o,data);
    return o;
  }
}

