using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MapGen.City;

using System;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class CityCreator : GenericCreator<CityGeneratorParam, CityData>
{
  public override IMapGenerator<CityData> getGenerator(CityGeneratorParam param)
  {
    return new CityGenerator(param);
  }
}

