using UnityEngine;
[ExecuteInEditMode]
public class CityCreator : GenericCreator<CityGeneratorParam, CityData>
{
  public override IMapGenerator<CityData> getGenerator(CityGeneratorParam param)
  {
    return new CityGenerator(param);
  }
}

