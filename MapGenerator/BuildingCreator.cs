
using UnityEngine;
[ExecuteInEditMode]
public class BuildingCreator : GenericCreator<BuildingGeneratorParam, BuildingData>
{
  public override IMapGenerator<BuildingData> getGenerator(BuildingGeneratorParam param)
  {
    return new BuildingGenerator(param);
  }
}
